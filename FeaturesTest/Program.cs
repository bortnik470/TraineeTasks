using FeaturesTest;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using CustomTuple = (string address, int);

bool TestMail(string? str, [CallerArgumentExpression("str")] string? param = default)
{
    string math = @"\.com$";

    if (Regex.IsMatch(str, math))
    {
        Console.WriteLine($"Correct address {str} was provided by {param}");
        return true;
    }
    else
    {
        Console.WriteLine($"Incorrect address {str} was provided by {param}");
        return false;
    }
}

string GetAddress()
{
    return "IcorrectAddress.com";
}

CustomTuple tuple = ("microsoft.net", 1);
ClassA CorrectAddress = new ClassA("google.com");
ClassB IncorrectAddress = new ClassB("google.net");

TestMail(CorrectAddress.StringA);
TestMail(IncorrectAddress.StringB);
TestMail(CorrectAddress.ToString());
TestMail(tuple.address);
TestMail(GetAddress());