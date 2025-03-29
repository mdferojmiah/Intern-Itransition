using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ApiResponse<T>{
    public bool Success{get; set;}
    public string? Message{get; set;}
    public T? Data{get; set;}
    public int StatusCode{get; set;}
    public List<string>? Errors{get; set;}
    public DateTime Time{get; set;}


    private ApiResponse(bool success, int statusCode, List<string>? errors, T? data, string message = ""){
        Success = success;
        Message = message;
        Data = data;
        StatusCode = statusCode;
        Errors = errors;
        Time = DateTime.Now;
    }


    public static ApiResponse<T> SuccessResponse(bool success, int statusCode, List<string>? errors, T? data, string message = ""){
        return new ApiResponse<T>(success, statusCode, errors, data, message);
    }

    public static ApiResponse<T> ErrorResponse(bool success, int statusCode, List<string>? errors, T? data, string message = ""){
        return new ApiResponse<T>(success, statusCode, errors, data, message);
    }
}
