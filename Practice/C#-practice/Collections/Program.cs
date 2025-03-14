using System;
using System.Collections.Generic;
using System.Collections;


class Program{
    public static void Main(string[] args){
        // ArrayList arrayList = new ArrayList();
        // arrayList.Add(1);
        // arrayList.Add(2.2);
        // arrayList.Add(null);
        // arrayList.Add("Feroj");
        // arrayList.RemoveAt(2);
        // foreach(var item in arrayList){
        //     Console.WriteLine(item);
        // }

        Hashtable ht = new Hashtable();
        ht.Add(1, "Feroj");
        ht.Add(2.2, 1000);
        ht.Add(true, false);

        foreach(DictionaryEntry de in ht){
            Console.WriteLine($"{de.Key}: {de.Value}");
        } 
        Console.WriteLine(ht.Contains(2));
    }
}


