using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyJokesApp.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Item? Item { get; set; }
    }
}