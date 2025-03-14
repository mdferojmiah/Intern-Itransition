using System;

enum Months{
    January = 1,
    February,
    March,
    April,
    May,
    June,
    July
}

class MainClass {
    public static void Main(string[] args){
        Months months = Months.January;
        int numMonths = (int) Months.January;

        switch (months){
            case Months.January:
                Console.WriteLine($"{months}: {numMonths}");
                break;
            case Months.February:
                Console.WriteLine("something");
                break;
        }
    }
}