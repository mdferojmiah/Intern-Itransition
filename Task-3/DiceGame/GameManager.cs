using System;
using System.Collections.Generic;

public class GameManager{
    //this class is for managing the whole game
    private List<Dice> diceList;
    private Random random = new Random();

    public GameManager(List<Dice> diceList){
        this.diceList = diceList;
    }

    public void PlayGame(){
        int firstMover = DetermineFirstMover();
        int computerDiceIndex, userDiceIndex;
        Dice userDice, computerDice;
        int computerRoll, userRoll;

        if(firstMover == 1){
            computerDiceIndex = SelectComputerDice();
            computerDice = diceList[computerDiceIndex];
            Console.WriteLine($"I make the first move and choose the [{string.Join(",",computerDice.Faces)}] dice.");
            
            userDiceIndex = SelectUserDice(computerDiceIndex);
            userDice = diceList[userDiceIndex];
            Console.WriteLine($"You choose the [{string.Join(",", userDice.Faces)}] dice.");
            
            computerRoll = ComputersTurn(computerDice);
            userRoll = UsersTurn(userDice);
        }else {
            userDiceIndex = SelectUserDice(-1);
            userDice = diceList[userDiceIndex];
            Console.WriteLine($"You choose the [{string.Join(",", userDice.Faces)}] dice.");
            
            computerDiceIndex = SelectComputerDice(userDiceIndex);
            computerDice = diceList[computerDiceIndex];
            Console.WriteLine($"I choose the [{string.Join(",",computerDice.Faces)}] dice.");
            
            userRoll = UsersTurn(userDice);
            computerRoll = ComputersTurn(computerDice);
        }

        if(userRoll > computerRoll){
            Console.WriteLine($"You WIN({userRoll} > {computerRoll})!!!");
        } else if(userRoll < computerRoll){
            Console.WriteLine($"I WIN({computerRoll} > {userRoll})!!!");
        }else {
            Console.WriteLine($"It's a TIE({userRoll} = {computerRoll})!!!");
        }
    }

    private int ComputersTurn(Dice computerDice){
        Console.WriteLine("It's time for my roll.");
        var(computerNumber, computerKey, computerHmac) = RandomGenerator.GenerateRandom(6);
        Console.WriteLine($"I've selected a random number in the range of 0..6 (HMAC = {computerHmac})");
        Console.WriteLine("Add your number modulo 6.");
        int userNumber = GetUserNumber(6);
        Console.WriteLine($"My number is {computerNumber} (KEY = {BitConverter.ToString(computerKey).Replace("-", "")}).");
        int fairNumber = RandomGenerator.CalculateResult(computerNumber, userNumber, 6);
        Console.WriteLine($"The fair number generation result is {computerNumber} + {userNumber} = {fairNumber} mod 6");
        int computerRoll = computerDice.Roll(fairNumber);
        Console.WriteLine($"My roll result is {computerRoll}.");
        return computerRoll;
    }

    private int UsersTurn(Dice userDice){
        Console.WriteLine("It's time for your roll.");
        var(computerNumber, computerKey, computerHmac) = RandomGenerator.GenerateRandom(6);
        Console.WriteLine($"I've selected a random number in the range of 0..6 (HMAC = {computerHmac})");
        Console.WriteLine("Add your number modulo 6.");
        int userNumber = GetUserNumber(6);
        Console.WriteLine($"My number is {computerNumber} (KEY = {BitConverter.ToString(computerKey).Replace("-", "")}).");
        int fairNumber = RandomGenerator.CalculateResult(computerNumber, userNumber, 6);
        Console.WriteLine($"The fair number generation result is {computerNumber} + {userNumber} = {fairNumber} mod 6");
        int userRoll = userDice.Roll(fairNumber);
        Console.WriteLine($"Your roll result is {userRoll}.");
        return userRoll;
    }

    private int DetermineFirstMover(){
        Console.WriteLine("Let's determine who makes the first move.");
        var (computerNumber, key, hmac) = RandomGenerator.GenerateRandom(2);
        Console.WriteLine($"I've selected a random value in the range of 0..1 (HMAC = {hmac}).");
        Console.WriteLine("Try to guess my selection!");
        int userNumber = GetUserNumber(2);
        int result = RandomGenerator.CalculateResult(computerNumber, userNumber, 2);
        Console.WriteLine($"My selection: {computerNumber} (KEY = {BitConverter.ToString(key).Replace("-", "")}).");
        return result;
    }

    private int GetUserNumber(int range){
        for(int i = 0; i < range; i++){
            Console.WriteLine($"{i} - {i}");
        }
        Console.WriteLine($"X - exit");
        Console.WriteLine($"? - help");

        while(true){
            Console.Write("Your Selection: ");
            string? input = Console.ReadLine();
            if(input.Equals("X", StringComparison.OrdinalIgnoreCase)){
                Environment.Exit(0);
            }else if(input.Equals("?", StringComparison.OrdinalIgnoreCase)){
                TableGenerator.DisplayTable(diceList);
            }else if(int.TryParse(input, out int userNumber) && userNumber >= 0 && userNumber < range){
                return userNumber;
            }else{
                Console.WriteLine("Invalid selection. Try again!");
            }
        }
    }

    private int SelectComputerDice(int excludeIndex = -1){
        int index = random.Next(diceList.Count);
        if(excludeIndex == index && diceList.Count > 2){
            index = (index + 1) % diceList.Count;
        }
        return index;
    }

    private int SelectUserDice(int excludeIndex){
        Console.WriteLine("Choose your dice: ");
        for(int i = 0; i < diceList.Count; i++){
            if(i != excludeIndex){
                Console.WriteLine($"{i} - {string.Join(",", diceList[i].Faces)}");
            }
        }
        Console.WriteLine("X - exit");
        Console.WriteLine("? - help");
        while(true){
            Console.Write("Your Selection: ");
            string? input = Console.ReadLine();
            if(input.Equals("X", StringComparison.OrdinalIgnoreCase)){
                Environment.Exit(0);
            }else if(input.Equals("?", StringComparison.OrdinalIgnoreCase)){
                TableGenerator.DisplayTable(diceList);
            }else if(int.TryParse(input, out int userNumber) && userNumber >= 0 && userNumber < diceList.Count && userNumber != excludeIndex){
                return userNumber;
            }else{
                Console.WriteLine("Invalid selection. Try again!");
            }
        }
    }
}