using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using PrimeNumberGenerator;

public class Decrypt
{
    private BigInteger p, q;
    private BigInteger n;
    private BigInteger e;
    private BigInteger d;
    public Decrypt()
    {
        // Generiere Primzahlen p und q
        p = PrimeGenerator.genPrime();
        Console.WriteLine("p found: " + p);
        q = PrimeGenerator.genPrime();
        Console.WriteLine("q found: " + q);

        n = p * q;
        e = findE();
        d = findD();
        Console.WriteLine($"d: {d}");
    
        // Debug ausgaben zur Überprüfung, ob E und D gültig sind
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
        // zufälliger Startwert zwischen 1 und 99/100 phi(n)
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(1, 99 * (phi() / 100));
        BigInteger start = startSequence.First();

        //die nächsten Möglichkeiten durchlaufen, bis ein Wert Teilerfremd zu Phi ist
        while (Euclid.GreatestCommonDivisor(start, phi()) != 1)
        {
            ++start;
        }
        return start;
    }

    // Das multiplikative inverse d von e und phi (den Privatschlüssel) mithilfe des erweiterten euklidischen Algorithmus finden
    private BigInteger findD()
    {
        // führe erweiterten euklidischen Algorithmus durch
        BigInteger gcd = Euclid.ExtendedGreatestCommonDivisor(e, phi(), out BigInteger x, out BigInteger y);
        
        // modulares multiplikatives Inverses nur verfügbar, wenn e und phi teilerfremd sind
        if (gcd != 1)
            throw new Exception("Kein Wert für d möglich!");

        // das modulare multiplikative Inverse ergibt sich aus dem erweiterten euklidischen Algorithmus
        // durch die Formel d = (x % phi + phi) % phi
        // Das Phi in der Klammer wird dazu addiert, damit das Ergebnis auch für Zahlen funktioniert
        BigInteger inv = (x % phi() + phi()) % phi();
        return inv;
    }

    /// <summary>
    /// Entschlüssele den Geheimtext c mit der Formel m = c^d mod m
    /// </summary>
    /// <param name="c">Der geheimtext c</param>
    /// <returns>den Klartext m</returns>
    public byte decryptMessage(BigInteger c)
    {
        BigInteger m = BigInteger.ModPow(c, d, n);
        return (byte)m;
    }
}