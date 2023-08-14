
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.Random;

namespace PrimeNumberGenerator;


/// <summary> 
/// erzeuge Primzahlen für die RSA-Verschlüsselung 
/// </summary>
public static class PrimeGenerator
{
       
    /// <summary> 
    /// erzeuge Primzahl
    /// </summary>
    public static BigInteger genPrime()
    {
        // bestimme zufälligen startwert zwischen 1 Million und 100 Millionen
        Random r = new Random();
        BigInteger val = r.NextInt64(10_000, 1_000_000);
        // sicherstellen, dass val ungerade ist
        if (val % 2 == 0)
            ++val;

        while (true)        
        {
            if (isProbablyPrime(val))
                return val;
            val += 2;
        }   
    }
    
    private static readonly int repetitions = 10;
    /// <summary> prüfe, ob num eine Primzahl ist, indem 
    /// getestet wird, ob sie durch eine Zahl teilbar ist, 
    /// die kleiner ist als sie </summary>
    private static bool isProbablyPrime(BigInteger m)
    {
        if (m % 2 == 0)
            return false;
        
        int s = FindS(m);
        BigInteger d = FindD(m, s);
        
        for (int i = 0; i < repetitions; ++i)
        {
            Random rand = new CryptoRandomSource();
            var a  = rand.NextBigIntegerSequence(BigInteger.Parse("1000000000"), BigInteger.Parse("1000000000000")).First();

            BigInteger x = BigInteger.ModPow(a, d, m);
            if(x == 1 || x == m - 1)
                continue;
            for (int r = 0; r < s; r++)
            {
                x = BigInteger.ModPow(x, 2, m);
                if(x == 1)
                    return false;
                if(x == m - 1)
                    break;
            }
            if(x != m - 1)
                return false;
        }
        return true;
    }

    // private static bool RunMillerRabinIteration(BigInteger m)
    // {
    //     int s = FindS(m);
    //     BigInteger d = FindD(m, s);
    //
    //     Random r = new CryptoRandomSource();
    //     BigInteger a = r.NextBigIntegerSequence(BigInteger.Parse("1000000000"), BigInteger.Parse("1000000000000")).First();
    //
    //     BigInteger x = BigInteger.ModPow(a, d, m);
    //     
    //     if (x == 1 || x == m - 1)
    //         return false;
    //     
    //     
    // }

    private static int FindS(BigInteger m)
    {
        BigInteger d = m - 1;
        int s = 0;
        while (d % 2 == 0)
        {
            d /= 2;
            s++;
        }

        return s;
    }

    private static BigInteger FindD(BigInteger m, int s)
    {
        return (m - 1) / Euclid.PowerOfTwo(s);
    }
}