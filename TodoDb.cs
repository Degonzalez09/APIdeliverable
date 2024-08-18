using Microsoft.EntityFrameworkCore;

class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options)
        : base(options) { }

    public DbSet<Todo> Todos => Set<Todo>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Manager> Managers => Set<Manager>();
    public DbSet<SuperAdmin> SuperAdmins => Set<SuperAdmin>();
}