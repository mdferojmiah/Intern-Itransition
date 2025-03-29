using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// descriptive name
// plurals for collections
// plurals/{singular} example: /categories/{categoryID}
// use hipens for multiple words for improving readability example: /product-categories
// versioning exmaple: /v1/api/categories
// avoid using verb in the url path examaple: /create-categories[bad example] /api/categories[good example]



[ApiController]
[Route("v1/api/categories/")]
public class CategoryController: ControllerBase{
    private static List<Category> categories= new List<Category>();
    public const int successStatusCode = 200;
    public const string successMessage = "Categories returned successfully!";
    public const int createdStatusCode = 201;
    public const string createdMessage = "Category created successfully!";
    public const int updatedStatusCode = 204;
    public const string updatedMessage = "Category updated successfully!";
    public const int deletedStatusCode = 204;
    public const string deletedMessage = "Category deleted successfully!";
    
    

    [HttpGet]
    public IActionResult GetCategories(){
        // if(!string.IsNullOrEmpty(searchValue)){
        //     var searchResult = categories.Where(c => !string.IsNullOrEmpty(c.CategoryName) && c.CategoryName.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
        //     return Ok(ApiResponse<List<Category>>.SuccessResponse(true, successStatusCode, null, searchResult, successMessage));
        // }

        var categoryList = categories.Select(c => new ReadCategoryDTO{
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Description = c.Description,
            CreatedAt = c.CreatedAt,
        }).ToList();
        return Ok(ApiResponse<List<ReadCategoryDTO>>.SuccessResponse(true, successStatusCode, null, categoryList, successMessage));
    }

    [HttpGet("{categoryId:guid}")]
    public IActionResult GetCategoryById(Guid categoryId){
        var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
        if(foundCategory == null){
            return Ok(ApiResponse<object>.ErrorResponse(false, 404, new List<string>{"Invalid ID: Category with this id does not exists!"}, null, "Category does not exist!"));
        }

        var readCategoryDTO = new ReadCategoryDTO{
            CategoryId = foundCategory.CategoryId,
            CategoryName = foundCategory.CategoryName,
            Description = foundCategory.Description,
            CreatedAt = foundCategory.CreatedAt,
        };
        return Ok(ApiResponse<ReadCategoryDTO>.SuccessResponse(true, successStatusCode, null, readCategoryDTO, successMessage));
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CreateCategoryDTO response){
        if(!ModelState.IsValid){
            
        }
        var category = new Category{
            CategoryId = Guid.NewGuid(),
            CategoryName = response.CategoryName,
            Description = response.Description,
            CreatedAt = DateTime.UtcNow,
        };
        categories.Add(category);

        var readCategoryDTO = new ReadCategoryDTO {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
        };
        return Created(nameof(GetCategoryById), ApiResponse<ReadCategoryDTO>.SuccessResponse(true, createdStatusCode, null, readCategoryDTO, createdMessage));
    }

    [HttpDelete("{categoryId:guid}")]
    public IActionResult DeleteCategoryById(Guid categoryId){
        var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
        if (foundCategory == null){
            return Ok(ApiResponse<object>.ErrorResponse(false, 404, new List<string>{"Invalid ID: Categoey with this id does not exists!"}, null, "Category does not exist!"));
        }
        categories.Remove(foundCategory);
        return Ok(ApiResponse<object>.SuccessResponse(true, deletedStatusCode, null, null, deletedMessage));
    }

    [HttpPut("{categoryId:guid}")]
    public IActionResult UpdateCategoryById(Guid categoryId, [FromBody] UpdateCategoryDTO response){
        var foundCategory = categories.FirstOrDefault(category => category.CategoryId == categoryId);
        if(foundCategory == null){
            return Ok(ApiResponse<object>.ErrorResponse(false, 404, new List<string>{"Invalid ID: Categoey with this id does not exists!"}, null, "Category does not exist!"));
        }
        foundCategory.CategoryName = response.CategoryName ?? foundCategory.CategoryName;
        foundCategory.Description = response.Description ?? foundCategory.Description;
        return Ok(ApiResponse<object>.SuccessResponse(true, updatedStatusCode, null, null, updatedMessage));
    }
}