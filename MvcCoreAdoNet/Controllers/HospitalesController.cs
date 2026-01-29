using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Models;
using MvcCoreAdoNet.Repositories;

namespace MvcCoreAdoNet.Controllers
{
    public class HospitalesController : Controller
    {

        public RepositoryHospital repo;

        public HospitalesController()
        {
            this.repo = new RepositoryHospital();
        }
        public async Task<IActionResult> Index()
        {

            List<Hospital> hospitales =
                await this.repo.GetHospitalesAsync();
            return View(hospitales);
            
        }
        
        public async Task<IActionResult> Details(int id)
        {
            Hospital hospital = await this.repo.FindHospitalAsync(id);
            return View(hospital);
        }

        
        public async Task<IActionResult> Create()
        {
       
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Hospital hospital)
        {
            await this.repo.InsertHospitalAsync(hospital.IdHospital, hospital.Nombre, hospital.Direccion,
                hospital.Telefono, hospital.Camas);

            ViewBag.Mensaje = "Hospital Insertado";
            return View();
        }
        //public async Task<IActionResult> Edit(int id)
        //{
        //    Hospital hospital = await this.repo.FindHospitalAsync(id);
        //    return View(hospital);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(Hospital hospital)
        //{
        //    await this.repo.UpdateHospitalAsync(hospital.IdHospital, hospital.Nombre, hospital.Direccion,
        //        hospital.Telefono, hospital.Camas);

        //    ViewBag.Mensaje = "Hospital Modificado";

        //    return View();
        //}
        public async Task<IActionResult> Edit(int id)
        {
            Hospital hospital =
                await this.repo.FindHospitalAsync(id);
            return View(hospital);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Hospital hospital)
        {
            await this.repo.UpdateHospitalAsync
                (hospital.IdHospital, hospital.Nombre, hospital.Direccion
                , hospital.Telefono, hospital.Camas);
            ViewData["MENSAJE"] = "Hospital modificado";
            //DESPUES DE MODIFICAR, NOS VAMOS AL IACTIONRESULT DE INDEX
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this.repo.DeleteHospitalAsync(id);
            return View();
        }
    }
}
