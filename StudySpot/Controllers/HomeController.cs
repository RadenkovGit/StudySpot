using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudySpot.Models;

namespace StudySpot.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
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
