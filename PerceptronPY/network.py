import random
import json
import sys

import numpy as np

class QuadraticCost(object):

    @staticmethod
    def fn(y, d):
        """Вернуть стоимость, связанную с `` y`` и желаемым выводом ``d``.     """
        return 0.5*np.linalg.norm(y-d)**2

    @staticmethod
    def delta(y, d):
        """Возвращаем ошибку дельты из выходного слоя."""
        return (y-d) * phi_prime(y)


class CrossEntropyCost(object):

    @staticmethod
    def fn(y, d):
        """Вернуть стоимость, связанную с `` y`` и желаемым выводом ``d``.     """
        return np.sum(np.nan_to_num(-d*np.log(y)-(1-d)*np.log(1-y)))

    @staticmethod
    def delta(y, d):
        """Возвращаем ошибку дельты из выходного слоя."""
        return (y-d)



class Network(object):

    def __init__(self, sizes, cost=CrossEntropyCost):
        """Список` `size``  Уклоны и веса для
         сети инициализируются случайным образом, используя гауссовский
         распределение со средним 0 и дисперсией 1. Обратите внимание, что первый
         слой считается входным слоем, и по соглашению мы
         не будет устанавливать смещения для этих нейронов, так как смещения только
         когда-либо использовался для вычисления выходов из более поздних слоев."""
        
        self.cost = cost
        self.num_layers = len(sizes)
        self.sizes = sizes
        self.biases = [np.random.randn(y, 1) for y in sizes[1:]]
        self.db = [np.zeros(b.shape) for b in self.biases]
        self.weights = [np.random.randn(y, x)
                        for x, y in zip(sizes[:-1], sizes[1:])]
        self.dw = [np.zeros(w.shape) for w in self.weights]

    def feedforward(self, a):
        """Вернуть выход сети если ``a`` входной."""
        for b, w in zip(self.biases, self.weights):
            a = phi(np.dot(w, a)+b)
        return a

    def Train(self, training_data, epochs, mini_batch_size, eta, inertion,
            test_data=None, early_stopping_n=0):
        """Обучаем нейронную сеть с помощью мини-пакетного стохастика
         градиентный спуск. `` Training_data`` - это список кортежей
         `` (x, y) ``, представляющий тренировочные входы и желаемый
         выходы. Другие необязательные параметры
         само за себя. Если указано `` test_data``, то
         сеть будет оцениваться по данным испытаний  """

        training_data = list(training_data)
        n = len(training_data)

        if test_data:
            test_data = list(test_data)
            n_test = len(test_data)

        best_accuracy=0
        no_accuracy_change=0

        for j in range(epochs):
            random.shuffle(training_data)
            mini_batches = [training_data[k:k+mini_batch_size]
                                for k in range(0, n, mini_batch_size)]
            for mini_batch in mini_batches:
                self.update_mini_batch(mini_batch, eta, inertion)
            if test_data:
                if (early_stopping_n > 0):
                    accuracy = self.accuracy(test_data)
                    if accuracy > best_accuracy:
                        best_accuracy = accuracy
                        no_accuracy_change = 0
                    else:
                        no_accuracy_change += 1
                    if no_accuracy_change == early_stopping_n:
                        print("Training complete")
                        return
                print("Epoch {} : {} / {}".format(j,self.evaluate(test_data),n_test));
            else:
                print("Epoch {} complete".format(j))

    def update_mini_batch(self, mini_batch, eta, inertion):
        """Обновить весовые коэффициенты сети, применив
         градиентный спуск с использованием обратного распространения в одну мини-серию.
         `` Mini_batch`` - это список кортежей `` (x, y) ``, `` eta``
         это скорость обучения. """
        speed = eta/len(mini_batch)
        nabla_b = [np.zeros(b.shape) for b in self.biases]
        nabla_w = [np.zeros(w.shape) for w in self.weights]
        for x, y in mini_batch:
            cur_nabla_b, cur_nabla_w = self.backprop(x, y)
            nabla_b = [nb + nb_k for nb, nb_k in zip(nabla_b, cur_nabla_b)]
            nabla_w = [nw + nw_k for nw, nw_k in zip(nabla_w, cur_nabla_w)]
        self.dw = [inertion*_dw + speed*nw
                        for _dw, nw in zip(self.dw, nabla_w)]
        self.db = [inertion*_db + speed*nb
                       for _db, nb in zip(self.db, nabla_b)]
        self.weights = [w - _dw for w, _dw in zip(self.weights, self.dw)]
        self.biases = [b - _db for b, _db in zip(self.biases, self.db)]


    def backprop(self, x, d):
        """Вернуть кортеж` `(nabla_b, nabla_w)` `, представляющий
         градиент для функции стоимости C_x. `` nabla_b`` и
         `` nabla_w`` - это послойные списки массивов, похожие
         к `` self.biases`` и `` self.weights``. """
        cur_nabla_b = [np.zeros(b.shape) for b in self.biases]
        cur_nabla_w = [np.zeros(w.shape) for w in self.weights]
        # Идём вперед
        y = x
        yList = [x] # список для хранения всех активаций, слой за слоем
        for b, w in zip(self.biases, self.weights):
            v = np.dot(w, y)+b
            y = phi(v)
            yList.append(y)
        # двигаемся назад
        delta = self.cost.delta(y, d)#self.cost_derivative(yList[-1], d) * phi_prime(yList[-1])
        cur_nabla_b[-1] = delta
        cur_nabla_w[-1] = np.dot(delta, yList[-2].transpose())
        
        for layer in range(2, self.num_layers):            
            dphi = phi_prime(yList[-layer])
            delta = np.dot(self.weights[-layer+1].transpose(), delta) * dphi
            cur_nabla_b[-layer] = delta
            cur_nabla_w[-layer] = np.dot(delta, yList[-layer-1].transpose())
        return (cur_nabla_b, cur_nabla_w)

    def evaluate(self, test_data):
        """Возвращает количество тестовых входов, для которых нейронный
         сеть выводит правильный результат. Обратите внимание, что нейронный
         выход сети считается индексом того, что
         Нейрон в последнем слое имеет наибольшую активацию."""
        test_results = [(np.argmax(self.feedforward(x)), y)
                        for (x, y) in test_data]
        return sum(int(x == y) for (x, y) in test_results)

    def cost_derivative(self, y, d):
        """Вернуть вектор частных производных \ частный C_x /
         \ частичное a для выходных активаций."""
        return y-d

    def accuracy(self, data, convert=False):
        """Возвращает количество входных данных в `` data``, для которых нейронный
        сеть выводит правильный результат. Нейронные сети
        Предполагается, что выход является индексом любого нейрона в
        последний слой имеет наибольшую активацию.

        Флаг `` convert`` должен быть установлен в False, если набор данных
        данные проверки или проверки (обычный случай), и в True, если
        набор данных является данными обучения. 
        """
        if convert:
            results = [(np.argmax(self.feedforward(x)), np.argmax(y))
                       for (x, y) in data]
        else:
            results = [(np.argmax(self.feedforward(x)), y)
                        for (x, y) in data]

        result_accuracy = sum(int(x == y) for (x, y) in results)
        return result_accuracy

    
    def save(self, filename):
        """Сохраняет веса в файл ``filename``."""
        data = {"sizes": self.sizes,
                "weights": [w.tolist() for w in self.weights],
                "biases": [b.tolist() for b in self.biases],
                "cost": str(self.cost.__name__)}
        f = open(filename, "w")
        json.dump(data, f)
        f.close()

#### Loading a Network
def load(filename):
    """Загрузка из файла ``filename``.  вернёт сеть    """
    
    f = open(filename, "r")
    data = json.load(f)
    f.close()
    cost = getattr(sys.modules[__name__], data["cost"])
    net = Network(data["sizes"], cost=cost)
    net.weights = [np.array(w) for w in data["weights"]]
    net.biases = [np.array(b) for b in data["biases"]]
    return net



####№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№№
def phi(v):
    ###
    return 1.0/(1.0+np.exp(-v))

def phi_prime(y):
    """Производная предыдущей."""
    return y*(1-y)
