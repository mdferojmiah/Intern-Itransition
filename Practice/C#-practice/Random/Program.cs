using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace RandomShuffle{
    public class RandomShuffle{
        public static void Main(string[] args){
            Random random = new Random();

            // for(int i = 0; i < 10; i++){
            //     // Console.WriteLine(random.Next(10));
            //     SimpleMethod(random);
            // }

            List<PersonInfo> person = new List<PersonInfo>(){
                new PersonInfo{Name = "Feroj"},
                new PersonInfo{Name = "Atike"},
                new PersonInfo{Name = "Laden"},
                new PersonInfo{Name = "Sathi"},
                new PersonInfo{Name = "Nadira"},
                new PersonInfo{Name = "Islam"}
            };

            // var sortedPeople = person.OrderBy((x) => x.Name); // for sorting in alphbatic order
            var sortedPeople = person.OrderBy((x) => random.Next()); // for random shuffle 


            foreach(PersonInfo p in sortedPeople){
                Console.WriteLine(p.Name);
            }
        }

        public static void SimpleMethod(Random random){
            Console.WriteLine(random.Next(5, 11));
        }
    }

    public class PersonInfo{
        string? name;
        public string? Name {get; set;}
    }
}