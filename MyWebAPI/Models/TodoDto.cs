namespace Api.Models;

public class TodoDto
{
    public int Id { get; set; }
    public string Todo { get; set; } = string.Empty;
    public bool Completed { get; set; }
    public int UserId { get; set; }
}

public class ErrorResponse
{
    public int Status { get; set; }
    public string Error { get; set;} = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public class ApiException(int status, string error, string message) : Exception(message)
{
    public int Status { get; } = status;
    public string Error { get; } = error;
}