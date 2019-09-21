const fs = require('fs')
const PATH_TO_TRANING_SET = 'C:\\Users\\Artsiom\\Documents\\Projects\\DecisionTree\\DecisionTree\\training_set.json'

function value(results, name) {
	this.name = name
	this.results = results
	this.entropy = 0
	this.qty = 0

	results.forEach(x => this.qty += x)
	results.forEach((value) => {
		if (value === 0) return false
		this.entropy -= (value / this.qty) * Math.log2(value / this.qty)
	})
}

function feature(values, name) {
	this.name = name
	this.values = values
	this.infoEntr = getInfoEntropy(values)

	function getInfoEntropy(values) {
		let pn = 0
		values.forEach((item) => pn += item.qty)

		let entr = 0
		values.forEach((exodus) => {
			let resultSum = 0
			exodus.results.forEach((x) => {
				resultSum += x
			})
			entr += (resultSum / pn) * exodus.entropy
		})

		return entr
	}
}

function tree_node(childs) {
	this.childs = childs
}

function gain(s0, feature) {
	return s0 - feature.infoEntr
}

function getMaxInfoEntr(s0, features) {
	let maxGain = 0
	let maxIndex = 0
	features.forEach((item, i) => {
		let itemGain = gain(s0, item)
		if (maxGain < itemGain) {
			maxGain = itemGain
			maxIndex = i
		}
	})

	return { gain: maxGain, ...features[maxIndex] }
}

function build_s0(array, name) {
	let keyValue = []
	array.forEach(x => {
		isNaN(keyValue[x]) ? keyValue[x] = 1 : keyValue[x]++
	})
	return new value(Object.values(keyValue), name)
}

function build_features(input, decisions, name) {
	let freqOfEvent = []
	input.forEach((val, i) => {
		let decision = decisions[i]

		if(isNaN(freqOfEvent[val])) {
			freqOfEvent[val] = []
		}

		if(isNaN(freqOfEvent[val][decision])) {
			freqOfEvent[val][decision] = 0
		}
		freqOfEvent[val][decision]++
	})

	let values = []
	for(let valName in freqOfEvent) {
		// todo: mb save name of decision ?
		let freqEachDecision = Object.values(freqOfEvent[valName])
		values.push(new value(freqEachDecision, valName))
	}

	return new feature(values, name)
}

function buildDataSet(traning_set) {
	let resultFeatures = []

	let keys = Object.keys(traning_set)
	let decision_set = traning_set[keys[keys.length-1]]

	for(let i = 0; i < keys.length - 1; ++i) {
		let traningAttributeValues = traning_set[keys[i]]
		let nameOfAttribute = keys[i]
		resultFeatures.push(build_features(traningAttributeValues, decision_set, nameOfAttribute))
	}

	return resultFeatures
}

function decision_tree_build(traning_set) {
	let decision_s0 = build_s0(traning_set.decision, 'decision')
	console.log(decision_s0.name + 'S0: ' + decision_s0.entropy)
	
	let features = buildDataSet(traning_set)
	features.forEach(x => console.log(x))

	let root = getRoot(decision_s0.entropy, features)
	console.log("Root is:", root)
}

function getRoot(s0, features) {
	let result = getMaxInfoEntr(s0, features)
	let node = new tree_node(result.values)
	return node;
}

function id3(date_set) { 
	let root_feature = getRoot(date_set)

	root_feature.forEach(value => {
		var child = getRootWhenS0(value);
		value.addChild(child);
	});
}

fs.readFile(PATH_TO_TRANING_SET, 'utf8', (err, jsonString) => {
	if (err) {
		console.log("Error reading file from disk:", err)
		return
	}
	try {
		let data_set = JSON.parse(jsonString)
		decision_tree_build(data_set)
	} catch (err) {
		console.log('Error parsing JSON string:', err)
	}
})

const pred_lt = (x, y) => x < y
const pred_gt = (x, y) => x > y
const pred_lq = (x, y) => x <= y
const pred_gq = (x, y) => x >= y
const pred_eq = (x, y) => x === y

const predicats = {
	'lt': pred_lt,
	'gt': pred_gt,
	'lq': pred_lq,
	'gq': pred_gq,
	'eq': pred_eq
}