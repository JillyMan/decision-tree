from bayes_service import init, bayes_run
from bayes_data_layer import get_data

data = get_data()
init(data)
bayes_run()