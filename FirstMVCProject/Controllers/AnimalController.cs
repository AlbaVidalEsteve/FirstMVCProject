using FirstMVCProject.DAL;
using FirstMVCProject.Models;
using FirstMVCProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVCProject.Controllers
{
    public class AnimalController : Controller
    {
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
        
        public ActionResult AnimalCreate()
        {
            return RedirectToAction("Create", "Animal");
        }

        [HttpPost]
        public ActionResult Create( ) 
        {
            AnimalDAL dal = new AnimalDAL();
            //dal.Insert(animal);
            //vm = dal.GetById(id);

            if (AnimalCreate == null)
            {
                return NotFound();
            }
            return View();
        }
    }
}
