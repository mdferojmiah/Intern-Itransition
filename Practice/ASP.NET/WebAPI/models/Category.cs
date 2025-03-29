using System;
using System.Collections.Generic;

public class Category{
    public Guid CategoryId{get; set;}
    public string? CategoryName{get; set;}
    public string? Description{get; set;}
    public DateTime CreatedAt{set; get; }
}
