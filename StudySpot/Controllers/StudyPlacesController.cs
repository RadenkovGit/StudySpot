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
        await ValidatePlaceTypeAsync(model);

        if (!ModelState.IsValid)
        {
            await LoadPlaceTypesAsync(model);

            return View(model);
        }

        var id = await studyPlaceService.CreateAsync(model);
        TempData["SuccessMessage"] = "The study place was added.";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await studyPlaceService.GetForEditAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        await LoadPlaceTypesAsync(model);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([FromRoute] int id, StudyPlaceEditViewModel model)
    {
        if (!await studyPlaceService.ExistsAsync(id))
        {
            return NotFound();
        }

        model.Id = id;

        await ValidatePlaceTypeAsync(model);

        if (!ModelState.IsValid)
        {
            await LoadPlaceTypesAsync(model);

            return View(model);
        }

        if (!await studyPlaceService.UpdateAsync(model))
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "The study place was updated.";

        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var model = await studyPlaceService.GetForDeleteAsync(id);

        if (model == null)
        {
            return NotFound();
        }

        return View(model);
    }

    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed([FromRoute] int id)
    {
        if (!await studyPlaceService.DeleteAsync(id))
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "The study place was deleted.";

        return RedirectToAction(nameof(Index));
    }

    private async Task ValidatePlaceTypeAsync(StudyPlaceFormViewModel model)
    {
        if (model.PlaceTypeId.HasValue && !await studyPlaceService.PlaceTypeExistsAsync(model.PlaceTypeId.Value))
        {
            ModelState.AddModelError(nameof(model.PlaceTypeId), "Please select a valid place type.");
        }
    }

    private async Task LoadPlaceTypesAsync(StudyPlaceFormViewModel model)
    {
        var placeTypes = await studyPlaceService.GetPlaceTypesAsync();

        model.PlaceTypes = placeTypes
            .Select(t => new SelectListItem(t.Name, t.Id.ToString(), t.Id == model.PlaceTypeId))
            .ToList();
    }
}
