using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Uniwebsidad.BaseDatos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Uniwebsidad.Controllers
{
    public class AuthController : Controller
    {
        private readonly CalidadContext _context;

        public AuthController(CalidadContext _context)
        {
            this._context = _context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var usuario = _context._Usuarios.FirstOrDefault(o => o.Username == username && o.Password == password);
            
            if (usuario != null)
            {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, username)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);


                HttpContext.SignInAsync(claimsPrincipal);
                
                return RedirectToAction("TodosLosCursos", "Curso");
            }
            
            //HttpContext.Response.StatusCode = 400;
            ViewBag.Validation = "Usuario y/o contraseña incorrecta";
            
            return View();
        }


        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("TodosLosCursosLogout", "Curso");
        }
    }
}