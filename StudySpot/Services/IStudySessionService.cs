using StudySpot.ViewModels.StudySessions;

namespace StudySpot.Services;

public interface IStudySessionService
{
    Task<IReadOnlyList<PlaceOptionViewModel>> GetPlaceOptionsAsync();

    Task<bool> PlaceExistsAsync(int placeId);

    Task CreateAsync(StudySessionFormViewModel model);
}
