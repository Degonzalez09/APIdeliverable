using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

app.MapGet("/todoitems", async (TodoDb db) =>
    await db.Todos.ToListAsync());

app.MapGet("/todoitems/complete", async (TodoDb db) =>
    await db.Todos.Where(t => t.IsComplete).ToListAsync());

app.MapGet("/todoitems/{id}", async (int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
        is Todo todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/todoitems", async (Todo todo, TodoDb db) =>
{
    db.Todos.Add(todo);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", todo);
});

app.MapPut("/todoitems/{id}", async (int id, Todo inputTodo, TodoDb db) =>
{
    var todo = await db.Todos.FindAsync(id);

    if (todo is null) return Results.NotFound();

    todo.Name = inputTodo.Name;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/todoitems/{id}", async (int id, TodoDb db) =>
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }

    return Results.NotFound();
});


app.MapGet("/employees", async (TodoDb db) =>
await db.Employees.ToListAsync());

app.MapGet("/employees/{id}" , async (int id, TodoDb db) =>
await db.Employees.FindAsync(id)
is Employee employee
? Results.Ok(employee)
: Results.NotFound());

app.MapPost("/employees", async (Employee employee, TodoDb db) =>
{
    db.Employees.Add(employee);
    await db.SaveChangesAsync();
    return Results.Created($"/employees/{employee.Id}" , employee);
});

app.MapGet("/managers", async (TodoDb db) =>
await db.Managers.ToListAsync());

app.MapGet("/managers/{id}", async (int id, TodoDb db) =>
await db.Managers.FindAsync(id)
is Manager manager 
? Results.Ok(manager)
: Results.NotFound());

app.MapPost("/managers", async (Manager manager, TodoDb db) =>
{
    db.Managers.Add(manager);
    await db.SaveChangesAsync();
    return Results.Created($"/managers/{manager.Id}" , manager);
});

app.MapGet("/superadmins", async (int id, TodoDb db) =>
await db.SuperAdmins.FindAsync(id)
is SuperAdmin superAdmin
? Results.Ok(superAdmin)
: Results.NotFound());

app.MapPost("/superadmins", async (SuperAdmin superAdmin, TodoDb db) =>
{
    db.SuperAdmins.Add(superAdmin);
    await db.SaveChangesAsync();
    return Results.Created($"/superadmins/{superAdmin.Id}", superAdmin);
});

app.Run();