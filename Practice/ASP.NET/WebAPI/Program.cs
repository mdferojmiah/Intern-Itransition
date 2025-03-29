using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// .ConfigureApiBehaviorOptions(options =>{
//     options.SuppressModelStateInvalidFilter = true;
// });

builder.Services.Configure<ApiBehaviorOptions>(options => {
    options.InvalidModelStateResponseFactory = context => {
        var errors = context.ModelState
            .Where(e => e.Value != null && e.Value.Errors.Count > 0)
            .SelectMany(e => e.Value!.Errors.Select(e => e.ErrorMessage)).ToList();

        // var errorString = string.Join(";", errors.Select(e => $"{e.Field}: {string.Join(",", e.Errors)}"));
        // return new BadRequestObjectResult(new{
        //     Message = "Validation failed!",
        //     Errors = errorString
        // });
        return new BadRequestObjectResult(ApiResponse<object>.ErrorResponse(false, 400, errors, null, "Validation failed!"));
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => {
    return "API is working";
});
app.MapControllers();
app.Run();
