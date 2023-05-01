using PrimeNumberGenerator;
using System.Numerics;
using System.Text;

public class Program
{
    private static String? input;

    ///<summary> 
    /// frage Benutzer nach Eingabe von Text
    /// </summary>
    private static String? promtInput()
    {
        Console.Write("Please enter your message: ");
        String? prompt = Console.ReadLine();
        while (prompt == null)
            prompt = Console.ReadLine();
        return prompt;
    }

    public static void Main(String[] args)
    {
        // Bestimme zu verschlüsselnde Nachricht
        if (args.Length > 0)
        {
            // wenn Argumente übergeben wurden, kombiniere sie und nutze sie als Input
            String combinedArgs = "";
            foreach (String arg in args)
            {
                combinedArgs += arg;
            }
            input = combinedArgs;
            
        }
        // wenn keine Argumente gegeben wurden, fordere den Klartext vom Benutzer an
        else 
            input = promtInput();

        // bestimmen, ob ver- oder entschlüsselt werden soll
        Console.WriteLine("Debug: " + input);
        if (args[0] == "encrypt")
        {
            // Eingaben anfordern
            Console.WriteLine("Geben sie n an: ");
            BigInteger n = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Geben Sie e an: ");
            BigInteger e = BigInteger.Parse(Console.ReadLine());
            Console.WriteLine("Geben Sie die zu verschlüsselnde Nachricht an: ");
            string? message = Console.ReadLine();
            
            // Codieren der Nachricht
            byte[] encoded = textToBytes(message);
            foreach (var encodedLetter in encoded)
            {
                Console.Write($"{Encrypt.encryptMessage(n, e, encodedLetter)} ");
            }

        }
        else if (args[0] == "decrypt")
        {
            // Schlüsselgenerierung
            Decrypt decrypt = new Decrypt();

            // Eingabe anfordern und in byte[] umwandeln
            Console.WriteLine("Geben Sie den zu entschlüsselnden Text an: ");
            string? decryptInput = Console.ReadLine();   
            if (decryptInput == null)
                return;
            string[] split = decryptInput.Split(' ');
            byte[] bytes = new byte[split.Length];
            // entschlüsseln
            for (int i = 0; i < split.Length; ++i)
            {
                byte decrypted = decrypt.decryptMessage(int.Parse(split[i]));
                Console.WriteLine(decrypted);
                bytes[i] = decrypted;
            }
            Console.WriteLine($"Ihr klartext lauted: {bytesToText(bytes)}");
            
        } 
        else Console.WriteLine("Bitte \"encrypt\" oder \"decrypt\" als erstes Argument angeben!");
    }

    private static byte[] textToBytes(string? inputText)
    {
        if (inputText == null)
            return new byte[0];
        
        var utf8 = Encoding.UTF8;
        byte[] encoded = utf8.GetBytes(inputText);
        return encoded;
    }

    private static string bytesToText(byte[] bytes)
    {    
        var utf8 = Encoding.UTF8;
        return utf8.GetString(bytes);
    }
}