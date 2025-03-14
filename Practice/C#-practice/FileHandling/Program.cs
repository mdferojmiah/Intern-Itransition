using System;
using System.IO;
using System.Text;

namespace File{
    public class MainClass{
        // public static void Main(string[] args){
        //     string FilePath = @"C:\Users\01fer\Desktop\Intern-Itransition\Practice\C#-practice\FileHandling\MyFile.txt";
        //     FileStream fs = new FileStream(FilePath, FileMode.Append);
        //     byte[] ByteData = Encoding.Default.GetBytes("My name is Feroj Miah. I'm a wanna be software engineer.");
        //     fs.Write(ByteData, 0, ByteData.Length);
        //     fs.Close();
        //     Console.WriteLine("File has been edited and saved successfully!");
        // }

        // public static void Main(string[] args){
        //     try {
        //         string FilePath = @"C:\Users\01fer\Desktop\Intern-Itransition\Practice\C#-practice\FileHandling\MyFile.txt";
        //         FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
        //         string data;

        //         using(StreamReader streamReader = new StreamReader(fs)){
        //             data = streamReader.ReadToEnd();
        //         } 
        //         Console.WriteLine("Readed Data: " + data);
        //     }catch(Exception e){
        //         Console.WriteLine(e.Message);
        //     }
        // }


        // public static void Main(string[] args){
        //     try{
        //         StreamWriter streamWriter = new StreamWriter("C://Users/01fer/Desktop/Intern-Itransition/Practice/C#-practice/FileHandling/Proposal.txt");

        //         Console.Write("Enter the text you wanna store: ");
        //         string? inputdata = Console.ReadLine();

        //         streamWriter.Write(inputdata);
        //         Console.WriteLine("Data has been Written to the file");

        //         streamWriter.Flush();
        //         streamWriter.Close();
        //     }catch(Exception e){
        //         Console.WriteLine(e.Message);
        //     }
        // }


        // public static void Main(string[] args){
        //     Console.WriteLine("Welcome to the DashBoard!");
        //     Console.WriteLine("Enter 0 to exit!");
        //     Console.WriteLine();

            
        //     int choice = 1;
        //     while(true){
        //         Console.Write("Do you want to continue(1/0)[1 for yes and 0 for no]: ");
        //         choice = int.Parse(Console.ReadLine());
        //         Console.WriteLine();
        //         if(choice == 0){
        //             break;
        //         }

        //         Console.Write("Enter the first input: ");
        //         int a = int.Parse(Console.ReadLine());
        //         Console.Write("Enter the second input: ");
        //         int b = int.Parse(Console.ReadLine());

        //         int result = a + b;

        //         string FilePath = "C://Users/01fer/Desktop/Intern-Itransition/Practice/C#-practice/FileHandling/MyFile.txt";

        //         StreamWriter sw = new StreamWriter(FilePath, true);
        //         sw.WriteLine($"{a} + {b} = {result}");
        //         Console.WriteLine();
        //         sw.Flush();
        //         sw.Close();
        //     }
        // }

        public static void Main(string[] args){
            string filePath = @"C:\Users\01fer\Desktop\Intern-Itransition\Practice\C#-practice\FileHandling\MyTextFile.txt";

            StreamWriter sw = new StreamWriter(filePath);
            sw.WriteLine("Assalamu Alaikum my beloved brothers and sisters!");
            sw.WriteLine("Hope you guys are doing well. I'm also doing great by the grace of ALlah(SW).");
            sw.Flush();
            sw.Close();

            Console.WriteLine("Approch 1: using ReadToEnd Method.");
            //if we use using(object){} then we don't have to close the object 
            // or else we must have to flush and close the object 
            using(StreamReader sr = new StreamReader(filePath)){
                Console.WriteLine(sr.ReadToEnd());
            }
            Console.WriteLine();

            Console.WriteLine("Approch 2: using ReadLine Method.");
            StreamReader reader = new StreamReader(filePath);
            reader.BaseStream.Seek(0, SeekOrigin.Begin);
            string? data = reader.ReadLine();
            while(data != null){
                Console.WriteLine(data);
                data = reader.ReadLine();
            }
            //reader is need not to be flushed!
            reader.Close();
            
        }
    }
}