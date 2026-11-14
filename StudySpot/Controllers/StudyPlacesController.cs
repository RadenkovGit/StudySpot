using Microsoft.AspNetCore.Mvc;
using StudySpot.Services;

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
}
