using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StudySpot.ViewModels.StudyPlaces;

public class StudyPlaceEditViewModel : StudyPlaceFormViewModel
{
    [BindNever]
    public int Id { get; set; }
}
