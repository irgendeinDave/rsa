
/// <summary>
/// Eine Klasse, die unbegrenzt große Zahlen speichern und mit ihnen rechnen kann
/// kann auch mit Dezimalzahlen arbeiten
/// </summary>
public class BigNum
{
    // Der als String gespeicherte Wert der Zahl
    private string number = new string("");

    // get-Methode für number
    public string Number {
        get {return number;}
    }

    private bool isValid(string val)
    {
        int numberOfPeriods = 0;
        // sicherstellen, dass nur Zahlen eingegeben werden
        for (int i = 0; i < val.Length; i++)
        {
            // negative Zahlen erlauben
            if (i == 0 && val[i] == '-')
                continue;

            if (val[i] == '.') 
                if (numberOfPeriods == 1)
                {
                    Console.WriteLine($"{val} ist keine gültige Zahl! Bitte nur einen Punkt verwenden! Der Wert 0 wird stattdessen verwendet!");
                    return false;
                }
                else 
                {
                    numberOfPeriods++;
                    continue;
                }

            // Versuche, den Wert zu einer Zahl umzuwandeln
            // Wert kann nicht umgewandelt werden, wenn die Methode TryParse false ist
            int parsedNumber;
            if (!int.TryParse(val[i].ToString(), out parsedNumber)) 
            {
                Console.WriteLine($"{val} ist keine gültige Zahl! Der Wert 0 wird stattdessen verwendet!");
                return false;
            }
        }
        return true;
    }

    #region Konstruktoren
    public BigNum(string _value) => number =  isValid(_value) ? new string(_value) : new string("0");   

    public BigNum(double _value) => number = new string(_value.ToString());

    public BigNum(int _value) => number = new string(_value.ToString());
    #endregion

    private bool isNegative(BigNum n)
    {
        foreach (char digit in n.Number)
        {
            if (digit != '0')
                if (digit == '-')
                    return true;
                else 
                    return false;
        }
        return false; // default Wert, tritt nur auf wenn die Zahl nur aus Nullen besteht oder leeer ist
    }
    public void add(BigNum toAdd) 
    {   
        string thisNumber = number;
        string addNumber = toAdd.Number;
        //wenn ganze Zahlen, hinten einen Punkt anfügen, damit der Code unten funktioniert
        if (thisNumber.Split('.').Length == 1)
            thisNumber += ".";
        if (addNumber.Split('.').Length == 1)
            addNumber += ".";

        // beide Strings auf die gleiche Größe bringen
        ExtendStringsToSameLength(ref thisNumber, ref addNumber);
        Console.WriteLine(thisNumber);
        Console.WriteLine(addNumber);

        // "schriftliches addieren" 
        string result = "";
        int toNextDigit = 0; // die "gemerkte Zahl"
        for (int i = thisNumber.Length - 1; i >= 0; i--)
        {
            if (thisNumber[i] == '.')
            {
                result = '.' + result;
                continue;
            }
            
            int sum = int.Parse(thisNumber[i].ToString()) + int.Parse(addNumber[i].ToString()) + toNextDigit;
            
            if (sum > 9)
            {
                toNextDigit = 1;  
                result = (sum - 10).ToString() + result;              
            }
            else 
            {
                toNextDigit = 0;
                result = sum.ToString() + result;
            }
        }
        number = result;
        Console.WriteLine($"result: {result}");     
    }

    public void subtract(BigNum toSubtract)
    {
        string thisNumber = number;
        string subNumber = toSubtract.Number;
        //wenn ganze Zahlen, hinten einen Punkt anfügen, damit der Code unten funktioniert
        if (thisNumber.Split('.').Length == 1)
            thisNumber += ".";
        if (subNumber.Split('.').Length == 1)
            subNumber += ".";

        // beide Strings auf die gleiche Größe bringen
        ExtendStringsToSameLength(ref thisNumber, ref subNumber);

        // "schriftliches subtrahieren" 
        string result = "";
        int toNextDigit = 0; // die "gemerkte Zahl"
        for (int i = thisNumber.Length - 1; i >= 0; i--)
        {
            if (thisNumber[i] == '.')
            {
                result = '.' + result;
                continue;
            }
            
            int sum = int.Parse(thisNumber[i].ToString()) - int.Parse(subNumber[i].ToString()) + toNextDigit;
            
            if (sum > 9)
            {
                toNextDigit = 1;  
                result = (sum - 10).ToString() + result;              
            }
            else 
            {
                toNextDigit = 0;
                result = sum.ToString() + result;
            }
        }
        number = result;
        Console.WriteLine($"result: {result}");
    }

    private static void ExtendStringsToSameLength(ref string str1, ref string str2)
    {
        // teile die Strings am Punkt
        string[] str1Parts = str1.Split('.');
        string[] str2Parts = str2.Split('.');

        string str1Int = str1Parts[0];
        string str2Int = str2Parts[0];
        int str1FracLen = str1Parts.Length > 1 ? str1Parts[1].Length : 0;
        int str2FracLen = str2Parts.Length > 1 ? str2Parts[1].Length : 0;

        // fülle die einzelnen Teile mit Nullen auf
        int maxLength = Math.Max(str1Int.Length, str2Int.Length);
        str1Int = str1Int.PadLeft(maxLength, '0');
        str2Int = str2Int.PadLeft(maxLength, '0');

        // Füge die Strings wieder zusammen
        str1 = str1Int + "." + (str1Parts.Length > 1 ? str1Parts[1].PadRight(maxLength - str1FracLen, '0') : string.Empty);
        str2 = str2Int + "." + (str2Parts.Length > 1 ? str2Parts[1].PadRight(maxLength - str2FracLen, '0') : string.Empty);
    }
}
