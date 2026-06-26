namespace Backend.Models;

public class RunEvent
{
    public DateOnly Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
