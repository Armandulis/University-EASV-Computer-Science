using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeAPI2.Interfaces
{
    public interface IPrimesRepository
    {
        int GetPrimesSum(int primes);

        bool IsPrime(int number);
    }
}
