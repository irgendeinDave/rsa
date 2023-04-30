using System.Numerics; //BigInteger
using PrimeNumberGenerator;

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

        Console.WriteLine($"Ecrypting message \"{input}\"");

        Decrypt decrypt = new Decrypt();
        int inputNumber;
        int encryptedMessage;
        if (int.TryParse(input, out inputNumber))
        {
            encryptedMessage = Encrypt.encryptMessage(decrypt.n, decrypt.e, inputNumber);
            Console.WriteLine($"Verschlüsselte Nachricht: {encryptedMessage}");
            Console.WriteLine($"Entschlüsselte Nachricht: {decrypt.decryptMessage(encryptedMessage)}");
        }    
    }
}