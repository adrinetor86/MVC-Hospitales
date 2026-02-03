using Microsoft.AspNetCore.Mvc;

namespace MvcCrudPlantilla.Controllers;

public class EventoController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}