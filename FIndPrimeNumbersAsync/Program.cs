using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FIndPrimeNumbersAsync
{
    class MainClass
    {
        static void Main(string[] args)
        {
            ProcessPrimesAsync();
            Console.ReadLine();
        }

        private static async void ProcessPrimesAsync()
        {
            var sw = new Stopwatch();
            sw.Start();
            const int numParts = 10;
            Task<List<int>>[] primes = new Task<List<int>>[numParts];
            for (int i = 0; i < numParts; i++)
            {
                primes[i] = GetPrimeNumbersAsync(i == 0 ? 2 : i * 1000000 + 1, (i + 1) * 1000000);
            }
            var results = await Task.WhenAll(primes);
            Console.WriteLine("Total prime numbers: {0}\nProcess time: {1}", results.Sum(p => p.Count), sw.ElapsedMilliseconds);
        }

        private static async Task<List<int>> GetPrimeNumbersAsync(int minimum, int maximum)
        {
            var count = maximum - minimum + 1;
            List<int> result = new List<int>();

            return await Task.Factory.StartNew(() =>
            {
                for (int i = minimum; i <= maximum; i++)
                {
                    if (IsPrimeNumber(i))
                    {
                        result.Add(i);
                    }
                }
                return result;
            });
        }

        static bool IsPrimeNumber(int number)
        {
            if (number % 2 == 0)
            {
                return number == 2;
            }
            else
            {
                var topLimit = (int)Math.Sqrt(number);
                for (int i = 3; i <= topLimit; i += 2)
                {
                    if (number % i == 0) return false;
                }
                return true;
            }
        }
    }
}
