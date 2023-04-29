using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using PrimeNumberGenerator;

 class Decrypt
{
    private BigInteger p, q;
    private BigInteger n;
    BigInteger e;
    public Decrypt()
    {
        p = PrimeGenerator.genPrime();
        q = PrimeGenerator.genPrime();
        n = p * q;
        e = findE();
    }

    private BigInteger phi()
    {
        return (p - 1) * (q - 1);
    }

    private BigInteger findE()
    {
        // zufälliger Startwert zwischen 1/10 n und 5/10 n
        var divRem = BigInteger.DivRem(n, new BigInteger(10));
        BigInteger oneTenth = divRem.Quotient;
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(n / 10, 5 * (n / 10));
        BigInteger start = 0;
        foreach (var val in startSequence)
        {
            start = val;
            break; // nur der erste Wert wird benutzt
        }
        while (Euclid.GreatestCommonDivisor(start, phi()) != 1)
        {
            ++start;
        }

        Console.WriteLine("E: " + start);
        return start;
    }
}