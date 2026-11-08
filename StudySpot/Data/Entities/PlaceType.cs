using System.ComponentModel.DataAnnotations;
using static StudySpot.Common.ValidationConstants;

namespace StudySpot.Data.Entities;

public class PlaceType
{
    public int Id { get; set; }

    [Required]
    [MaxLength(PlaceTypeNameMaxLength)]
    public string Name { get; set; } = string.Empty;

    public ICollection<StudyPlace> StudyPlaces { get; set; } = new List<StudyPlace>();
}
