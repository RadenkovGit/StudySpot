using Microsoft.EntityFrameworkCore;
using StudySpot.Data;
using StudySpot.Data.Entities;
using StudySpot.ViewModels.StudySessions;

namespace StudySpot.Services;

public class StudySessionService : IStudySessionService
{
    private readonly StudySpotDbContext context;

    public StudySessionService(StudySpotDbContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyList<PlaceOptionViewModel>> GetPlaceOptionsAsync()
    {
        return await context.StudyPlaces
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ThenBy(p => p.Id)
            .Select(p => new PlaceOptionViewModel { Id = p.Id, Name = p.Name })
            .ToListAsync();
    }

    public async Task<bool> PlaceExistsAsync(int placeId)
    {
        return await context.StudyPlaces.AnyAsync(p => p.Id == placeId);
    }

    public async Task CreateAsync(StudySessionFormViewModel model)
    {
        var session = new StudySession
        {
            StudyPlaceId = model.StudyPlaceId!.Value,
            Date = model.Date!.Value,
            DurationMinutes = model.DurationMinutes!.Value,
            Notes = model.Notes
        };

        context.StudySessions.Add(session);
        await context.SaveChangesAsync();
    }
}
