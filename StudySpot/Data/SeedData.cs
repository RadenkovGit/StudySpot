using StudySpot.Data.Entities;

namespace StudySpot.Data;

// Fixed ids keep the seed stable across migrations and repeated runs.
public static class SeedData
{
    public static readonly PlaceType[] PlaceTypes =
    {
        new() { Id = 1, Name = "Library" },
        new() { Id = 2, Name = "Cafe" },
        new() { Id = 3, Name = "University" },
        new() { Id = 4, Name = "Home" },
        new() { Id = 5, Name = "Other" }
    };

    public static readonly StudyPlace[] StudyPlaces =
    {
        new()
        {
            Id = 1,
            Name = "City Library Reading Room",
            Location = "Central Square, second floor",
            Notes = "Quiet at all times. Lockers at the entrance.",
            HasWifi = true,
            HasPowerOutlets = true,
            PlaceTypeId = 1
        },
        new()
        {
            Id = 2,
            Name = "Corner Cafe",
            Location = "Park Street 12",
            Notes = "Good for short sessions. Gets loud around noon.",
            HasWifi = true,
            HasPowerOutlets = false,
            PlaceTypeId = 2
        },
        new()
        {
            Id = 3,
            Name = "Faculty Study Hall",
            Location = "Main campus, building B, room 204",
            Notes = null,
            HasWifi = true,
            HasPowerOutlets = true,
            PlaceTypeId = 3
        },
        new()
        {
            Id = 4,
            Name = "Desk at Home",
            Location = "Home office",
            Notes = "Best for evening sessions.",
            HasWifi = true,
            HasPowerOutlets = true,
            PlaceTypeId = 4
        }
    };
}
