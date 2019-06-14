using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using GameTech.Contexts;
using GameTech.Models;

namespace GameTech.Controllers
{
    public class LocacaoController : Controller
    {
        //Instânciação de uma variável do tipo EFContext
        private EFContext db = new EFContext();

        // GET: Locacao
        public ActionResult Index()
        {
            //Incluí o campo UsuarioAtual a prod_Aluguels
            var prod_Aluguels = db.Prod_Aluguels.Include(p => p.UsuarioAtual);
            return View(prod_Aluguels.ToList());
        }

        // GET: Locacao/Details/5
        public ActionResult Details(int? id)
        {
            //Se não houver nenhum valor de id
            if (id == null)
            {
                //Retorna a messagem de bad request
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod_Aluguel prod_Aluguel = db.Prod_Aluguels.Find(id);
            if (prod_Aluguel == null)
            {
                return HttpNotFound();
            }
            return View(prod_Aluguel);
        }

        // GET: Locacao/Create
        public ActionResult Create()
        {
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu");
            return View();
        }

        // POST: Locacao/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdAID,ProdANome,ProdAPlat,ProdAGen,Alugado,UsuAtualID,DuracLoc")] Prod_Aluguel prod_Aluguel)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);


                prod_Aluguel.UsuAtualID = idLogado;
                db.Prod_Aluguels.Add(prod_Aluguel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu", prod_Aluguel.UsuAtualID);
            return View(prod_Aluguel);
        }

        // GET: Locacao/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod_Aluguel prod_Aluguel = db.Prod_Aluguels.Find(id);
            if (prod_Aluguel == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu", prod_Aluguel.UsuAtualID);
            return View(prod_Aluguel);
        }

        // POST: Locacao/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdAID,ProdANome,ProdAPlat,ProdAGen,Alugado,UsuAtualID,DuracLoc")] Prod_Aluguel prod_Aluguel)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
                prod_Aluguel.UsuAtualID = idLogado;
                db.Entry(prod_Aluguel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu", prod_Aluguel.UsuAtualID);
            return View(prod_Aluguel);
        }

        // GET: Locacao/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod_Aluguel prod_Aluguel = db.Prod_Aluguels.Find(id);
            if (prod_Aluguel == null)
            {
                return HttpNotFound();
            }
            return View(prod_Aluguel);
        }

        // POST: Locacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prod_Aluguel prod_Aluguel = db.Prod_Aluguels.Find(id);
            db.Prod_Aluguels.Remove(prod_Aluguel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpGet]
        public ActionResult Alugar(int id)
        {
            Prod_Aluguel prod = db.Prod_Aluguels.Find(id);
            return View(prod);
        }
        [HttpPost]
        public ActionResult Alugar(Prod_Aluguel prod)
        {
            var identity = (ClaimsIdentity)User.Identity;
            int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
            prod.UsuAtualID = idLogado;
            Prod_Aluguel prod1 = db.Prod_Aluguels.Where(p => p.ProdAID == prod.ProdAID).FirstOrDefault();
            //prod1.Alugado = false;
            ViewBag.MSG = "Produto já alugado";
            if (prod1.Alugado == true)
            {
                ViewBag.Mensagem = "Jogo já alugado";
                return View(prod);
            }

            else
            {
                prod1.Alugado = true;
                prod1.DuracLoc = prod.DuracLoc;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Devolver(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
            //prod.UsuAtualID = idLogado;
            Prod_Aluguel prod1 = db.Prod_Aluguels.Where(p => p.ProdAID == id).FirstOrDefault();
            prod1.Alugado = false;
            prod1.DuracLoc = null;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}
