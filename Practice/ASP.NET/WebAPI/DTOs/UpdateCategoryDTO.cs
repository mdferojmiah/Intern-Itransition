using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


public class UpdateCategoryDTO{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters!")]
    public string? CategoryName{get; set;}
    [StringLength(500, MinimumLength =2, ErrorMessage ="Category description must be between 2 and 100 characters!")]
    public string? Description{get; set;} = string.Empty;
}