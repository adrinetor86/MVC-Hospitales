using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;

namespace MvcCoreAdoNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DatosPersona()
        {
            Persona persona = new Persona();
            persona.Nombre = "Adrian";
            persona.Apellido = "Jacek";
            return View(persona);
        }

   
    }
}
