from bayes_data_layer import get_data
from bayes_render import ui_render
from bayes_core import *

isRunning = True
tags = []
attrs = []
hipoths = []
used_attribute = []

def get_question(prices):
    not_used = max(prices.items(), key=lambda x: x[1])
    used_attribute[not_used[0]] = True
    return attrs[not_used[0]]

def recalculate_attr_price():
    prices = {}
    for tag in tags:
        attr_id = tag.attribute.id
        if used_attribute[attr_id]:
            continue

        if attr_id not in prices:
            prices[attr_id] = 0

        pp = tag.pp
        pm = tag.pm
        p = tag.hipothesis.p
        phe = get_phe(p, pp, pm)
        pnhe = get_phe(p, 1 - pp, 1 - pm)
        prices[attr_id] += abs(phe - pnhe)
    return prices

def recalculate_hypotesis(question_info, answer):
    pp, pm = 0.0
    if answer.type == 'Range':
        lerp(pp,)
    elif answer.type == 'Binary':
        pass

def check_end():
    isRunning = False
    pass

def bayes_system():
    while isRunning:
        recalculate_attr_price()
        question_info = get_question()
        answer = ui_render(question_info)
        recalculate_hypotesis(question_info, answer)
        check_end()

#bayes_system()

def test_prices():
    prices = recalculate_attr_price()
    l = list(prices.items())
    l.sort(key=lambda x: x[0])
    print(l)
    question = get_question(prices)
    print(question.question)

def init():
    global tags, attrs, hipoths, used_attribute
    _tuple = get_data()
    hipoths = _tuple[0]
    attrs = _tuple[1]
    tags = _tuple[2]
    used_attribute = [False] * len(attrs)

def unit_test():
    test_prices()

init()
unit_test()