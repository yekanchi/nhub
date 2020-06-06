import matplotlib
import matplotlib.pyplot as plt
import numpy as np

def runMaintest():
    # Data for plotting
    t = np.arange(0.0, 8.0, 0.001)
    s = np.sin(2 * np.pi * t)

    fig, ax = plt.subplots()
    ax.plot(t, s)

    ax.set(xlabel='time (s)', ylabel='voltage (mV)',
        title='About as simple as it gets, folks')
    ax.grid()

    fig.savefig("test.png")
    plt.show()