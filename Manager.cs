public class Manager : Employee
{
    public int TeamSize { get; set; }
    public string? Department { get; set; }

    public Manager()
    {
        JobTitle = "Manager";
    }
}