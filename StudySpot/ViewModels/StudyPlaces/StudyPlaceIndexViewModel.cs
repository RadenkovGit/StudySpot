using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudySpot.ViewModels.StudyPlaces;

public class StudyPlaceIndexViewModel
{
    public string? Search { get; set; }

    public int? PlaceTypeId { get; set; }

    public bool HasFilters => !string.IsNullOrWhiteSpace(Search) || PlaceTypeId.HasValue;

    public IEnumerable<SelectListItem> PlaceTypes { get; set; } = new List<SelectListItem>();

    public IReadOnlyList<StudyPlaceListItemViewModel> Places { get; set; } = new List<StudyPlaceListItemViewModel>();
}
