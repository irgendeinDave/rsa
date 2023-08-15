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
        do
        {
            p = PrimeGenerator.genPrime();
            Console.WriteLine("p found: " + p);
            q = PrimeGenerator.genPrime();
            Console.WriteLine("q found:" + q);

            n = p * q;
            e = findE();
            d = findD();
            Console.WriteLine($"d: {d}");
            
        } while (!(e < phi() && (d * e) % phi() == 1)); // findD() gibt manchmal einen negativen Wert zurück, wodurch die 
        // Verschlüsselung fehlschägt. Deshalb wiederholen wir den Vorgang so lange, bis ein gültiger Schlüssel erzeugt wurde

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

        //die nächsten Möglichkeiten durchlaufen, bis ein Wert Teilerfremd zu Phi ist
        while (Euclid.GreatestCommonDivisor(start, phi()) != 1)
        {
            ++start;
        }
        return start;
    }

    // d mithilfe des erweiterten euklidischen Algorithmus finden
    private BigInteger findD()
    {
        BigInteger gcd = Euclid.ExtendedGreatestCommonDivisor(e, phi(), out BigInteger x, out BigInteger y);
        if (gcd != 1)
            throw new Exception("Kein Wert für d möglich!");

        BigInteger res = (x % phi()) % phi();
        return res;
    }

    public byte decryptMessage(BigInteger c)
    {
        BigInteger m = BigInteger.ModPow(c, d, n);
        return (byte)m;
    }
}