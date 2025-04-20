using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyJokesApp.Models
{
    public class Category
    {
        public int Id{get; set;}
        public string Name{get; set;} = null!;
        public List<Item>? Items{get; set;}
    }
}