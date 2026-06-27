namespace Backend.Models;

public class OtherEvent
{
    public DateOnly Date { get; set; }
    public string Location { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
