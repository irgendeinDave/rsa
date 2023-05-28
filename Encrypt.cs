
using System.Numerics;

public static class Encrypt
{
    public static BigInteger encryptMessage(BigInteger n, BigInteger e, BigInteger m)
    {
        BigInteger c = BigInteger.ModPow(m, e, n);
        return c;
    }
}