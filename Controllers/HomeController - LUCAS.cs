using ProvaFacil.Context;
using ProvaFacil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProvaFacil.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private UsuarioContext db = new UsuarioContext();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Mensagem = "Olá";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var usuario = new Usuario
            {
                ReturnUrl = returnUrl
            };

            return View(usuario);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = db.Usuario.SingleOrDefault<Usuario>(u => u.Login == usuario.Login);

            if (user == null)
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View();
            }

            if (usuario.Login == user.Login && usuario.Senha == user.Senha)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Login),
                },
                "ApplicationCookie");

                var ctx = Request.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return Redirect(GetRedirectUrl(usuario.ReturnUrl));
            }

            // Autenticação falhou
            ModelState.AddModelError("", "Usuário ou senha inválidos");
            return View();
        }

        private string GetRedirectUrl(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home");
            }

            return returnUrl;
        }
    }
}