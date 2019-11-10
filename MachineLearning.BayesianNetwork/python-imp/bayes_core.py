import math

RangeType = 'Range'
BinaryType = 'Binary'

class Hipothesis:
    def __init__(self, id, name, p):
        self.id = id
        self.name = name
        self.p = p

class Attribute:
    def __init__(self, id, name, question, _type):
        self.id = id
        self.name = name
        self.question = question
        self.type = _type

class Tag:
    def __init__(self, hipothesis, attribute, pp, pm):
        self.pp = pp
        self.pm = pm
        self.attribute = attribute
        self.hipothesis = hipothesis

class InputType:
    def __init__(self, _type, value):
        self.type = _type
        self.value = int(value)

class Binary(InputType):
    def __init__(self, value):
        InputType.__init__(self, BinaryType, value)

class Range(InputType):
    def __init__(self, start, end, value):
        InputType.__init__(self, RangeType, value)
        self.start = int(start)
        self.end = int(end)

    def normalize(self):
        s = 0
        e = abs(self.start) + abs(self.end)
        v = abs(self.start) + self.value
        return v / (s + e)

def phe_func(p, pp, pm):
    return (p * pp) / (p * pp + (1-p) * pm)

def lerp(start, end, t): 
    return start + (end - start) * t

def interpolate_result_clamp01(phne, ph, phe, r):
    if r > 0.5:
        return lerp(ph, phe, r)
    elif r < 0.5:
        return lerp(phne, ph, r)
    return ph

def interpolate_result_binary(phne, phe, r):
    return phne if r == 0 else phe
