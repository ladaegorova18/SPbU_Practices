using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Philosophers
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = 10;
            var forks = new object[n];
            var philosophers = new Philosopher[n];
            var threads = new Thread[n];
            for (var i = 0; i < n; ++i)
            {
                forks[i] = new object();
            }
            for (var i = 0; i < n; ++i)
            {
                philosophers[i] = new Philosopher(forks, i);
                threads[i] = new Thread(philosophers[i].Live);
                threads[i].Start();
            }
            Console.ReadLine();
            foreach (var cell in philosophers)
            {
                cell.Alive = false;
            }
        }
    }
}
