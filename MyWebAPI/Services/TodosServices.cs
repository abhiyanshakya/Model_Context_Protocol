using System.Text;
using System.Text.Json;
using Api.Models;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public interface ITodoService
{
    Task<string> GetTodosAsync();

    Task<string> PostTodoAsync(TodoDto todo);
}
public class TodosService(HttpClient httpClient): ITodoService
{
    private const string GetTodosRoute = "todos";
    private const string CreateTodoRoute = "/todos/add";

    public async Task<string> GetTodosAsync()
    {
        try
        {
            return await httpClient.GetStringAsync(GetTodosRoute);
        }

        //401 - Unauthorize
        catch (UnauthorizedAccessException)
        {
            throw new ApiException(
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                "Unauthorized access while processing the todo request.");
        }

        //400 - Bad Request
        catch (ArgumentException)
        {
            throw new ApiException(
                StatusCodes.Status400BadRequest,
                "Bad Request",
                "Invalid todo request");
        }

        //500 - Internal Server Error
        catch (Exception)
        {
            throw new ApiException(
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "An unexpected error occurred while processing the request.");
        }
    }
    
    public async Task<string> PostTodoAsync(TodoDto todo)
    {
        try
        {
            var json = JsonSerializer.Serialize(
                todo,
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync(
                CreateTodoRoute, content);

            response.EnsureSuccessStatusCode(); // throws an exception --> handled below as a 500.

            return await response.Content.ReadAsStringAsync();
        }

        //401 - Unauthorize
        catch (UnauthorizedAccessException)
        {
            throw new ApiException(
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                "You are not authorized to access or modify todo data.");
        }

        //400 - Bad Request
        catch (ArgumentException)
        {
            throw new ApiException(
                StatusCodes.Status400BadRequest,
                "Bad Request",
                "The request contains invalid or missing data.");
        }

        //500 - Internal Server Error
        catch (Exception)
        {
            throw new ApiException(
                StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                "Failed to create todo, An unexpected error occurred while processing the todo request.");
        }
    }
}