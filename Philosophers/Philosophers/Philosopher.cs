using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Philosophers
{
    public class Philosopher
    {
        private object fork1;
        private object fork2;
        private Random rnd = new Random();
        private int number;

        private const int minTime = 0;
        private const int maxTime = 1;

        private int countPhilosophers;
        public bool Alive { get; set; } = true;

        public Philosopher(object[] forks, int i)
        {
            number = i;
            fork1 = forks[i];
            countPhilosophers = forks.Length;
            if (i + 1 >= forks.Length)
            {
                fork1 = forks[0];
                fork2 = forks[i];
                return;
            }
            fork2 = forks[i + 1];
        }

        public void Live()
        {
            while (Alive)
            {
                Console.WriteLine("eating");
                Eat();
                Think();
            }
        }

        private void Eat()
        {
            Console.WriteLine($"I am a philosopher number {number} and take a fork number {number}");
            lock (fork1)
            {
                Console.WriteLine($"I am a philosopher number {number} and take a fork number {(number + 1) % countPhilosophers}");
                lock (fork2)
                {
                    Thread.Sleep(rnd.Next(minTime, maxTime));
                }
            }
        }

        private void Think()
        {
            Console.WriteLine("I am a philosopher number: " + number);
            Thread.Sleep(rnd.Next(minTime, maxTime));
        }
    }
}
