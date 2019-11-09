from bayes_core import Boolean, Range

def ui_render_message(message):
    print(message)

def ui_render(question):
    print("Question:", question.question)
    answer = ui_input_range_type(question.type)
    return answer

def ui_input_range_type(input_type):
    if input_type == 'Binary':
        return ui_boolean_input()
    elif input_type == 'Range':
        return ui_range_input()
    else:
        print('Invalid input')

def ui_range_input():
    start = input('from->')
    end = input('to->')
    value = input('value->')
    return Range(start, end, value)

def ui_boolean_input():
    while True:
        result = input('Yes (1) or No (0) -> ')
        if result == '1':
            return Boolean(1)
        elif result == '0':
            return Boolean(0)
        else:
            print("Try again")