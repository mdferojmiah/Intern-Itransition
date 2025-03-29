using System;
using System.Collections.Generic;
using System.Linq;


public class ReadCategoryDTO{
    public Guid CategoryId{get; set;}
    public string? CategoryName{get; set;}
    public string? Description{get; set;} = string.Empty;
    public DateTime CreatedAt{set; get; }
}