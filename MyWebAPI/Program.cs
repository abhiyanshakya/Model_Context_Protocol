//DUMMY API Source - https://dummyjson.com/

using Api.Services;

//builder contains WebApplicationBuilder obj created by Microsoft.
var builder = WebApplication.CreateBuilder(args);

/**
*-----------------ASP.NET services our application use-----------------
*/
builder.Services.AddControllers();

//registering and configure an httpclient.
builder.Services.AddHttpClient<ITodoService, TodosService>(client =>
{
    client.BaseAddress =
        new Uri("https://dummyjson.com/todos");
});

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();