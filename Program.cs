using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseStaticFiles();
app.UseDefaultFiles();

using var db = new ApplicationContext();

app.MapGet("/api/tasks", () => db.Tasks.ToListAsync());

app.MapGet("/", async (context) => await context.Response.SendFileAsync("wwwroot/index.html"));

app.MapPost("api/tasks/upload", async (tasks.Task [] tasks) =>
{
    foreach (var task in tasks)
    {
        await db.Tasks.AddAsync(task);
    }
    await db.SaveChangesAsync();
    return tasks;
});

app.MapPost("api/tasks", async (tasks.Task task) =>
{
    await db.Tasks.AddAsync(task);
    await db.SaveChangesAsync();
    return task;
});

app.MapDelete("api/tasks/{id:int}", async(int id) =>
{
    tasks.Task? task = await db.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    if (task == null) return Results.NotFound("Task not found");

    db.Tasks.Remove(task);
    await db.SaveChangesAsync();
    return Results.Json(task);
});

app.Run();
