using Microsoft.EntityFrameworkCore;
using StudySpot.Data;
using StudySpot.Data.Entities;
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

    public async Task<IReadOnlyList<PlaceTypeOptionViewModel>> GetPlaceTypesAsync()
    {
        return await context.PlaceTypes
            .AsNoTracking()
            .OrderBy(t => t.Id)
            .Select(t => new PlaceTypeOptionViewModel { Id = t.Id, Name = t.Name })
            .ToListAsync();
    }

    public async Task<bool> PlaceTypeExistsAsync(int placeTypeId)
    {
        return await context.PlaceTypes.AnyAsync(t => t.Id == placeTypeId);
    }

    public async Task<int> CreateAsync(StudyPlaceFormViewModel model)
    {
        var place = new StudyPlace
        {
            Name = model.Name,
            Location = model.Location,
            Notes = model.Notes,
            HasWifi = model.HasWifi,
            HasPowerOutlets = model.HasPowerOutlets,
            PlaceTypeId = model.PlaceTypeId!.Value
        };

        context.StudyPlaces.Add(place);
        await context.SaveChangesAsync();

        return place.Id;
    }
}
