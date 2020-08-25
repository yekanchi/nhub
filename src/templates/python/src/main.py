
import matplotlib
import matplotlib.pyplot as plt
import numpy as np



print("hello world")

def doJob(jobName):
    print("job is done for job:" + jobName)

doJob("my job")


# Data for plotting
t = np.arange(0.0, np.pi, 0.2)
s = 1 + np.sin(2 * np.pi * t)

fig, ax = plt.subplots()
ax.plot(t, s)


ax.set(xlabel='time (s)', ylabel='voltage (mV)',
       title='sin wave demonstration with matplotlib')
ax.grid()


fig.savefig("test.png")
plt.show()