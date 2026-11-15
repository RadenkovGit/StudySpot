using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudySpot.Services;
using StudySpot.ViewModels.StudySessions;

namespace StudySpot.Controllers;

public class StudySessionsController : Controller
{
    private readonly IStudySessionService studySessionService;

    public StudySessionsController(IStudySessionService studySessionService)
    {
        this.studySessionService = studySessionService;
    }

    [HttpGet]
    public async Task<IActionResult> Create(int? placeId)
    {
        // A malformed or unknown placeId is an error, not something to ignore silently.
        if (!ModelState.IsValid || (placeId.HasValue && !await studySessionService.PlaceExistsAsync(placeId.Value)))
        {
            return NotFound();
        }

        var model = new StudySessionFormViewModel
        {
            StudyPlaceId = placeId,
            Date = Today
        };
        await LoadFormDataAsync(model);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudySessionFormViewModel model)
    {
        if (model.StudyPlaceId.HasValue && !await studySessionService.PlaceExistsAsync(model.StudyPlaceId.Value))
        {
            ModelState.AddModelError(nameof(model.StudyPlaceId), "Please select a valid place.");
        }

        if (model.Date.HasValue && model.Date.Value > Today)
        {
            ModelState.AddModelError(nameof(model.Date), "The date cannot be in the future.");
        }

        if (!ModelState.IsValid)
        {
            await LoadFormDataAsync(model);

            return View(model);
        }

        await studySessionService.CreateAsync(model);
        TempData["SuccessMessage"] = "The study session was recorded.";

        return RedirectToAction("Details", "StudyPlaces", new { id = model.StudyPlaceId });
    }

    private static DateOnly Today => DateOnly.FromDateTime(DateTime.Today);

    private async Task LoadFormDataAsync(StudySessionFormViewModel model)
    {
        var places = await studySessionService.GetPlaceOptionsAsync();

        model.Places = places
            .Select(p => new SelectListItem(p.Name, p.Id.ToString(), p.Id == model.StudyPlaceId))
            .ToList();
        model.MaxDate = Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}
