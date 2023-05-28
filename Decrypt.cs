using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using PrimeNumberGenerator;

public class Decrypt
{
    private BigInteger p, q;
    public BigInteger n;
    public BigInteger e;
    private BigInteger d;
    public Decrypt()
    {
        p = PrimeGenerator.genPrime();
        Console.WriteLine("p found");
        q = PrimeGenerator.genPrime();
        Console.WriteLine("q found");

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
        // zufälliger Startwert zwischen 1 und 99/100 n
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(1, 99 * (n / 100));
        BigInteger start = startSequence.First();

        //die nächsten Möglichkeiten durchlaufen, bis ein Wert die Bedingung ggt(e, phi(n)) = 1 erfüllt
        while (Euclid.GreatestCommonDivisor(start, phi()) != 1)
        {
            ++start;
        }
        return start;
    }

    // d mithilfe des erweiterten euklidischen Algorithmus finden
    private BigInteger findD()
    {
        BigInteger x, y;
        var gcd = Euclid.ExtendedGreatestCommonDivisor(e, phi(), out x, out y);
        if (gcd != 1)
            throw new Exception("Error: d does not exist!");

        var res = (x % phi()) % phi();
        return res;
    }

    public byte decryptMessage(BigInteger c)
    {
        BigInteger m = BigInteger.ModPow(c, d, n);
        return (byte)m;
    }
}