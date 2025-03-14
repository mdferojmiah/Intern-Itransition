using System;


interface IAnimal{
    void animalSound();
    void run();
}

interface IPet{
    void play();
}


//sealed keyword is used, if we don't want other class to inherit from a class
sealed class Parts{
    private string? gear;
    public string? Gear{
        get; set;
    }
}

abstract class Vehicle{
    public abstract void Honk();
}

 class Car: Vehicle {
    private string? modelName;
    // public string Name {
    //     get{return name;}
    //     set{name = value;}
    // }
    //shorthand Property
    public string? ModelName{
        get; set;
    }

    public override void Honk()
    {
        Console.WriteLine("Teet, teet!");
    }
}

class Marcedes: Car{
    public override void Honk(){
        Console.WriteLine("Peep, peeep!");
    }
}

class Ford: Car{
    public override void Honk(){
        Console.WriteLine("Taap, Taaap!");
    }
}



class Pig : IAnimal
{
    public void animalSound()
    {
        Console.WriteLine("weee weee!");
    }

    public void run()
    {
        Console.WriteLine("Pig is running!");
    }
}

class Cat : IAnimal, IPet
{
    public void animalSound()
    {
        Console.WriteLine("Meaw Meaw!");
    }

    public void play()
    {
        Console.WriteLine("Cat is playing!");
    }
    public void run()
    {
        Console.WriteLine("Cat is running!");
    }
}

public class MainClass{
    public static void Main(string[] args){
        Car car = new Car();
        Car marcedes = new Marcedes();
        Car ford = new Ford();

        Parts parts = new Parts();
        parts.Gear = "3rd Gear!";
        Console.WriteLine(parts.Gear);

        car.ModelName = "Odi R8";
        Console.WriteLine(car.ModelName);
        car.Honk();
        marcedes.Honk();
        ford.Honk();


        Pig pig = new Pig();
        pig.animalSound();
        pig.run();

        Cat cat = new Cat();
        cat.animalSound();
        cat.run();
        cat.play();
        
    }
}