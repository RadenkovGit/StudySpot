namespace StudySpot.ViewModels.StudyPlaces;

public class StudyPlaceDetailsViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string? Notes { get; set; }

    public string PlaceTypeName { get; set; } = string.Empty;

    public bool HasWifi { get; set; }

    public bool HasPowerOutlets { get; set; }

    public int SessionsCount { get; set; }

    public int TotalMinutes { get; set; }
}
