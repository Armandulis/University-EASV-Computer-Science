using PrimeAPI2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeAPI2.Repository
{
    public class PrimesRepo : IPrimesRepository
    {
        public int GetPrimesSum(int input)
        {
            Stack<int> digits = new Stack<int>();
            int primesSum = 0;

            // Extract digits from user's input
            while (input > 0)
            {
                var digit = input % 10;
                input /= 10;

                // Check if digit is a prime
                if (IsPrime(digit))
                {
                    // Add digit to the sum
                    primesSum += digit;
                }
            }

            // Return sum of primes in user's input
            return primesSum;
        }

        public bool IsPrime(int number)
        {
            int m = number / 2;
            for (int i = 2; i <= m; i++)
            {
                if (number % i == 0)
                {
                    // Number is not a prime
                    return false;
                }
            }


            // Number is a prime
            return true;
        }
    }
}
