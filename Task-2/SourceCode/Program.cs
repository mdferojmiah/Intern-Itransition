using System;
using System.IO;
using System.Collections.Generic;
using Org.BouncyCastle.Crypto.Digests;

class Task2
{
    public static void Main()
    {
        string folderPath = @"C:\Users\01fer\Desktop\Intern-Itransition\Task-2\task2";
        string email = "01ferojmiah@gmail.com";
        string[] files = Directory.GetFiles(folderPath);
        if (files.Length != 256)
        {
            Console.WriteLine("Error: Incorrect number of files!");
            return;
        }

        List<string> hashes = new List<string>();
        foreach (string file in files)
        {
            hashes.Add(ComputeSHA3_256(File.ReadAllBytes(file)));
        }
        hashes.Sort((a, b) => string.Compare(b, a));
        string finalString = string.Concat(hashes) + email.ToLower();
        string finalHash = ComputeSHA3_256(System.Text.Encoding.UTF8.GetBytes(finalString));
        Console.WriteLine(finalHash);
    }

    public static string ComputeSHA3_256(byte[] data)
    {
        Sha3Digest digest = new Sha3Digest(256);
        digest.BlockUpdate(data, 0, data.Length);
        byte[] hash = new byte[digest.GetDigestSize()];
        digest.DoFinal(hash, 0);
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }
}
