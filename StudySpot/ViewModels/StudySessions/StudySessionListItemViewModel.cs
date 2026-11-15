namespace StudySpot.ViewModels.StudySessions;

public class StudySessionListItemViewModel
{
    public int Id { get; set; }

    public int StudyPlaceId { get; set; }

    public string PlaceName { get; set; } = string.Empty;

    public DateOnly Date { get; set; }

    public int DurationMinutes { get; set; }

    public string? Notes { get; set; }
}
