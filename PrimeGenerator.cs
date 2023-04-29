using MathNet.Numerics;
using System.Numerics;
using System.Reflection.Metadata;

/// <summary> 
/// erzeuge Primzahlen für die RSA-Verschlüsselung mithilfe von Mill's Konstante
/// </summary>
public static class PrimeGenerator
{
    private static decimal millsConstant = 1.3063778838630806904686144926m;
       
    /// <summary> 
    /// erzeuge 32 bit Primzahl
    /// </summary>
    public static int genPrime()
    {
        if (isPrime(uint.MaxValue / 4))
            Console.WriteLine("is prime");
        else 
        Console.WriteLine("is not prime");
        return 0;
    }

    private static bool isPrime(BigInteger num)
    {
        if (num % 2 == 0)
            return false;
        
        for (BigInteger i = 3; i < (num - 1); i += 2)
        {
            if (num % i == 0)
                return false;
        }
        return true;
        
    }
    
    
}