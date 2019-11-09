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

class Boolean(InputType):
    def __init__(self, value):
        InputType.__init__(self, 'bool')

class Range(InputType):
    def __init__(self, start, end, value):
        InputType.__init__(self, 'range', value)
        self.start = start
        self.end = end

class QuestionInfo():
    answer_info = None
    def __init__(self, question):
        self.question = question
 
def get_phe(p, pp, pm):
    return (p * pp) / (p*pp + (1-p)*pm)

def lerp(start, end, t): 
    return start + (end - start) * t