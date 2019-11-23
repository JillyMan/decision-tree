from bayes_core import *

def get_data():
    h1 = Hipothesis(0, 'Low acum', 0.1)
    h2 = Hipothesis(1, 'have not oil', 0.05)
    h3 = Hipothesis(2, 'break zajiganie', 0.01)
    h4 = Hipothesis(3, 'bad svechi', 0.01)

    a1 = Attribute(0, "On lights", "Is lights  on?", BinaryType)
    a2 = Attribute(1, "Low oil pointer", "Is oil pointer low?", RangeType)
    a3 = Attribute(2, "Otsirela car", "Does car longer stay under rain?", RangeType)
    a4 = Attribute(3, "Tech-service", "How much old car doesn't use service?", RangeType)
    a5 = Attribute(4, "Starter ratates", "Does starter rotates?", RangeType)
    a6 = Attribute(5, "Car not on", "Auto doesn't run?", BinaryType)

    hipoths = [h1, h2, h3, h4]
    attrs = [a1, a2, a3, a4, a5, a6]

    tags = [
        Tag(h1, a1, 0, 0.99),
        Tag(h1, a2, 0.7, 0.05),
        Tag(h1, a4, 0.2, 0.5),
        Tag(h1, a5, 0, 0.99),
        Tag(h1, a6, 1, 0.01),

        Tag(h2, a2, 1, 0.01),
        Tag(h2, a6, 0.9, 0.02),

        Tag(h3, a3, 0.9, 0.1),
        Tag(h3, a4, 0.25, 0.5),
        Tag(h3, a6, 0.9, 0.02),

        Tag(h4, a4, 0.01, 0.5),
        Tag(h4, a6, 0.9, 0.02),
    ]

    return (hipoths, attrs, tags)