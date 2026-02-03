using Microsoft.AspNetCore.Mvc;
using MvcCrudPlantilla.Models;
using MvcCrudPlantilla.Repository;

namespace MvcCrudPlantilla.Controllers;

public class PlantillaController : Controller
{
    private RepositoryPlantilla repo;
    public PlantillaController()
    {
        repo = new RepositoryPlantilla();
    }
    
    public IActionResult Index()
    {
        List<Plantilla> plantillas = repo.GetPlantillas();
    
        return View(plantillas);
    }

    
    public IActionResult Details(int empleado_no)
    {

        Plantilla plantilla = repo.FindPlantillaById(empleado_no);
        
        return View(plantilla);
    }
    
    public IActionResult Update(int empleado_no)
    {
        Plantilla plantilla = repo.FindPlantillaById(empleado_no);
        return View(plantilla);
    }
    
     [HttpPost]
    public async Task<IActionResult> Update(int hospital_Cod,int sala_Cod,int empleado_no,
        string apellido,string funcion,string turno,int salario)
    {

        await repo.UpdatePlantillaAsync(hospital_Cod, sala_Cod, empleado_no, apellido, funcion, turno, salario);
        
        return RedirectToAction("Index");
    } 
    
    
    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Insert(int hospital_Cod,int sala_Cod,int empleado_no,
        string apellido,string funcion,string turno,int salario)
    {

        await repo.InsertPlantillaAsync(hospital_Cod, sala_Cod, empleado_no, apellido, funcion, turno, salario);
        
        return RedirectToAction("Index");
    }
    
    public IActionResult BuscadorPlantilla()
    {
        
         List<string> funciones = repo.GetFunciones();
         ViewData["FUNCIONES"] = funciones;
        return View();
    }
    
    [HttpPost]
    public IActionResult BuscadorPlantilla(string funcion)
    {
        ResumenPlantilla resumen = repo.GetPlantillasByFuncion(funcion);
        List<string> funciones = repo.GetFunciones();
        ViewData["FUNCIONES"] = funciones;
        
        return View(resumen);
    }
    
}