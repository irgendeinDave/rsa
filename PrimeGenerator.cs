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
        
        return 0;
    }

    private static bool isPrime(BigInteger num)
    {
        var start = (num - 1) % 2 == 0 ? num - 2 : num - 1;
        Console.WriteLine("Start: " + start);
        for (BigInteger i = start; i > 2; i -= 2)
        {
            if (num % i == 0)
                return false;
        }
        return true;
        
    }
    
    
}