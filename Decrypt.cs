using System.Data;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using MathNet.Numerics;
using MathNet.Numerics.Random;
using PrimeNumberGenerator;

 class Decrypt
{
    private BigInteger p, q;
    private BigInteger n;
    private BigInteger e;
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
        Console.WriteLine($"E valid: {Euclid.GreatestCommonDivisor(e, phi()) == 1 && e < phi() && PrimeGenerator.isPrime(e)}");
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
        // zufälliger Startwert zwischen 1/10 n und 5/10 n
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(n / 10, 5 * (n / 10));
        BigInteger start = startSequence.First();

        //die nächsten Möglichkeiten durchlaufen, bis ein Wert die Bedingung ggt(e, phi(n)) = 1 erfüllt
        while (true)
        {
            if (Euclid.GreatestCommonDivisor(start, phi()) == 1 && PrimeGenerator.isPrime(start))                      
                return start;           
            ++start;
        }
    }

    private BigInteger findD()
    {
        // Ähnliche Vorgehensweise wie in findE(), aber mit einer anderen Bedingung
        Random r = new Random();
        IEnumerable<BigInteger> startSequence = r.NextBigIntegerSequence(n / 100, n / 10);
        BigInteger start = startSequence.First();

        while (start * e % phi() != 1)
        {
            ++start;
        }
        return start;

    }
}