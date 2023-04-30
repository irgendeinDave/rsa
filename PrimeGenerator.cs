
using System.Numerics;
using MathNet.Numerics;

namespace PrimeNumberGenerator;


/// <summary> 
/// erzeuge Primzahlen für die RSA-Verschlüsselung 
/// </summary>
public static class PrimeGenerator
{
       
    /// <summary> 
    /// erzeuge 32 bit Primzahl
    /// </summary>
    public static BigInteger genPrime()
    {
        // bestimme zufälligen startwert zwischen 1 Million und 100 Millionen
        Random r = new Random();
        BigInteger val = r.NextInt64(100_000, 10_000_000);
        // sicherstellen, dass val ungerade ist
        if (val % 2 == 0)
            ++val;

        while (true)        
        {
            if (isPrime(val))
                return val;
            val += 2;
        }   
    }

    /// <summary> prüfe, ob num eine Primzahl ist, indem 
    /// getestet wird, ob sie durch eine Zahl teilbar ist, 
    /// die kleiner ist als sie </summary>
    public static bool isPrime(BigInteger num)
    {
        Console.WriteLine("Testing primality of " + num);
        if (num % 2 == 0)
            return false;
        
        for (BigInteger i = 3; i < (num - 1); i += 2)
        {
            if (num % i == 0)
                return false;
        }
        Console.WriteLine("…Done");
        return true;
    } 
}