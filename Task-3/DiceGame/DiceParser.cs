using System;
using System.Collections.Generic;

public class DiceParser{
    //this class will take the array from Dice class and create a dice list.
    public static List<Dice> ParseDice(string[] args){
        if (args == null || args.Length < 3)
        {
            throw new ArgumentException("Three Dice configaration is required!");
        }

        List<Dice> diceList = new List<Dice>();
        foreach(string arg in args){
            try{
                Dice dice = new Dice(arg);
                diceList.Add(dice);
            }catch(FormatException){
                throw new ArgumentException($"Invalid dice configaration: {arg}, Faces must be comma-separated!");
            }catch(ArgumentException ex){
                throw new ArgumentException(ex.Message);
            }
        }

        return diceList;
    }
}
