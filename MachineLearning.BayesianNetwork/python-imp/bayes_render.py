from bayes_core import Binary, Range

def ui_render_message(message):
    print(message)

def ui_render(question):
    print("Question:", question.question)
    answer = ui_input_range_type(question.type)
    return answer

def ui_input_range_type(input_type):
    if input_type == 'Binary':
        return ui_binary_input()
    elif input_type == 'Range':
        return ui_range_input()
    else:
        print('Invalid input')

def ui_range_input():
    # hard code !!!!!
    # start = input('from->')
    # end = input('to->')
    value = input('Input value from [-5, 5] -> ')
    return Range(-5, 5, value)

def ui_binary_input():
    while True:
        result = input('Yes (1) or No (0) -> ')
        if result == '1':
            return Binary(1)
        elif result == '0':
            return Binary(0)
        else:
            print("Try again")