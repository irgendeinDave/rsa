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
            // wenn argumente übergeben wurden, combiniere sie und nutze sie als input
            String combinedArgs = "";
            for (int i = 0; i < args.Length; i++)
            {
                combinedArgs += args[i] + " ";
            }
            input = combinedArgs;
            
        }
        // wenn keine Argumente gegeben wurden, fordere den Klartext vom Benutzer an
        else 
            input = promtInput();

        Console.WriteLine($"Ecrypting message \"{input}\"");

        Decrypt d = new Decrypt();
    }
}