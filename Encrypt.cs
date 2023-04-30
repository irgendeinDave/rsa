
using System.Numerics;

public static class Encrypt
{
    public static int encryptMessage(BigInteger n, BigInteger e, int m)
    {
        BigInteger c = BigInteger.ModPow(new BigInteger(m), e, n);
        return (int)c;   
    }
}