using System;

public class Dice{
    //this class will take a stirng input and convert it into a array
    public int[] Faces {get;}

    public Dice (string faceValues){
        Faces = faceValues.Split(",").Select(int.Parse).ToArray();
        if(Faces.Length == 0){
            throw new ArgumentException("Dice has no face. Dice must contain one face!");
        }
    }

    public int Roll(int faceIndex){
        if(faceIndex < 0 || faceIndex >= Faces.Length){
            throw new ArgumentOutOfRangeException($"{nameof(faceIndex)} is out of range!");
        }
        return Faces[faceIndex];
    }

    public int FaceCount(){
        return Faces.Length;
    }

}