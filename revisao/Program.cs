using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using revisao.models;
using REVISAO.Models;

var builder = WebApplication.CreateBuilder(args);
// Habilita o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddDbContext<Context>();
var app = builder.Build();

app.MapGet("/", ()=> "KatsuMouley was here");
app.UseCors("AllowAll"); // Ativa o CORS para toda a API
app.MapPost("/add_user", async ([FromBody]User user, [FromServices]Context context) =>
{
    context.Add(user);
    await context.SaveChangesAsync();
    return Results.Created($"/get_user/{user.name}", user);
});

app.MapGet("/get_user/{id}", async (int id, Context context) =>
{
    var users = await context.TabelaUsers
                .Where(p => p.Id == id)
                .ToListAsync();

    return Results.Ok(users); // Adicionando "return" para enviar a resposta
});


app.MapGet("/get_users", async (Context context)=>{
    var produtos = await context.TabelaUsers.ToListAsync();
    return Results.Ok(produtos);
});

app.MapPut("/update_user/{id}", async (int id, [FromBody]User newUser, Context context) =>
{
    var oldUser = await context.TabelaUsers.FindAsync(id);
     if (oldUser == null)
    {
        return Results.NotFound("Usuario não encontrado.");
    }
    oldUser.Age = newUser.Age;
    oldUser.name = newUser.name;
    await context.SaveChangesAsync();
    return Results.Ok(oldUser);
});

app.MapDelete("/delete_user/{id}", async (int id, Context context)=>
{
    var user = await context.TabelaUsers.FindAsync(id);
    if(user == null)
    {
        return Results.NotFound("Produto não encontrado.");
    }
    context.TabelaUsers.Remove(user);
    await context.SaveChangesAsync();
    return Results.Ok($"produto de id {id} deletado com sucesso");
});

app.Run();