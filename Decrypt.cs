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
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(n / 10, 5 * (n / 10));
        BigInteger start = startSequence.First();
        Console.WriteLine("N: " + n);

        //die nächsten möglichkeiten durchlaufen, bis ein Wert die Bedingung ggt(e, phi(n)) = 1 erfüllt
        while (Euclid.GreatestCommonDivisor(start, phi()) != 1)
        {
            Console.WriteLine(start);
            ++start;
        }

        Console.WriteLine("E: " + start);
        return start;
    }
}