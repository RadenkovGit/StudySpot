namespace StudySpot.ViewModels.StudySessions;

public class StudySessionHistoryViewModel
{
    // Set when the history is limited to a single place.
    public int? PlaceId { get; set; }

    public string? PlaceName { get; set; }

    public IReadOnlyList<StudySessionListItemViewModel> Sessions { get; set; } = new List<StudySessionListItemViewModel>();
}
