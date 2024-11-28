using FirstMVCProject.DAL;
using FirstMVCProject.Models;
using FirstMVCProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

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
            DetailsAnimalViewModel vm = new DetailsAnimalViewModel();
            vm.AnimalDetail = _animalDAL.GetById(id);
            TempData["Animal"] = JsonConvert.SerializeObject(vm);

            return RedirectToAction("Details", "Animal", new { id });
        }

        [HttpGet]
        public ActionResult Details()
        {
            if (TempData["Animal"] != null)
            {
                var json = TempData["Animal"] as string;
                var vm = JsonConvert.DeserializeObject<DetailsAnimalViewModel>(json);
                if (vm == null ||vm.AnimalDetail.IdAnimal ==0 ) 
                {
                    ViewBag.NoAnimal = "No se ha encontrado ningún animal.";
                
                }
                return View(vm);
            }
            return RedirectToAction("Index", "Home"); //Redirigir si no hay datos           
        }
        
        //public IActionResult ShowAnimalCreate()
        //{
            
        //    return RedirectToAction("Form", "Animal");
        //}

        [HttpPost]
        public ActionResult ShowAnimalEdit(int idAnimal)
        {
            Animal animalToEdit = _animalDAL.GetById(idAnimal);
            if (animalToEdit == null)
            {
                ViewBag.NoAnimal = "No se ha encontrado ningún animal.";
            }
            
            TempData["Animal"] = JsonConvert.SerializeObject(animalToEdit);
            return RedirectToAction("Form", "Animal");
        }

        [HttpGet]
        public IActionResult Form()
        {
            var json = TempData["Animal"] as string;
            var animalToEdit = JsonConvert.DeserializeObject<Animal>(json);
            if (animalToEdit == null) //Desde el boton create
            {
                var tiposAnimales = _tipoAnimalDAL.GetAll();
                ViewBag.TipoAnimales = new SelectList(tiposAnimales, "IdTipoAnimal", "TipoDescripcion");

                return View(new Animal());
            }
            else //Desde el boton edit
            {
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
