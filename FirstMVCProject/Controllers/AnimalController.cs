using FirstMVCProject.DAL;
using FirstMVCProject.Models;
using FirstMVCProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FirstMVCProject.Controllers
{
    public class AnimalController : Controller
    {

        private readonly AnimalDAL _animalDAL;
        private readonly TipoAnimalDAL _tipoAnimalDAL;

        public AnimalController()
        {
            _animalDAL = new AnimalDAL();
            _tipoAnimalDAL = new TipoAnimalDAL();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AnimalDetail(int id)
        {
            return RedirectToAction("Details", "Animal", new { id });
        }       

        [HttpGet]
        public ActionResult Details(int id)
        { 
            AnimalDAL dal = new AnimalDAL();
            DetailsAnimalViewModel vm = new DetailsAnimalViewModel();
            vm.AnimalDetail = dal.GetById(id);

            if (vm.AnimalDetail == null) 
            {
                return NotFound();
            }
            return View(vm);            
        }
        
        public IActionResult ShowAnimalCreate()
        {
            return RedirectToAction("Form", "Animal");
        }

        [HttpPost]
        public ActionResult ShowAnimalEdit(int idAnimal)
        {
            return RedirectToAction("Form", "Animal", new { idAnimal });
        }

        [HttpGet]
        public IActionResult Form(int idAnimal)
        {
            if (idAnimal == 0) //Desde el boton create
            {
                var tiposAnimales = _tipoAnimalDAL.GetAll();
                        
                ViewBag.TipoAnimales = new SelectList(tiposAnimales, "IdTipoAnimal", "TipoDescripcion");

                return View(new Animal());
            }
            else //Desde el boton edit
            {
                Animal animalToEdit = _animalDAL.GetById(idAnimal);

                if (animalToEdit == null)
                {
                    return NotFound();
                }

                var tiposAnimales = _tipoAnimalDAL.GetAll();

                ViewBag.TipoAnimales = new SelectList(tiposAnimales, "IdTipoAnimal", "TipoDescripcion", animalToEdit.RIdTipoAnimal);

                return View(animalToEdit);
            }
        }

        [HttpPost]
        public IActionResult SaveAnimal(Animal animal)
        {
            if (animal == null || string.IsNullOrEmpty(animal.NombreAnimal))
            {
                return View(animal); // Devolver la vista con los errores del formulario
            }

            if (animal.IdAnimal == 0) // Crear uno nuevo
            {
                _animalDAL.Insert(animal);
            }
            else // Update si ya tiene un id (ya está creado)
            {
                _animalDAL.Update(animal);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
