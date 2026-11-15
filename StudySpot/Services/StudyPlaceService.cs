using Microsoft.EntityFrameworkCore;
using StudySpot.Data;
using StudySpot.Data.Entities;
using StudySpot.ViewModels.StudyPlaces;
using static StudySpot.Common.ValidationConstants;

namespace StudySpot.Services;

public class StudyPlaceService : IStudyPlaceService
{
    private readonly StudySpotDbContext context;

    public StudyPlaceService(StudySpotDbContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyList<StudyPlaceListItemViewModel>> GetAllAsync(string? search, int? placeTypeId)
    {
        var query = context.StudyPlaces.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();

            // No name or location is this long, and a longer term would exceed the SQL parameter size.
            if (term.Length > Math.Max(PlaceNameMaxLength, PlaceLocationMaxLength))
            {
                return new List<StudyPlaceListItemViewModel>();
            }

            query = query.Where(p => p.Name.Contains(term) || p.Location.Contains(term));
        }

        if (placeTypeId.HasValue)
        {
            query = query.Where(p => p.PlaceTypeId == placeTypeId.Value);
        }

        return await query
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

    public async Task<bool> ExistsAsync(int id)
    {
        return await context.StudyPlaces.AnyAsync(p => p.Id == id);
    }

    public async Task<StudyPlaceEditViewModel?> GetForEditAsync(int id)
    {
        return await context.StudyPlaces
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new StudyPlaceEditViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Location = p.Location,
                Notes = p.Notes,
                HasWifi = p.HasWifi,
                HasPowerOutlets = p.HasPowerOutlets,
                PlaceTypeId = p.PlaceTypeId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(StudyPlaceEditViewModel model)
    {
        var place = await context.StudyPlaces.FindAsync(model.Id);

        if (place == null)
        {
            return false;
        }

        place.Name = model.Name;
        place.Location = model.Location;
        place.Notes = model.Notes;
        place.HasWifi = model.HasWifi;
        place.HasPowerOutlets = model.HasPowerOutlets;
        place.PlaceTypeId = model.PlaceTypeId!.Value;

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<StudyPlaceDeleteViewModel?> GetForDeleteAsync(int id)
    {
        return await context.StudyPlaces
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new StudyPlaceDeleteViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Location = p.Location,
                PlaceTypeName = p.PlaceType.Name,
                SessionsCount = p.StudySessions.Count
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // The sessions of the place are removed by the ON DELETE CASCADE foreign key.
        var deletedRows = await context.StudyPlaces
            .Where(p => p.Id == id)
            .ExecuteDeleteAsync();

        return deletedRows > 0;
    }
}
