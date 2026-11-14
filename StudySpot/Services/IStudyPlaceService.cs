using StudySpot.ViewModels.StudyPlaces;

namespace StudySpot.Services;

public interface IStudyPlaceService
{
    Task<IReadOnlyList<StudyPlaceListItemViewModel>> GetAllAsync();

    Task<StudyPlaceDetailsViewModel?> GetDetailsAsync(int id);

    Task<IReadOnlyList<PlaceTypeOptionViewModel>> GetPlaceTypesAsync();

    Task<bool> PlaceTypeExistsAsync(int placeTypeId);

    Task<int> CreateAsync(StudyPlaceFormViewModel model);
}
