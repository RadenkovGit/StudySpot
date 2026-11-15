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

    public async Task<string?> GetPlaceNameAsync(int placeId)
    {
        return await context.StudyPlaces
            .AsNoTracking()
            .Where(p => p.Id == placeId)
            .Select(p => p.Name)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<StudySessionListItemViewModel>> GetHistoryAsync(int? placeId)
    {
        var query = context.StudySessions.AsNoTracking();

        if (placeId.HasValue)
        {
            query = query.Where(s => s.StudyPlaceId == placeId.Value);
        }

        return await query
            .OrderByDescending(s => s.Date)
            .ThenByDescending(s => s.Id)
            .Select(s => new StudySessionListItemViewModel
            {
                Id = s.Id,
                StudyPlaceId = s.StudyPlaceId,
                PlaceName = s.StudyPlace.Name,
                Date = s.Date,
                DurationMinutes = s.DurationMinutes,
                Notes = s.Notes
            })
            .ToListAsync();
    }

    public async Task<StudySessionListItemViewModel?> GetAsync(int id)
    {
        return await context.StudySessions
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new StudySessionListItemViewModel
            {
                Id = s.Id,
                StudyPlaceId = s.StudyPlaceId,
                PlaceName = s.StudyPlace.Name,
                Date = s.Date,
                DurationMinutes = s.DurationMinutes,
                Notes = s.Notes
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deletedRows = await context.StudySessions
            .Where(s => s.Id == id)
            .ExecuteDeleteAsync();

        return deletedRows > 0;
    }
}
