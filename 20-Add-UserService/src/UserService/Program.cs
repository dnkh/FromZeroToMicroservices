using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/users", Results<Ok<IList<User>>, NoContent> () =>
{     
    var users = GetUsers();
    //return TypedResults.Ok(users);

    return users.Count > 0
        ? TypedResults.Ok(users)
        : TypedResults.NoContent();
        
// new Microsoft.AspNetCore.Mvc.ProblemDetails
//     {
//         Title = "Not Found",
//         Detail = "The requested resource was not found.",
//         Status = StatusCodes.Status404NotFound,
//         Type = "https://httpstatuses.com/404",
//     });
})
.WithName("GetUsers")
.WithOpenApi();

app.Run();

IList<User> GetUsers() => new[]
{
    new User(1, "John Doe"),
    new User(2, "Jane Doe"),
};

// IList<User> GetUsers() => new List<User>();

public record User(int Id, string Name);

