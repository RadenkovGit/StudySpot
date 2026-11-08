using System.ComponentModel.DataAnnotations;
using static StudySpot.Common.ValidationConstants;

namespace StudySpot.Data.Entities;

public class StudySession
{
    public int Id { get; set; }

    public int StudyPlaceId { get; set; }

    public StudyPlace StudyPlace { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int DurationMinutes { get; set; }

    [MaxLength(SessionNotesMaxLength)]
    public string? Notes { get; set; }
}
