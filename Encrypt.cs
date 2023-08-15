
using System.Numerics;

public static class Encrypt
{
    /// <summary>
    /// Verschlüssele die Nachricht m
    /// </summary>
    /// <param name="n">Teil "n" des public key</param>
    /// <param name="e">Teil "e" des Public key</param>
    /// <param name="m">Die zu verschlüsselnde Nachricht m als BigInteger Zahlenwert</param>
    /// <returns>Den verschlüsselten Zahlenwert</returns>
    public static BigInteger encryptMessage(BigInteger n, BigInteger e, BigInteger m)
    {
        BigInteger c = BigInteger.ModPow(m, e, n);
        return c;
    }
}