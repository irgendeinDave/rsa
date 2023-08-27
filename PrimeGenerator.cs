
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
        // bestimme zufälligen Startwert 
        Random r = new CryptoRandomSource();
        BigInteger val = r.NextBigIntegerSequence(BigInteger.Parse("10000000000000000000000"), BigInteger.Parse("1000000000000000000000000000")).First();
        // sicherstellen, dass val ungerade ist
        if (val % 2 == 0)
            ++val;

        while (true)        
        {
            if (IsProbablyPrime(val))
                return val;
            val += 2;
        }   
    }
    
    private static readonly int repetitions = 10;
    /// <summary>
    /// Führe den Miller-Rabin Test durch, um die Primalität einer Zahl zu prüfen
    /// </summary>
    /// <param name="m">Die zu prüfende Zahl</param>
    /// <returns>
    /// True, wenn m mit sehr hoher Wahrscheinlichkeit eine Primzahl ist
    /// False, wenn m mit Sicherheit keine Primzahl ist
    /// </returns>
    private static bool IsProbablyPrime(BigInteger m)
    {
        // es dürfen nur ungerade Zahlen getestet werden
        if (m % 2 == 0)
            return false;
        
        // Funktionsaufrufe, um s und d zu bestimmen
        int s = FindS(m);
        BigInteger d = FindD(m, s);
        
        // führe den Test mit mehreren Zeugen durch
        for (int i = 0; i < repetitions; ++i)
        {
            // generiere zufälligen Zeugen a
            Random rand = new CryptoRandomSource();
            var a  = rand.NextBigIntegerSequence(BigInteger.Parse("1000000000"), BigInteger.Parse("1000000000000")).First();
            
            // setze Startwert für x zu a^d mod m
            BigInteger x = BigInteger.ModPow(a, d, m);
            if(x == 1 || x == m - 1)
                continue;
            
            // führe s mal die Berechnung x = x² mod m durch
            for (int r = 0; r < s; ++r)
            {
                x = BigInteger.ModPow(x, 2, m);
                // wenn bei irgendeinem Zeugen x = 1, ist m keine Primzahl
                if(x == 1)
                    return false;
                // wenn x == m - 1, prüfe den nächsten Zeugen
                if(x == m - 1)
                    break;
            }
            // Wenn nach Schleife x nicht m - 1 ist, ist m keine Primzahl
            if(x != m - 1)
                return false;
        }
        // wenn nach allen Iterationen nicht festgestellt wurde, dass m die Bedingungen für eine Primzahl nicht erfüllt, ist m wohl eine Primzahl
        return true;
    }
    
    /// <summary>
    /// Finde den s, also die Anzahl, wie oft m ohne Rest durch zwei teilbar ist
    /// </summary>
    /// <param name="m">Die zu prüfende Zahl m</param>
    /// <returns>s</returns>
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

    /// <summary>
    /// Wende die Formel d = (m - 1) / 2^s an, um d zu bestimmen
    /// </summary>
    /// <param name="m">Die zu prüfende Zahl m</param>
    /// <param name="s">Der zuvor bestimmete Wert für s</param>
    /// <returns>d</returns>
    private static BigInteger FindD(BigInteger m, int s)
    {
        return (m - 1) / Euclid.PowerOfTwo(s);
    }
}