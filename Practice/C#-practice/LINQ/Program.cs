using System;
using System.Collections.Generic;
using System.Linq;

public class Program{
    // public static void Main(string[] args){
    //     // List<int> list1 = new List<int>{1,2,3,4,5,6,7,8,9,10};
    //     // List<int> list2 = new List<int>{1,3,5,7,9};
    //     List<Student> students = new List<Student>{
    //         new Student("Feroj", 37, "O+"),
    //         new Student("Samiul", 13, "B+"),
    //         new Student("Tanjim", 18, "B-")
    //     };

    //     // var evenNumbers = list1.Where(num => num % 2 == 0);
    //     var selectedStudent = students.Where(student => student.Id > 15);
    //     var studentBg = students.Select(student => (student.Name, student.Bg));

    //     // if(selectedStudent.Any()){
    //     //     Console.WriteLine($"The Count: {selectedStudent.Count()}");
    //     //     foreach(var item in selectedStudent){
    //     //         Console.WriteLine($"{item.Id} {item.Name}");
    //     //     }
    //     // } else{
    //     //     Console.WriteLine("Found Nothing! :(");
    //     // }

    //     if(studentBg.Any()){
    //         Console.WriteLine($"The Count: {studentBg.Count()}");
    //         foreach(var item in studentBg){
    //             Console.WriteLine($"{item.Name} {item.Bg}");
    //         }
    //     } else{
    //         Console.WriteLine("Found Nothing! :(");
    //     }
    // }

    public static void Main(string[] args){
        List<Student> students = new List<Student>{
            new Student("Feroj", 37, "O+"),
            new Student("Samiul", 13, "B+"),
            new Student("Tanjim", 18, "B-"),
            new Student("Atike", 37, "O+")
        };

        var sortedStudents = students.OrderBy(student => student.Id).ThenBy(students => students.Name).ThenBy(students => students.Bg);
        var doesContainFeroj = students.Select(students => students.Name).Contains("Feroj");

        if(doesContainFeroj){
            Console.WriteLine("Found Feroj! :)");
        }

        if(sortedStudents.Any()){
            foreach(var item in sortedStudents){
                Console.WriteLine($"{item.Name} {item.Id} {item.Bg}");
            }
        }else {
            Console.WriteLine("Found Nothing! :(");
        }

    }

}

public class Student{
    public string Name{get; set;}
    public int Id{get; set;}
    public string Bg{get; set;}
    
    public Student(string name, int id, string bg){
        Name = name;
        Id = id;
        Bg = bg;
    }
}