using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudySpot.Models;
using StudySpot.Services;

namespace StudySpot.Controllers;

public class HomeController : Controller
{
    private const int RecentSessionsCount = 5;

    private readonly IStudySessionService studySessionService;

    public HomeController(IStudySessionService studySessionService)
    {
        this.studySessionService = studySessionService;
    }

    public async Task<IActionResult> Index()
    {
        var recentSessions = await studySessionService.GetRecentAsync(RecentSessionsCount);

        return View(recentSessions);
    }

    public IActionResult Status(int code)
    {
        if (code == StatusCodes.Status404NotFound)
        {
            return View("NotFound");
        }

        return Error();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
