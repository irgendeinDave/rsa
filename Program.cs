using PrimeNumberGenerator;
using System.Numerics;
using System.Text;

public class Program
{
    private static String? input;

    /// <summary> 
    /// frage Benutzer nach Eingabe von Text
    /// </summary>
    private static String promtInput()
    {
        Console.Write("Please enter your message: ");
        String? prompt = Console.ReadLine();
        while (prompt == null)
            prompt = Console.ReadLine();
        return prompt;
    }

    public static void Main(String[] args)
    {
        // Bestimme, ob entschlüsselt oder verschlüsselt werden soll
        if (args.Length > 0)
            input = args[0];
        else // wenn keine Argumente gegeben wurden, fordere den Klartext vom Benutzer an
            input = promtInput();

        if (input == "encrypt" || input == "e")
        {
            while (true)
            {
                // Eingaben anfordern
                Console.WriteLine("Geben Sie n an: ");
                BigInteger n = BigInteger.Parse(Console.ReadLine());
                Console.WriteLine("Geben Sie e an: ");
                BigInteger e = BigInteger.Parse(Console.ReadLine());
                Console.WriteLine("Geben Sie die zu verschlüsselnde Nachricht an: ");
                string? message = Console.ReadLine();

                // Codieren der Nachricht
                byte[] encoded = textToBytes(message);
                foreach (var encodedLetter in encoded)
                {
                    // Verschlüsseln der Nachricht
                    Console.Write($"{Encrypt.encryptMessage(n, e, encodedLetter)} ");
                }
                Console.Write(new String('\n', 3));
            }
        }
        if (input == "decrypt" || input == "d")
        {
            // Schlüsselgenerierung
            Decrypt decrypt = new Decrypt();
            while (true)
            {
                // Eingabe anfordern und in byte[] umwandeln
                Console.WriteLine("Geben Sie den zu entschlüsselnden Text an: ");
                string? decryptInput = Console.ReadLine();
                if (string.IsNullOrEmpty(decryptInput))
                    continue;
                string[] split = decryptInput.Split(' ');
                byte[] bytes = new byte[split.Length];
                // entschlüsseln
                for (int i = 0; i < split.Length; ++i)
                {
                    if (string.IsNullOrEmpty(split[i]))
                        continue;
                    
                    byte decrypted = decrypt.decryptMessage(BigInteger.Parse(split[i]));
                    bytes[i] = decrypted;
                }
                Console.WriteLine($"Ihr klartext lauted: {bytesToText(bytes)}");
                Console.Write(new String('\n', 3));
            }
        } // ungültiges Argument
        Console.WriteLine("Bitte \"encrypt\" oder \"decrypt\" als erstes Argument angeben!");
    }

    #region UTF-8 Codierung
    // Codierung des Klartextes zu Byte-Array mithilfe der C#-Standardbibliothek
    private static byte[] textToBytes(string? inputText)
    {
        if (inputText == null)
            return new byte[0];

        var utf8 = Encoding.UTF8;
        byte[] encoded = utf8.GetBytes(inputText);
        return encoded;
    }

    // Codierung des als Byte-Array gegebenen wieder entschlüsselten Textes zu Klartext mithilfe der C#-Standardbibliothek
    private static string bytesToText(byte[] bytes)
    {
        var utf8 = Encoding.UTF8;
        return utf8.GetString(bytes);
    }
    #endregion
}