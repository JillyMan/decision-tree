// function value(p, n, name) {
//   this.name = name;
//   this.p = p;
//   this.n = n;
//   this.qty = p + n;
//   this.entropy = !!!p || !!!n ? 0 :
//                 (-p/this.qty)*Math.log2(p/this.qty) +
//                 (-n/this.qty)*Math.log2(n/this.qty);
// }

function value(results, name) {
  this.name = name;
  this.qty = result.length;
  this.results = results;
  this.entropy = 0;
  results.Keys(key) {
    let value = result[key];
    if(value === 0) return 0;
    this.entropy += -(value / this.qty) * Math.log2(value/this.qty);
  }
}

function feature(values, name) {
  this.name = name;
  this.values = values;
  this.infoEntr = getInfoEntropy(values);

  function getInfoEntropy(v) {
    let pn = 0;
    v.forEach((item) => pn += item.qty);

    let entr = 0;
    v.forEach((item) => {
      let resultSum = 0;
      item.results.ForEach(x) {
        resultSum += x;
      }
      entr + = (resultSum / pn) * item.entropy;
    });

    return entr;
  }
}

function gain(s0, feature) {
  return s0 - feature.infoEntr;
}

function getMaxInfoEntr(s0, features) {
  let maxGain = 0;
  let i = 0;
  features.forEach((item, i) => {
    let itemGain = gain(s0, item);
    if(maxGain < itemGain) {
      maxGain = itemGain;
      index = i;
    }
  })

  return {gain: maxGain, feature: features[i]};
}

const pred_lt = (x, y) => x < y;
const pred_gt = (x, y) => x > y;
const pred_lq = (x, y) => x <= y;
const pred_gq = (x, y) => x >= y;
const pred_eq = (x, y) => x === y;

const predicats = {
  'lt': pred_lt,
  'gt': pred_gt,
  'lq': pred_lq,
  'gq': pred_gq,
  'eq': pred_eq
};

function id3(features) {
}

function decision_tree_build() {
  let s0 = new value(4, 3).entropy;
  let features = [
    new feature([new value([2, 2], 'yes'), new value([1, 2], 'no')], `higher in the table`),
    new feature([new value([4, 1], 'home'), new value([0, 2], 'guest')], `where game`),
    new feature([new value([2, 1], 'here'), new value([3, 1], 'are not here')], `leads`),
    new feature([new value([1, 2], 'yes'), new value([3, 1], 'no')], `rain`)
  ];

  let tree = id3();
  let result = getMaxInfoEntr(s0, features);
  console.log(result.feature.name);
  printWithPreccision(result.gain);
}

function printWithPreccision(val) {
  console.log(val.toFixed(3));
}
