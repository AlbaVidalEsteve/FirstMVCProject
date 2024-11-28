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
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                UsuarioDAL dal = new UsuarioDAL();
                Usuario usuario = new Usuario();

                usuario.UserName = model.UserName;
                usuario.Password = model.Password;

                dal.CreateUsuario(usuario);

                Usuario validarCreacion = dal.GetUsuarioLogin(model.UserName, model.Password);
                // Validar usuario
                if(validarCreacion != null)
                {
                    HttpContext.Session.SetString("UserName", usuario.UserName);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "No se ha podido crear usuario");
            }
            return View(model);
        }
    }
}
