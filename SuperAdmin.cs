public class SuperAdmin : Manager
{
    public DateTime LastLogin { get; set; }
    public string? Permissions { get; set; }

    public SuperAdmin()
    {
        JobTitle = "SuperAdmin";
    }
}