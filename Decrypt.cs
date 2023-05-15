using System.Numerics;
using System.Reflection;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using PrimeNumberGenerator;

class Decrypt
{
    private BigInteger p, q;
    public BigInteger n;
    public BigInteger e;
    private BigInteger d;
    public Decrypt()
    {
        // p = PrimeGenerator.genPrime();
        // Console.WriteLine("p found");
        // q = PrimeGenerator.genPrime();
        // Console.WriteLine("q found");
        p = new BigInteger(353);
        q = new BigInteger(149);

        n = p * q;
        e = findE();
        d = findD();
        Console.WriteLine($"E valid: {Euclid.GreatestCommonDivisor(e, phi()) == 1 && e < phi()}");
        Console.WriteLine($"D valid: {e < phi() && (d * e) % phi() == 1}");

        // öffentlichen Schlüssel bekanntgeben
        Console.WriteLine("\nÖffentlicher Schlüssel:\n" +
        $"n: {n}\n" +
        $"e: {e}\n" +
        "Text, der mit diesem Schlüssel verschlüsselt wurde, muss mit mit dieser Instanz des Programmes wieder entschlüsselt werden");

    }

    private BigInteger phi()
    {
        return (p - 1) * (q - 1);
    }

    private BigInteger findE()
    {
        Console.WriteLine("Generating E");
        // zufälliger Startwert zwischen 1 und 99/100 n
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(1, 99 * (n / 100));
        BigInteger start = startSequence.First();

        //die nächsten Möglichkeiten durchlaufen, bis ein Wert die Bedingung ggt(e, phi(n)) = 1 erfüllt
        while (Euclid.GreatestCommonDivisor(start, phi()) != 1)
        {
            ++start;
        }
        Console.WriteLine("…Done");
        return start;
    }
    // multiplikatives Inverses e * d mod phi = 1
    private BigInteger findD()
    {
        // wenn ggT(e, phi()) == 1, gibt es kein multiplikatives Inverses und somit kein gültiges d
        /* if (Euclid.GreatestCommonDivisor(e, phi()) == 1)
            throw new Exception("Multiplicative inverse does not exist!"); */

        BigInteger result = BigInteger.ModPow(e, phi() - 1, n);
        Console.WriteLine("d:" + result);
        return result;
    }

    public byte decryptMessage(int c)
    {
        BigInteger m = BigInteger.ModPow(new BigInteger(c), d, n);
        return (byte)m;
    }

}