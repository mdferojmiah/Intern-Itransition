using System;
using System.Collections.Generic;

public class Program{
    //this is the main Program
    public static void Main(string[] args){
        try{
            List<Dice> diceList = new List<Dice>();
            diceList = DiceParser.ParseDice(args);
            GameManager gameManager= new GameManager(diceList);
            gameManager.PlayGame();
        }catch(ArgumentException ex){
            Console.WriteLine($"Error: {ex.Message}");
        }

    }
}