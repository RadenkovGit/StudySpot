using StudySpot.ViewModels.StudyPlaces;

namespace StudySpot.Services;

public interface IStudyPlaceService
{
    Task<IReadOnlyList<StudyPlaceListItemViewModel>> GetAllAsync(string? search, int? placeTypeId);

    Task<StudyPlaceDetailsViewModel?> GetDetailsAsync(int id);

    Task<IReadOnlyList<PlaceTypeOptionViewModel>> GetPlaceTypesAsync();

    Task<bool> PlaceTypeExistsAsync(int placeTypeId);

    Task<int> CreateAsync(StudyPlaceFormViewModel model);

    Task<bool> ExistsAsync(int id);

    Task<StudyPlaceEditViewModel?> GetForEditAsync(int id);

    Task<bool> UpdateAsync(StudyPlaceEditViewModel model);

    Task<StudyPlaceDeleteViewModel?> GetForDeleteAsync(int id);

    Task<bool> DeleteAsync(int id);
}
