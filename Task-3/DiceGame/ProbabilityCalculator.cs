using System.Collections.Generic;

public class ProbabilityCalculator{
    // this class is for calculating probability
    public static double CalculateProbability(Dice userDice, Dice computerDice){
        int userWin = 0;
        int totalRolls = userDice.Faces.Length * computerDice.Faces.Length;

        for(int i = 0; i < userDice.Faces.Length; i++){
            for(int j = 0; j < computerDice.Faces.Length; j++){
                if(userDice.Roll(i) > computerDice.Roll(j)){
                    userWin++;
                }
            }
        }
        return (double) userWin / totalRolls;
    }
}