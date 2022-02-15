using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeGeneratorWithGUI
{
    public class PrimeGenerator
    {
        public List<long> GetPrimesSequential(long fromNumber, long toNumber)
        {
            List<long> primes = new List<long>();

            foreach (long currentNumber in getNumberRange(fromNumber, toNumber))
            {
                bool isPrime = true;
                long division = currentNumber / 2;
                for (long i = 2; i <= division; i++)
                {
                    if (currentNumber % i == 0)
                    {
                        isPrime = false;
                        break;
                    }

                }

                if (isPrime && currentNumber != 1)
                {
                    // System.Diagnostics.Debug.WriteLine(currentNumber);
                    primes.Add(currentNumber);
                }
            }

            primes.Sort();
            return primes;
        }

        public List<long> generatePrimesInParaller(long fromNumber, long toNumber)
        {
            List<long> primes = new List<long>();

            Parallel.ForEach(getNumberRange(fromNumber, toNumber), (currentNumber) =>
            {
                bool isPrime = true;
                long division = currentNumber / 2;
                for (long i = 2; i <= division; i++)
                {
                    if (currentNumber % i == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime && currentNumber != 1)
                {
                    //System.Diagnostics.Debug.WriteLine(currentNumber);
                    primes.Add(currentNumber);
                }
            });

            primes.Sort();
            return primes;
        }

        public IEnumerable<long> getNumberRange(long fromNumber, long toNumber)
        {
            long number = fromNumber;

            while (number < toNumber)
            {
                number++;
                yield return number;
            }
        }

    }
}



////////////////////////// Compulsory questions /////////////////////////////
/// 3) a. GetPrimesSequential time: 2:58    GetPrimesParallel time: 1:18 ////
