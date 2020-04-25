using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FindPrimeNumbersParallel
{
    class MainClass
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            const int numParts = 5;
            var primes = new List<int>[numParts];
            Parallel.For(0, numParts, i => primes[i] = GetPrimeNumbers(i == 0 ? 2 : i * 2000000 + 1, (i + 1) * 2000000));
            var result = primes.Sum(p => p.Count);
            Console.WriteLine("Primes found: {0}\nTotal time: {1}", result, sw.ElapsedMilliseconds);
        }

        private static List<int> GetPrimeNumbers(int minimum, int maximum)
        {
            List<int> result = new List<int>();
            for (int i = minimum; i <= maximum; i++)
            {
                if (IsPrimeNumber(i))
                {
                    result.Add(i);
                }
            }
            return result;
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
