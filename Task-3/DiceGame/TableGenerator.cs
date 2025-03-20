using System;
using System.Collections.Generic;
using ConsoleTableExt;

public class TableGenerator
{
    //this clas is for the generating the table using ConsoleTableExt
    public static void DisplayTable(List<Dice> diceList)
    {
        var tableData = new List<List<object>>();
        var headers = new List<object> { "User dice v" };

        foreach (var dice in diceList)
        {
            headers.Add(string.Join(",", dice.Faces));
        }

        tableData.Add(headers);

        for (int i = 0; i < diceList.Count; i++)
        {
            var row = new List<object> { string.Join(",", diceList[i].Faces) };
            for (int j = 0; j < diceList.Count; j++)
            {
                if (i == j)
                {
                    row.Add("- (" + ProbabilityCalculator.CalculateProbability(diceList[i], diceList[j]).ToString("0.0000") + ")");
                }
                else
                {
                    row.Add(ProbabilityCalculator.CalculateProbability(diceList[i], diceList[j]).ToString("0.0000"));
                }
            }
            tableData.Add(row);
        }

        ConsoleTableBuilder.From(tableData)
            .WithTitle("Probability of the win for the user:", ConsoleColor.Green)
            .ExportAndWriteLine();
    }
}