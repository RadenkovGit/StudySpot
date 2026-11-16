using StudySpot.ViewModels.StudySessions;

namespace StudySpot.Services;

public interface IStudySessionService
{
    Task<IReadOnlyList<PlaceOptionViewModel>> GetPlaceOptionsAsync();

    Task<bool> PlaceExistsAsync(int placeId);

    Task CreateAsync(StudySessionFormViewModel model);

    Task<string?> GetPlaceNameAsync(int placeId);

    Task<IReadOnlyList<StudySessionListItemViewModel>> GetHistoryAsync(int? placeId);

    Task<IReadOnlyList<StudySessionListItemViewModel>> GetRecentAsync(int count);

    Task<StudySessionListItemViewModel?> GetAsync(int id);

    Task<bool> DeleteAsync(int id);
}
