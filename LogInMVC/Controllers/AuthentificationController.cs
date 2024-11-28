using LogInMVC.DAL;
using LogInMVC.Models;
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
                UsuarioDAL dal = new UsuarioDAL();
                Usuario usuario = dal.GetUsuarioLogin(model.Username, model.Password);
                //Validar usuari
                if (usuario != null)
                {
                    HttpContext.Session.SetString("Username", model.Username);
                    //Autentificación exitosa
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            
            }
            return View(model);
        }
    }
}
