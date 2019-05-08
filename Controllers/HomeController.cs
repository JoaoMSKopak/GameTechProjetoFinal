using GameTech.Models;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameTech.Contexts;
using System.Security.Claims;
using System;
using System.Net;
using System.Data.Entity;

namespace GameTech.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private EFContext context = new EFContext();
        // GET: Home
        public ActionResult Index()
        {
            //Verifica se o usuário está logado
            if (User.Identity.IsAuthenticated)
            {
                // Se estiver é exibida uma mensagem de boas vindas
                ViewBag.Mensagem = "Bem-Vindo";
            }
            return View();
        }
        public ActionResult Index2(string actionname, string controller, int id)
        {
            return View("Index");
        }

        // Autenticação de usuário
        public void Auth(Usuario myUser)
        {
            // Identidade do usuário
            var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, myUser.NomeUsu)
                },
                "ApplicationCookie");

            // Contexto da autenticação
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            // SignIn (autenticação de login)
            authManager.SignIn(identity);
        }

        // GET: Cadastro
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cadastro
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                // Busca se já existe um usuário cadastrado com o mesmo nome de usuário.
                var user = context.Usuarios.SingleOrDefault(u => u.NomeUsu == usuario.NomeUsu);

                // Se já existe um usuário.
                if (user != null)
                {
                    ModelState.AddModelError("", "Nome de usuário já existe.");
                    return View();
                }

                else
                {
                    // Verificação de idade

                    // Se cria uma variável chama zerotime(tempo zero)
                    DateTime zeroTime = new DateTime(1, 1, 1);

                    /* Subtração da data de hoje pela data de nascimento do usuário
                     * para se obter o período de tempo que se passou
                     */
                    TimeSpan span = DateTime.Now.Subtract(usuario.DataNasc);

                    /* Uma variável chamada years(anos) recebe a soma do tempo zero com
                    o período de tempo menos 1(já que o calendário gregoriano começa no ano 1)
                    */
                    int years = (zeroTime + span).Year - 1;

                    // Se o valor da variável for maior ou igual a 18
                    if (years >= 18)
                    {
                        // Se faz o cadastro do usuário e volta para a página inicial
                        context.Usuarios.Add(usuario);
                        context.SaveChanges();

                        return RedirectToAction("Index");
                    }

                    // Do contrário
                    else
                    {
                        //Mostra um erro e recarrega a página
                        ModelState.AddModelError("", "Somente maiores de idade podem se cadastrar");
                        return View();
                    }


                }

            }



            return View();
        }

        // GET: Login
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            var user = new Login
            {
                ReturnUrl = returnUrl
            };
            return View(user);
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            // Verifica se os campos da view são válidos 
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Persiste o banco procurando pelo usuário digitado
            var myUser = context.Usuarios.SingleOrDefault(u => u.NomeUsu == login.NomeUsu);

            if (myUser == null)
            {
                ModelState.AddModelError("", "Usuário ou senha inválidos");
                return View();
            }

            if (login.NomeUsu == myUser.NomeUsu && login.Senha == myUser.Senha)
            {
                Auth(myUser);
                int id = myUser.UsuarioId;
                return Redirect(GetRedirectUrl(login.ReturnUrl, id));
            }

            //Falha na autenticação
            ModelState.AddModelError("", "Usuário ou senha inválidos");


            return View();

        }

        // Método que pega o RedirectUrl de acordo com sucesso ou falha do login do usuário.
        private string GetRedirectUrl(string returnUrl, int id)
        {
            // Caso o usuário tenha feito login
            if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
            {
                return Url.Action("Index", "Home", id);
            }
            // Caso o usuário tenha um login inválido (volta para a mesma URL atual (action Login))
            return returnUrl;
        }

        // Método de logout
        public ActionResult LogOut()
        {
            // Contexto da autenticação
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            // SignOut(Desloga o usuário)
            authManager.SignOut("ApplicationCookie");

            // Retorna para a view Home
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios/Editar/5
        [HttpGet]
        public ActionResult Editar(/*int? id*/)
        {
            //Usuario usuario = context.Usuarios.Find(id);
            //if (id == null)
            //{
            //    return new
            //        HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}            
            //if (usuario == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // POST: Usuarios/Editar/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Editar(Usuario usuario)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        context.Entry(usuario).State = 
        //            EntityState.Modified;
        //        context.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(usuario);
        //}

        public ActionResult Inicio()
        {
            return View();
        }

        public ActionResult Detalhes()
        {
            return View();
        }

        public ActionResult AddJogo()
        {
            return View();
        }

        public ActionResult Compra()
        {
            return View();
        }

        public ActionResult Carrinho()
        {
            return View();
        }

        public ActionResult Aviso()
        {
            return View();
        }

        public ActionResult Aluguel()
        {
            return View();
        }
    }
}
