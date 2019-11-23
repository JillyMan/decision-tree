from bayes_core import *
from bayes_data_layer import get_data, BinaryType, RangeType
from bayes_render import ui_render, ui_render_message

# Plan for tomorow:
# - check on consistency (i.e Can i make answer?)

is_running = True
tags = []
attrs = []
hipoths = []
used_attribute = []

def get_attribute(prices):
    not_used = max(prices.items(), key=lambda x: x[1])
    used_attribute[not_used[0]] = True
    return attrs[not_used[0]]

def calc_probs(pp, pm, p):
    phe = phe_func(p, pp, pm)
    phne = phe_func(p, 1 - pp, 1 - pm)
    return (phe, phne)

def recalculate_attr_price():
    '''ph = P(H:E), phne = P(H:not E)'''
    prices = {}
    for tag in tags:
        attr_id = tag.attribute.id
        if used_attribute[attr_id]:
            continue

        if attr_id not in prices:
            prices[attr_id] = 0

        (phe, phne) = calc_probs(tag.pp, tag.pm, tag.hipothesis.p)
        prices[attr_id] += abs(phe - phne)
    return prices

#----Fucking copy paste------------
def recalculate_hipothesis_binary(tags, r):
    for tag in tags:
        hipothesis = tag.hipothesis       
        (phe, phne) = calc_probs(tag.pp, tag.pm, hipothesis.p)
        hipothesis.p = interpolate_result_binary(phne, phe, r)

def recalculate_hipothesis_range(tags, r):
    for tag in tags:
        hipothesis = tag.hipothesis
        (phe, phne) = calc_probs(tag.pp, tag.pm, hipothesis.p)
        hipothesis.p = interpolate_result_clamp01(phne, hipothesis.p, phe, r)
#------------

def recalculate_hypothesis(attribute_id, answer):
    filtered_tags = filter(lambda x: x.attribute.id == attribute_id, tags)
    if answer.type == BinaryType:
        recalculate_hipothesis_binary(filtered_tags, answer.value)
    elif answer.type == RangeType:
        recalculate_hipothesis_range(filtered_tags, answer.normalize())
    else:
        raise NotImplementedError('Invalid answer type')

def check_end():
    if all(used for used in used_attribute):
        is_running = False

def debug_hypothesis(tags):
    d = {}
    for tag in tags:
        h = tag.hipothesis
        if h.name not in d:
            d[h.name] = h.p

    for h in d.items():
        print(f'{h[0]} = {h[1]}')

def bayes_run():
    while is_running:
        prices = recalculate_attr_price()
        attribute = get_attribute(prices)
        answer = ui_render(attribute)
        recalculate_hypothesis(attribute.id, answer)
        debug_hypothesis(tags)
        check_end()
    ui_render_message('...............')

def init(data):
    global tags, attrs, hipoths, used_attribute
    hipoths = data[0]
    attrs = data[1]
    tags = data[2]
    used_attribute = [False] * len(attrs)