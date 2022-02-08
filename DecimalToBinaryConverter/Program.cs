using System.Diagnostics.CodeAnalysis;

namespace DecimalToBinaryConverter;

public class Program
{
    static void Main(String[] args)
    {
        Welcome();
        PrintMenu();
        MakeChoice();
        Console.WriteLine("Thank you for using my program");
    }


    public static void PrintMenu()
    {
        Console.WriteLine("Co chcesz zrobić ?: ");
        Console.WriteLine("1. Użyć kalkulatora");
        Console.WriteLine("2. Przekonwertować liczbę na ZM,U1,U2");
    }
    [SuppressMessage("ReSharper.DPA", "DPA0001: Memory allocation issues")]
    public static void MakeChoice()
    {
        Console.Write("Twój wybór: ");
        int choice = int.Parse(Console.ReadLine());
        char repeat;
        switch (choice)
        {
            case 1:
                do
                {
                    Console.Write("Wpisz pierwszą liczbę: ");
                    var firstNumber = double.Parse(Console.ReadLine());
                    Console.Write("Wpisz drugą liczbę: ");
                    var secondNumber = double.Parse(Console.ReadLine());
                    
                    var firstNumberAbs = Math.Abs(firstNumber);
                    var secondNumberAbs = Math.Abs(secondNumber);
                    var firstNumberInBinary = decimalToBinary(firstNumber, 10000).TrimEnd('0');
                    var firstNumberAbsInBinary = decimalToBinary(firstNumberAbs, 10000).TrimEnd('0');
                    var secondNumberAbsInBinary = decimalToBinary(secondNumberAbs, 10000).TrimEnd('0');
                    var secondNumberInBinary = decimalToBinary(secondNumber, 10000).TrimEnd('0');
                    
                        if (firstNumber < 0)
                        {
                            Console.WriteLine($"{firstNumber} in binary is: -{firstNumberAbsInBinary}");

                        }

                        if (secondNumber < 0)
                        {
                            Console.WriteLine($"{secondNumber} in binary is: -{secondNumberAbsInBinary}");

                        }
                        if(firstNumber>0){
                            Console.WriteLine($"{firstNumber} in binary is: {firstNumberInBinary}");
                        }

                        if (secondNumber > 0)
                        {
                            Console.WriteLine($"{secondNumber} in binary is: {secondNumberInBinary}");
                        }

                        MakeOperations(firstNumber, secondNumber);
                    
                    Console.Write("\nDo you want to repeat ? [y,Y/n,N]: ");
                    repeat = char.Parse(Console.ReadLine());
                    
                } while (repeat is 'y' or 'Y' or 't' or 'T');

                break;
            case 2:
                do
                {
                    Console.Write("Wpisz liczbę, którą chcesz przekonwertować: ");

                    int liczba = int.Parse(Console.ReadLine());

                    ConvertDecimalToZmU1U2(liczba);

                    Console.Write("\nDo you want to repeat ? [y,Y/n,N]: ");
                    repeat = char.Parse(Console.ReadLine());
                } while (repeat is 'y' or 'Y' or 't' or 'T');
                
                Console.WriteLine();
                PrintMenu();
                MakeChoice();

                break;
        }
    }
    public static void PrintStars()
    {
        Console.WriteLine("*************************************");
    }
    public static void Welcome()
    {
        PrintStars();
        Console.WriteLine("DECIMAL TO BINARY CONVERTER ");
        PrintStars();
        Console.WriteLine();
    }
    public static void MakeOperations(double num1, double num2)
    {
        Console.Write("Please choose the operator [+,-,*,/]: ");
        var userOperator = Console.ReadLine();

        if (num1 < 0 || num2 < 0)
        {
            var num1Abs = Math.Abs(num1);
            var num2Abs = Math.Abs(num2);

            if (userOperator == "+")
            {
                Console.WriteLine($"{num1} + {num2} in binary is: {PerformAddition(num1, num2)}");
            }
            else if (userOperator == "-")
            {
                Console.WriteLine($"{num1} - {num2} in binary is: {PerformSubtraction(num1, num2)}");
            }
            else if (userOperator == "*")
            {
                Console.WriteLine($"{num1} * {num2} in binary is : {PerformMultiplication(num1, num2)}");
            }
            else if (userOperator == "/")
            {
                Console.WriteLine($"{num1} / {num2} in binary is: {PerformDivision(num1, num2)}");
            }
        }
    }
    public static string Array2String<T>(IEnumerable<T> list)
    {
        return string.Join("", list);
    }
    public static void ConvertDecimalToZmU1U2(int liczba)
    {
        var number = liczba;
        int negacja = 0;
        byte[] U2 = new byte[32];
        byte[] ZM = new byte[32];
        byte[] U1 = new byte[32];
            for(int x=0;x<32;x++)
        {
            U2[31-x] = (byte)(liczba>>x & 1);
        }
        if(liczba < 0)
        {
            ZM[0] = 1;
            liczba*=-1;
            negacja = 1;
        }
        int i = 0;
        while(liczba > 0)
        {
            ZM[31-i] = (byte)(liczba % 2);
            liczba/=2;
            i++;
        }
        for(i=1;i<32;i++)
        {
            U1[i] = (byte)Math.Abs(ZM[i] - negacja);
        }
        U1[0] = ZM[0];
        //dla kontroli
        if (number < 0)
        {
            Console.WriteLine("Binary: -" + decimalToBinary(Math.Abs(number), 10000).TrimEnd('0'));
        }

        if (number > 0)
        {
            Console.WriteLine("Binary: " + decimalToBinary(number, 10000).TrimEnd('0'));

        }
        Console.WriteLine("U2: "+Array2String(U2).TrimStart('0'));
        Console.WriteLine("ZM: "+Array2String(ZM).TrimStart('0'));
        Console.WriteLine("U1: "+Array2String(U1).TrimStart('0'));
        
    }
    static String decimalToBinary(double num, int k_prec)
    {
        String binary = "";
  
        // Fetch the integral part of decimal number
        int Integral = (int) num;
  
        // Fetch the fractional part decimal number
        double fractional = num - Integral;
  
        // Conversion of integral part to
        // binary equivalent
        while (Integral > 0)
        {
            int rem = Integral % 2;
  
            // Append 0 in binary
            binary += ((char)(rem + '0'));
  
            Integral /= 2;
        }
  
        // Reverse string to get original binary
        // equivalent
        binary = reverse(binary);
  
        // Append point before conversion of
        // fractional part
        binary += ('.');
  
        // Conversion of fractional part to
        // binary equivalent
        while (k_prec-- > 0)
        {
            // Find next bit in fraction
            fractional *= 2;
            int fract_bit = (int) fractional;
  
            if (fract_bit == 1)
            {
                fractional -= fract_bit;
                binary += (char)(1 + '0');
            }
            else
            {
                binary += (char)(0 + '0');
            }
        }
  
        return binary;
    }
    static String reverse(String input)
    {
        char[] temparray = input.ToCharArray();
        int left, right = 0;
        right = temparray.Length - 1;
  
        for (left = 0; left < right; left++, right--)
        {
            // Swap values of left and right
            char temp = temparray[left];
            temparray[left] = temparray[right];
            temparray[right] = temp;
        }
        return String.Join("",temparray);
    }
    static string PerformAddition(double num1, double num2)
    {

        double sum = num1 + num2;
        Console.WriteLine($"{num1} + {num2} = {sum}");
        string sumInBinary = " ";

        if (sum > 0)
        {
            sumInBinary = decimalToBinary(sum, 10000).TrimEnd('0');
            return sumInBinary;
        }

        if (sum < 0)
        {
            sum = Math.Abs(sum); 
            sumInBinary = "-"+decimalToBinary(sum, 10000).TrimEnd('0');
        }

        return sumInBinary;
    }
    public static string PerformMultiplication(double num1, double num2)
    {
        double multiplication = num1 * num2;
        Console.WriteLine($"{num1} * {num2} = {multiplication}");
        string multiplicationInBinary = " ";

        if (multiplication > 0)
        {
            multiplicationInBinary = decimalToBinary(multiplication, 10000).TrimEnd('0');
        }
        if (multiplication < 0)
        {
            multiplication = Math.Abs(multiplication);
            multiplicationInBinary = "-"+decimalToBinary(multiplication, 10000).TrimEnd('0');
        }
        return multiplicationInBinary;
    }
    public static string PerformDivision(double num1, double num2)
    {
        
        double division = num1 / num2;
        Console.WriteLine($"{num1} / {num2} = {division}");
        
        string divisionInBinary = "";


        if (division > 0)
        {
            divisionInBinary = decimalToBinary(division, 10000).TrimEnd('0');
            
        }

        if (division < 0)
        {
            division = Math.Abs(division);
            divisionInBinary = "-"+decimalToBinary(division, 10000).TrimEnd('0');
            
        }

        return divisionInBinary;
    }
    public static string PerformSubtraction(double num1, double num2)
    {
        double subtraction = num1-num2;
        Console.WriteLine($"{num1} - {num2} = {subtraction}");
        string subtractionInBinary = " ";

        if (subtraction > 0)
        {

            subtractionInBinary = decimalToBinary(subtraction, 10000).TrimEnd('0');
            
        }

        if (subtraction < 0)
        {
            subtraction = Math.Abs(subtraction);
            subtractionInBinary = "-"+decimalToBinary(subtraction, 10000).TrimEnd('0');

        }

        return subtractionInBinary;
    }

}
