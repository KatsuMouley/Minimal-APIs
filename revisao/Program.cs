using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using revisao.models;
using REVISAO.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<Context>();
var app = builder.Build();

app.MapGet("/", ()=> "KatsuMouley was here");

app.MapPost("/add_user", async ([FromBody]User user, [FromServices]Context context) =>
{
    context.Add(user);
    await context.SaveChangesAsync();
    return Results.Created($"/get_user/{user.name}", user);
});

app.MapGet("/get_user/{nome}", async (string nome, Context context) =>
{
    var users = await context.TabelaUsers.Where(p => p.name.Contains(nome)).ToListAsync();
    return users.Any() ? Results.Ok(users) : Results.NotFound("Nenhum usuário encontrado.");
});

app.MapGet("/get_users", async (Context context)=>{
    var produtos = await context.TabelaUsers.ToListAsync();
    return Results.Ok(produtos);
});

app.Run();