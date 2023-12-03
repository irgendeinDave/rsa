using System.Numerics;
using System.Text;

public class Program
{
    private static String? input;

    /// <summary> 
    /// frage Benutzer nach Eingabe von Text
    /// </summary>
    private static String PromtInput()
    {
        Console.Write("Please enter your message: ");
        String? prompt = Console.ReadLine();
        while (String.IsNullOrEmpty(prompt))
            prompt = Console.ReadLine();
        return prompt;
    }

    public static void Main(String[] args)
    {
        // Bestimme, ob entschluesselt oder verschluesselt werden soll
        if (args.Length > 0)
            input = args[0];
        else // wenn keine Argumente gegeben wurden, fordere den Benutzer auf, einen Modus anzugeben
            input = PromtInput();

        if (input == "encrypt" || input == "e")
        {
            while (true)
            {
                // Eingaben anfordern
                Console.WriteLine("Geben Sie n an: ");
                BigInteger n = BigInteger.Parse(Console.ReadLine()!);
                Console.WriteLine("Geben Sie e an: ");
                BigInteger e = BigInteger.Parse(Console.ReadLine()!);
                Console.WriteLine("Geben Sie die zu verschluesselnde Nachricht an: ");
                string? message = Console.ReadLine();

                // Codieren der Nachricht
                byte[] encoded = TextToBytes(message);
                foreach (var encodedLetter in encoded)
                {
                    // Verschluesseln der Nachricht
                    Console.Write($"{Encrypt.EncryptMessage(n, e, encodedLetter)} ");
                }
                Console.Write(new String('\n', 3));
            }
        }
        if (input == "decrypt" || input == "d")
        {
            // Schluesselgenerierung
            Decrypt decrypt = new Decrypt();
            while (true)
            {
                // Eingabe anfordern und in byte[] umwandeln
                Console.WriteLine("Geben Sie den zu entschluesselnden Text an: ");
                string? decryptInput = Console.ReadLine();
                if (string.IsNullOrEmpty(decryptInput))
                    continue;
                string[] split = decryptInput.Split(' ');
                byte[] bytes = new byte[split.Length];
                // entschluesseln
                for (int i = 0; i < split.Length; ++i)
                {
                    if (string.IsNullOrEmpty(split[i]))
                        continue;
                    
                    byte decrypted = decrypt.DecryptMessage(BigInteger.Parse(split[i]));
                    bytes[i] = decrypted;
                }
                Console.WriteLine($"Ihr Klartext lautet: {BytesToText(bytes)}");
                Console.Write(new String('\n', 3));
            }
        } // ungueltiges Argument
        Console.WriteLine("Bitte \"encrypt\" oder \"decrypt\" als erstes Argument angeben!");
    }

    #region UTF-8 Codierung
    // Codierung des Klartextes zu Byte-Array mithilfe der C#-Standardbibliothek
    private static byte[] TextToBytes(string? inputText)
    {
        if (inputText == null)
            return Array.Empty<byte>();

        var utf8 = Encoding.UTF8;
        byte[] encoded = utf8.GetBytes(inputText);
        return encoded;
    }

    // Codierung des als Byte-Array gegebenen wieder entschluesselten Textes zu Klartext mithilfe der C#-Standardbibliothek
    private static string BytesToText(byte[] bytes)
    {
        var utf8 = Encoding.UTF8;
        return utf8.GetString(bytes);
    }
    #endregion
}