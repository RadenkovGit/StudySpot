namespace StudySpot.ViewModels.StudyPlaces;

public class StudyPlaceListItemViewModel
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public string PlaceTypeName { get; set; } = string.Empty;

    public bool HasWifi { get; set; }

    public bool HasPowerOutlets { get; set; }
}
