from bayes_data_layer import get_data
from bayes_render import ui_render, ui_render_message
from bayes_core import *

#Plan for tomorow: need recalculate all probability, and check on consistency (i.e Can i'm give answer?)

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
    return True

def bayes_system():
    while check_end():       
        prices = recalculate_attr_price()
        attribute = get_question(prices)       
        answer = ui_render(attribute)
#       recalculate_hypotesis(attribute, answer)

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
bayes_system()
#unit_test()