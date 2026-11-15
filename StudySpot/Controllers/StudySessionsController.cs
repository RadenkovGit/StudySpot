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
    public async Task<IActionResult> Index(int? placeId)
    {
        if (!ModelState.IsValid)
        {
            return NotFound();
        }

        var model = new StudySessionHistoryViewModel { PlaceId = placeId };

        if (placeId.HasValue)
        {
            model.PlaceName = await studySessionService.GetPlaceNameAsync(placeId.Value);

            if (model.PlaceName == null)
            {
                return NotFound();
            }
        }

        model.Sessions = await studySessionService.GetHistoryAsync(placeId);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var session = await studySessionService.GetAsync(id);

        if (session == null)
        {
            return NotFound();
        }

        return View(session);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
    {
        if (!await studySessionService.DeleteAsync(id))
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "The study session was deleted.";

        return RedirectToAction(nameof(Index));
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
