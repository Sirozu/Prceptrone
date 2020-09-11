import pickle
import gzip
import numpy as np

def load_data():
    """Вернуть данные MNIST в виде кортежа, содержащего данные обучения,
     данные проверки и данные испытаний.
     `` Training_data`` возвращается как кортеж с двумя записями.
     Первая запись содержит фактические тренировочные образы. Это
     Numpy ndarray с 50000 записей. Каждая запись, в свою очередь,
     numpy ndarray с 784 значениями, представляющими 28 * 28 = 784
     пикселей в одном изображении MNIST.
     Вторая запись в кортеже `` training_data`` - это numpy ndarray
     содержащий 50000 записей. Эти записи просто цифры
     значения (0 ... 9) для соответствующих изображений, содержащихся в первом
     запись кортежа.
     `` Validation_data`` и `` test_data`` похожи, за исключением
     каждый содержит только 10000 изображений.
     `` training_data`` портится в `` load_data_wrapper () ``
    """
    f = gzip.open('mnist.pkl.gz', 'rb')
    training_data, validation_data, test_data = pickle.load(f, encoding="latin1")
    f.close()
    return (training_data, validation_data, test_data)

def load_data_wrapper():
    """Вернуть кортеж, содержащий `` (training_data, validation_data,
     test_data) ``. Основаный на `` load_data``
     В частности, `` training_data`` - это список, содержащий 50 000
     2-кортежи `` (x, y) ``. `` x`` - 784-мерный numpy.ndarray
     содержащий входное изображение. `` y`` является 10-мерным
     numpy.ndarray, представляющий единичный вектор, соответствующий
     правильная цифра для `` x``.
     `` validation_data`` и `` test_data`` являются списками, содержащими 10000
     2-кортежи `` (x, y) ``. В каждом случае `` x`` является 784-мерным
     numpy.ndarry, содержащий входное изображение, а `` y`` - это
     соответствующая классификация, то есть значения цифр (целые числа)
     соответствует `` x``."""
    tr_d, va_d, te_d = load_data()
    training_inputs = [np.reshape(x, (784, 1)) for x in tr_d[0]]
    training_results = [vectorized_result(y) for y in tr_d[1]]
    training_data = zip(training_inputs, training_results)
    validation_inputs = [np.reshape(x, (784, 1)) for x in va_d[0]]
    validation_data = zip(validation_inputs, va_d[1])
    test_inputs = [np.reshape(x, (784, 1)) for x in te_d[0]]
    test_data = zip(test_inputs, te_d[1])
    return (training_data, validation_data, test_data)

def vectorized_result(j):
    """Вернуть 10-мерный единичный вектор с 1,0 в смезении
        положение и нули в другом месте. Это используется для преобразования цифры
     (0 ... 9) в соответствующий желаемый выход из нейронного
     сеть."""
    e = np.zeros((10, 1))
    e[j] = 1.0
    return e
