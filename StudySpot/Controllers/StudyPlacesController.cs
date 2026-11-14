using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudySpot.Services;
using StudySpot.ViewModels.StudyPlaces;

namespace StudySpot.Controllers;

public class StudyPlacesController : Controller
{
    private readonly IStudyPlaceService studyPlaceService;

    public StudyPlacesController(IStudyPlaceService studyPlaceService)
    {
        this.studyPlaceService = studyPlaceService;
    }

    public async Task<IActionResult> Index()
    {
        var places = await studyPlaceService.GetAllAsync();

        return View(places);
    }

    public async Task<IActionResult> Details(int id)
    {
        var place = await studyPlaceService.GetDetailsAsync(id);

        if (place == null)
        {
            return NotFound();
        }

        return View(place);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new StudyPlaceFormViewModel();
        await LoadPlaceTypesAsync(model);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StudyPlaceFormViewModel model)
    {
        if (model.PlaceTypeId.HasValue && !await studyPlaceService.PlaceTypeExistsAsync(model.PlaceTypeId.Value))
        {
            ModelState.AddModelError(nameof(model.PlaceTypeId), "Please select a valid place type.");
        }

        if (!ModelState.IsValid)
        {
            await LoadPlaceTypesAsync(model);

            return View(model);
        }

        var id = await studyPlaceService.CreateAsync(model);
        TempData["SuccessMessage"] = "The study place was added.";

        return RedirectToAction(nameof(Details), new { id });
    }

    private async Task LoadPlaceTypesAsync(StudyPlaceFormViewModel model)
    {
        var placeTypes = await studyPlaceService.GetPlaceTypesAsync();

        model.PlaceTypes = placeTypes
            .Select(t => new SelectListItem(t.Name, t.Id.ToString(), t.Id == model.PlaceTypeId))
            .ToList();
    }
}
