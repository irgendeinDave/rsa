using System.Numerics;
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

    private BigInteger findD()
    {
        Console.WriteLine("Generating D");
        // Ähnliche Vorgehensweise wie in findE(), aber mit einer anderen Bedingung
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(n / 100, n / 10);
        BigInteger start = startSequence.First();

        while (start * e % phi() != 1)
        {
            ++start;
        }
        Console.WriteLine("…Done");
        return start;

    }

    public int decryptMessage(int c)
    {
        BigInteger m = BigInteger.ModPow(new BigInteger(c), d, n);
        return (int)m;
    }
    
}