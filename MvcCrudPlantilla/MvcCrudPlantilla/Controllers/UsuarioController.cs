using Microsoft.AspNetCore.Mvc;
using MvcCrudPlantilla.Models;
using MvcCrudPlantilla.Repository;

namespace MvcCrudPlantilla.Controllers;

public class UsuarioController : Controller
{
     RepositoryUsuarios repo;
    public UsuarioController()
    {
        repo = new RepositoryUsuarios();
    }
    
    // GET
    public IActionResult Index()
    {
        List<Usuario> usuarios = repo.GetUsuarios();
        return View(usuarios);
    } 
    
    public IActionResult Details(int idUsuario)
    {
        Usuario usuario = repo.GetDatosUsuario(idUsuario);
        return View(usuario);
    }
}