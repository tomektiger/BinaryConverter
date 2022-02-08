using System.Text.RegularExpressions;

namespace DecimalToZmU1U2;

public class Program
{
    static void Main(String[] args)
    {

        Console.WriteLine("Co chcesz zrobić ?: ");
        Console.WriteLine("1. Użyć kalkulatora");
        Console.WriteLine("2. Przekonwertować liczbę na ZM,U1,U2");
        Console.Write("Twój wybór: ");
        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                Console.Write("Wpisz pierwszą liczbę: ");
                var firstNumber = double.Parse(Console.ReadLine());
                Console.Write("Wpisz drugą liczbę: ");
                var secondNumber = double.Parse(Console.ReadLine());

                var firstNumberInBinary = decimalToBinary(firstNumber, 100000).TrimEnd('0');
                var secondNumberInBinary = decimalToBinary(secondNumber, 100000).TrimEnd('0');

                Console.WriteLine($"{firstNumber} in binary is: {firstNumberInBinary}");
                Console.WriteLine($"{secondNumber} in binary is: {secondNumberInBinary}");
                Console.Write("Take the operator: [+,-,*,/]: ");
                

                break;
            case 2:
                char repeat;

                do
                {
                    Console.Write("Wpisz liczbę, którą chcesz przekonwertować: ");

                    int liczba = int.Parse(Console.ReadLine());

                    ConvertDecimalToZmU1U2(liczba);

                    Console.Write("\nDo you want to repeat ? [y,Y/n,N]: ");
                    repeat = char.Parse(Console.ReadLine());
                } while (repeat is 'y' or 'Y' or 't' or 'T');

                break;
        }
    }

    public static string Array2String<T>(IEnumerable<T> list)
    {
        return string.Join("", list);
    }

    public static void ConvertDecimalToZmU1U2(int liczba)
    {
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
        Console.WriteLine("U2: "+Array2String(U2));
        Console.WriteLine("ZM: "+Array2String(ZM));
        Console.WriteLine("U1: "+Array2String(U1));
        
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
    
    static void CalculateBinarySum(int num1, int num2)
    {
        int i = 0;
        int rem = 0;
        string str="";

        while (num1 != 0 || num2 != 0)
        {
            str += (num1 % 10 + num2 % 10 + rem) % 2;
            rem = (num1 % 10 + num2 % 10 + rem) / 2;

            num1 = num1 / 10;
            num2 = num2 / 10;
        }

        if (rem != 0)
            str += rem;
        

        Console.Write("Sum is : ");
        for (i = str.Length - 1; i >= 0; i--)
        {
            Console.Write(str[i]);
        }
        Console.WriteLine();
    }

}
