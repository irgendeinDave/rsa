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
        q = PrimeGenerator.genPrime();

        n = p * q;
        e = findE();
        d = findD();
        Console.WriteLine($"d: {d}");
    
        // Debug ausgaben zur UEberpruefung, ob E und D gueltig sind
        // Console.WriteLine($"E valid: {Euclid.GreatestCommonDivisor(e, phi()) == 1 && e < phi()}");
        // Console.WriteLine($"D valid: {e < phi() && (d * e) % phi() == 1}");
        
        
        
        // oeffentlichen Schluessel bekanntgeben
        Console.WriteLine("\nOEffentlicher Schluessel:\n" +
        $"n: {n}\n" +
        $"e: {e}\n" +
        "Text, der mit diesem Schluessel verschluesselt wurde, muss mit mit dieser Instanz des Programmes wieder entschluesselt werden");
    }

    private BigInteger phi()
    {
        return (p - 1) * (q - 1);
    }

    private BigInteger findE()
    {
        // zufaelliger Startwert zwischen 1 und 99/100 phi(n)
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(1, 99 * (phi() / 100));
        BigInteger start = startSequence.First();

        //die naechsten Moeglichkeiten durchlaufen, bis ein Wert Teilerfremd zu Phi ist
        while (Euclid.GreatestCommonDivisor(start, phi()) != 1)
        {
            ++start;
        }
        return start;
    }

    // Das multiplikative inverse d von e und phi (den Privatschluessel) mithilfe des erweiterten euklidischen Algorithmus finden
    private BigInteger findD()
    {
        // fuehre erweiterten euklidischen Algorithmus durch
        BigInteger gcd = Euclid.ExtendedGreatestCommonDivisor(e, phi(), out BigInteger x, out BigInteger y);
        
        // modulares multiplikatives Inverses nur verfuegbar, wenn e und phi teilerfremd sind
        if (gcd != 1)
            throw new Exception("Kein Wert fuer d moeglich!");

        // das modulare multiplikative Inverse ergibt sich aus dem erweiterten euklidischen Algorithmus
        // durch die Formel d = (x % phi + phi) % phi
        // Das Phi in der Klammer wird dazu addiert, damit das Ergebnis auch fuer Zahlen funktioniert
        BigInteger inv = (x % phi() + phi()) % phi();
        return inv;
    }

    /// <summary>
    /// Entschluessele den Geheimtext c mit der Formel m = c^d mod m
    /// </summary>
    /// <param name="c">Der geheimtext c</param>
    /// <returns>den Klartext m</returns>
    public byte decryptMessage(BigInteger c)
    {
        BigInteger m = BigInteger.ModPow(c, d, n);
        return (byte)m;
    }
}