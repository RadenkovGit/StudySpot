using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using static StudySpot.Common.ValidationConstants;

namespace StudySpot.ViewModels.StudySessions;

public class StudySessionFormViewModel
{
    private string? notes;

    [Required(ErrorMessage = "Please select a place.")]
    [Display(Name = "Place")]
    public int? StudyPlaceId { get; set; }

    [Required(ErrorMessage = "Please enter the date of the session.")]
    public DateOnly? Date { get; set; }

    [Required(ErrorMessage = "Please enter the duration.")]
    [Range(SessionMinDurationMinutes, SessionMaxDurationMinutes)]
    [Display(Name = "Duration (minutes)")]
    public int? DurationMinutes { get; set; }

    [StringLength(SessionNotesMaxLength)]
    public string? Notes
    {
        get => notes;
        set => notes = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    public IEnumerable<SelectListItem> Places { get; set; } = new List<SelectListItem>();

    // Latest selectable date (today) in the yyyy-MM-dd format used by the date input.
    public string MaxDate { get; set; } = string.Empty;
}
