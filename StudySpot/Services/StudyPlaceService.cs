using Microsoft.EntityFrameworkCore;
using StudySpot.Data;
using StudySpot.ViewModels.StudyPlaces;

namespace StudySpot.Services;

public class StudyPlaceService : IStudyPlaceService
{
    private readonly StudySpotDbContext context;

    public StudyPlaceService(StudySpotDbContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyList<StudyPlaceListItemViewModel>> GetAllAsync()
    {
        return await context.StudyPlaces
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ThenBy(p => p.Id)
            .Select(p => new StudyPlaceListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Location = p.Location,
                PlaceTypeName = p.PlaceType.Name,
                HasWifi = p.HasWifi,
                HasPowerOutlets = p.HasPowerOutlets
            })
            .ToListAsync();
    }

    public async Task<StudyPlaceDetailsViewModel?> GetDetailsAsync(int id)
    {
        return await context.StudyPlaces
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new StudyPlaceDetailsViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Location = p.Location,
                Notes = p.Notes,
                PlaceTypeName = p.PlaceType.Name,
                HasWifi = p.HasWifi,
                HasPowerOutlets = p.HasPowerOutlets
            })
            .FirstOrDefaultAsync();
    }
}
