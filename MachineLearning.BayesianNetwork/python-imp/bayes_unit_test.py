from bayes_core import *
from bayes_service import *

def test_prices():
    prices = recalculate_attr_price()
    l = list(prices.items())
    l.sort(key=lambda x: x[0])
    print(l)
    question = get_attribute(prices)
    print(question.question)

def test_normalize_user_input():
    answer = Range(-5, 5, 0)
    a = Attribute(1, 'name1', 'question1', RangeType)
    h1 = Hipothesis(1, 'h1', 0.1)
    h2 = Hipothesis(2, 'h2', 0.05)
    h3 = Hipothesis(3, 'h3', 0.01)
    tags = [
        Tag(h1, a, 1, 0.01),
        Tag(h2, a, 0.9, 0.02),
        Tag(h3, a, 0.9, 0.2),
    ]
    recalculate_hypothesis(tags, a)
    print(tags)

def range_test():
    r1 = Range(-5, 5, 0)
    print(r1.normalize())
    r1 = Range(-5, 5, 5)
    print(r1.normalize())
    r1 = Range(-5, 5, -5)
    print(r1.normalize())
    r1 = Range(-3, 4, 2)
    print(r1.normalize())
    r1 = Range(-3, 4, 4)
    print(r1.normalize())
    r1 = Range(-10, 1, 1)
    print(r1.normalize())
    r1 = Range(-10, 1, -1)
    print(r1.normalize())

def unit_test():
    # test_prices()
    # test_normalize_user_input()
    # range_test()
    pass

#unit_test()
