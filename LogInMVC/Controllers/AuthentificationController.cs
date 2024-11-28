using LogInMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LogInMVC.Controllers
{
    public class AuthentificationController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Validar usuari
                if (model.Username == "admin" && model.Password == "password")
                {
                    //Autentificación exitosa
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            
            }
            return View(model);
        }
    }
}
