using System;

class MyClass{
    public static void Main(string[] args){
        string text = "   Hello Sathi!  ";
        string emptytezt = "";
        Console.WriteLine(text);
        Console.WriteLine($"The length of the string: {text.Length}");
        Console.WriteLine($"0th index of the string: {text[0]}");

        Console.WriteLine(string.IsNullOrEmpty(text));
        Console.WriteLine(string.IsNullOrEmpty(emptytezt));

        Console.WriteLine(text.Trim());
        Console.WriteLine(text.ToUpper());
        Console.WriteLine(text.ToLower());
        Console.WriteLine(text.Substring(6));

        string insertedText = text.Insert(0, "Hi! ");
        Console.WriteLine(insertedText);

        string removeText = insertedText.Remove(4);
        Console.WriteLine(removeText);

        string replacedText = text.Replace("Hello ", "Hi ");
        Console.WriteLine(replacedText);
        Console.WriteLine(text.Contains("Sathi"));

        string reversedText = new string(text.Reverse().ToArray());
        Console.WriteLine(reversedText);

        var words = text.Split(" ");
        Console.WriteLine(string.Join(",", words));
    }
}