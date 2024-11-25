using FirstMVCProject.DAL;
using FirstMVCProject.Models;
using FirstMVCProject.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstMVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
                
        public IActionResult Index()
        {
            AnimalDAL dal = new AnimalDAL();
            AnimalesViewModel viewModel = new AnimalesViewModel();
            viewModel.Animals = dal.GetAll();

            return View(viewModel);
        }               

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
