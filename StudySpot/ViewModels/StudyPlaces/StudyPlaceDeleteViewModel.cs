namespace StudySpot.ViewModels.StudyPlaces;

public class StudyPlaceDeleteViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string PlaceTypeName { get; set; } = string.Empty;

    public int SessionsCount { get; set; }
}
