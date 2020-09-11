import mnist_loader
training_data, validation_data, test_data = mnist_loader.load_data_wrapper()
training_data = list(training_data)

import network 
# три слоя 784 - тк картинка 30-прост 10 - выход
net = network.Network([784, 30, 10])
# 7 эпох
net.Train(training_data, 30, 10, 3.0, 0.1, test_data=test_data, early_stopping_n=3)
#net.save("net.dat")

net = network.load("net.dat")

import numpy as np
count = 0
total = 0
for x, d in validation_data:
    total += 1
    y = net.feedforward(x)
    if (d != np.argmax(y)):
        count += 1
        #print("{} isn't {}".format(np.argmax(y), d))
print(count)
print(total)
print("Accurate : {}".format(1 - count/total))
    

