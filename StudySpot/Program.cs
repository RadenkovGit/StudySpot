using Microsoft.EntityFrameworkCore;
using StudySpot.Data;
using StudySpot.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<StudySpotDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
        // SQL Server 2019 (the LocalDB version used for development) supports level 150.
        sqlOptions.UseCompatibilityLevel(150)));

builder.Services.AddScoped<IStudyPlaceService, StudyPlaceService>();
builder.Services.AddScoped<IStudySessionService, StudySessionService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Status", "?code={0}");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
