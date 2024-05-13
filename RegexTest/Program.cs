using System.Text.RegularExpressions;

string regPattern1 = @"(first)(?=grade)";
string regPattern2 = @"[0-9]{5}";
string regPattern3 = @"(?<=first)(second)";
string regPattern4 = @"[1-8]+0";
string regPattern5 = @"^[a-zA-Z0-9\._\+\-]+@[a-zA-Z0-9]+\.[a-z]{3,}$";

string value1 = "firstgradefirstdecade";
string value2 = "1234512ds345";
string value3 = "firstsecond44532 firstgrade";
string value4 = "0420359128450";
string value5 = "someMail@gmail.com";

Regex regex3 = new Regex(regPattern3);

var result1 = Regex.Matches(value1, regPattern1);

var result2 = Regex.Matches(value2, regPattern2);

var result3 = regex3.Replace(value3, "Third");

var result4 = Regex.Matches(value4, regPattern4);

var result5 = Regex.Matches(value5, regPattern5);

foreach (Match m in result1)
{
    Console.WriteLine(m);
}

foreach (Match m in result2)
{
    Console.WriteLine(m);
}

Console.WriteLine(result3);

foreach (Match m in result4)
{
    Console.WriteLine(m);
}

foreach (Match m in result5)
{
    Console.WriteLine(m);
}