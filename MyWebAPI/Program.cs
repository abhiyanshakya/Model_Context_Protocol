//DUMMY API Source - https://dummyjson.com/

using Api.Services;

//builder contains WebApplicationBuilder obj created by Microsoft.
var builder = WebApplication.CreateBuilder(args);

/**
*-----------------ASP.NET services our application use-----------------
*/
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

//registering and configure an httpclient.
builder.Services.AddHttpClient<ITodoService, TodosService>(client =>
{
    client.BaseAddress =
        new Uri("https://dummyjson.com/todos");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
// You can remove this check if you want Swagger available in staging/production environment.
{
    app.UseSwagger();   // Serves the generated OpenAPI spec file as a JSON endpoint
    app.UseSwaggerUI(); // Serves the interactive web UI
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();