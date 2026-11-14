using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static StudySpot.Common.ValidationConstants;

namespace StudySpot.ViewModels.StudyPlaces;

public class StudyPlaceFormViewModel
{
    private string name = string.Empty;
    private string location = string.Empty;
    private string? notes;

    // Text is trimmed on binding so that length rules apply to what is actually stored.
    [Required]
    [StringLength(PlaceNameMaxLength, MinimumLength = PlaceNameMinLength)]
    public string Name
    {
        get => name;
        set => name = value?.Trim() ?? string.Empty;
    }

    [Required]
    [StringLength(PlaceLocationMaxLength, MinimumLength = PlaceLocationMinLength)]
    public string Location
    {
        get => location;
        set => location = value?.Trim() ?? string.Empty;
    }

    [StringLength(PlaceNotesMaxLength)]
    public string? Notes
    {
        get => notes;
        set => notes = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    [Display(Name = "Wi-Fi available")]
    public bool HasWifi { get; set; }

    [Display(Name = "Power outlets available")]
    public bool HasPowerOutlets { get; set; }

    [Required(ErrorMessage = "Please select a place type.")]
    [Display(Name = "Place type")]
    public int? PlaceTypeId { get; set; }

    public IEnumerable<SelectListItem> PlaceTypes { get; set; } = new List<SelectListItem>();
}
