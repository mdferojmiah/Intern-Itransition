using System;

class Program{
    public static void Main(string[] args){
        int [] nums = { 1, 2, 5, 3, 4};
        // int total = 0;
        // foreach(int num in nums){
        //     total += num;
        // }
        // Console.WriteLine(total);

        // //2d array
        int [,] matrix = {{1, 2, 3,},  {4, 5, 6}};
        // //printing matrix
        // for(int i = 0; i < 2; i++){
        //     for(int j = 0; j < 3; j++){
        //         Console.Write(matrix[i,j] + " ");
        //     }
        //     Console.WriteLine();
        // }


        // //jagged array
        // int [][] jaggedArray = {
        //     new [] { 1, 2, 3,},
        //     new [] {4},
        //     new [] {5, 6, 7, 8}
        // };

        // foreach(var row in jaggedArray){
        //     foreach(var item in row){
        //         Console.Write($"{item} ");
        //     }
        //     Console.WriteLine();
        // }


        //property and methods
        Console.WriteLine($"Length of the array: {nums.Length} {matrix.Length}");
        Console.WriteLine($"Rank of the array: {nums.Rank} {matrix.Rank}");

        Console.WriteLine($"Maximum of the array: {nums.Max()}");
        Console.WriteLine($"Minimum of the array: {nums.Min()}");
        Console.WriteLine($"Sum of the array: {nums.Sum()}");
        Console.WriteLine($"Average of the array: {nums.Average()}"); 

        Console.WriteLine("Before sorting!");
        PrintArray(nums);  

        Console.WriteLine("After sorting!");
        Array.Sort(nums);
        PrintArray(nums);

        Console.WriteLine("After Reverse!");
        Array.Reverse(nums);
        PrintArray(nums);  

        Console.WriteLine($"{Array.IndexOf(nums, 5)}");
        Console.WriteLine($"{Array.Exists(nums, num => num == 6)}");

        int[] copy = new int[nums.Length];
        Array.Copy(nums, copy, nums.Length);
        PrintArray(copy);  

        Array.Clear(copy, 0, copy.Length);
        Console.WriteLine("After clearing copy");
        PrintArray(copy);  


        Console.WriteLine();
        Console.WriteLine($"{Sum(1,2)} {Sum(1, 2, 3)} {Sum(1,2,3,4)}");
    }

    public static void PrintArray(int[] nums){
        foreach(int num in nums){
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }

    //params is used to receive unlimited parameter or arguments
    public static int Sum(params int[] numbers){
        int total = 0;
        foreach(int num in numbers){
            total += num;
        }
        return total;
    }
}