// See https://aka.ms/new-console-template for more information
using System.Numerics;

Console.WriteLine(DecimalToSnafu(File.ReadAllLines("Input.txt").Select(line => SnafuToDecimal(line)).Sum()));

long SnafuToDecimal(string line)
{
    long result = 0;
    foreach(char c in line)
    {
        result *= 5;
        switch(c)
        {
            case '=':
                result += -2;
                break;
            case '-':
                result += -1;
                break;
            case '0':
                result += 0;
                break;
            case '1':
                result += 1;
                break;
            case '2':
                result += 2;
                break;
        }
    }
    return result;
}

string DecimalToSnafu(long dec)
{
    string result = string.Empty; ;
    while(dec != 0)
    {
        switch((dec + 2)% 5)
        {
            case 0:
                result = "=" + result;
                dec -= -2;
                break;
            case 1:
                result = "-" + result;
                dec -= -1;
                break;
            case 2:
                result = "0" + result;
                break;
            case 3:
                result = "1" + result;
                dec -= 1;
                break;
            case 4:
                result = "2" + result;
                dec -= 2;
                break;
        }
        if(dec % 5 != 0)
        {
            throw new InvalidOperationException("Something went wrong here...");
        }
        dec /= 5;
    }
    return result;
}