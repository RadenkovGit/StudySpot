using System.ComponentModel.DataAnnotations;
using static StudySpot.Common.ValidationConstants;

namespace StudySpot.Data.Entities;

public class StudyPlace
{
    public int Id { get; set; }

    [Required]
    [MaxLength(PlaceNameMaxLength)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(PlaceLocationMaxLength)]
    public string Location { get; set; } = string.Empty;

    [MaxLength(PlaceNotesMaxLength)]
    public string? Notes { get; set; }

    public bool HasWifi { get; set; }

    public bool HasPowerOutlets { get; set; }

    public int PlaceTypeId { get; set; }

    public PlaceType PlaceType { get; set; } = null!;

    public ICollection<StudySession> StudySessions { get; set; } = new List<StudySession>();
}
