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
    public class VendaController : Controller
    {
        private EFContext db = new EFContext();

        // GET: Venda
        public ActionResult Index(string ordClass)
        {
            ViewBag.ParamClassNome = String.IsNullOrEmpty(ordClass) ? "nome_desc" : "";
            ViewBag.ParamClassPlat = ordClass == "Plataforma" ? "plat_desc" : "Plataforma";
            var prod_vendas = from p in db.Prod_Vendas
                               select p;
            switch (ordClass)
            {
                case "nome_desc":
                    prod_vendas = prod_vendas.OrderByDescending(p => p.ProdVNome);
                    break;
                case "Plataforma":
                    prod_vendas = prod_vendas.OrderBy(p => p.ProdVPlat);
                    break;
                case "plat_desc":
                    prod_vendas = prod_vendas.OrderByDescending(p => p.ProdVPlat);
                    break;
                default:
                    prod_vendas = prod_vendas.OrderBy(p => p.ProdVID);
                    break;
            }
            PVModel pVModel = new PVModel();
            ViewBag.products = pVModel.acharTodos();
            prod_vendas = db.Prod_Vendas.Include(p => p.UsuarioAtual);
            return View(db.Prod_Vendas.ToList());
        }

        // GET: Venda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod_Venda prod_Venda = db.Prod_Vendas.Find(id);
            prod_Venda.UsuarioAtual = db.Usuarios.FirstOrDefault(u => u.UsuarioId == prod_Venda.UsuAtualID);
            if (prod_Venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu", prod_Venda.UsuAtualID);
            return View(prod_Venda);
        }

        // GET: Venda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Venda/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdVID,ProdVNome,ProdVPlat,ProdVGen,ProdVPrec,UsuAtualID")] Prod_Venda prod_Venda)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);

                prod_Venda.UsuAtualID = idLogado;
                db.Prod_Vendas.Add(prod_Venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prod_Venda);
        }

        // GET: Venda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod_Venda prod_Venda = db.Prod_Vendas.Find(id);
            if (prod_Venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "Usuario", "NomeUsu", prod_Venda.UsuAtualID);
            return View(prod_Venda);
        }

        // POST: Venda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdVID,ProdVNome,ProdVPlat,ProdVGen,ProdVPrec,UsuAtualID")] Prod_Venda prod_Venda)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
                prod_Venda.UsuAtualID = idLogado;
                db.Entry(prod_Venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "Usuario", "NomeUsu", prod_Venda.UsuAtualID);
            return View(prod_Venda);
        }

        // GET: Venda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod_Venda prod_Venda = db.Prod_Vendas.Find(id);
            if (prod_Venda == null)
            {
                return HttpNotFound();
            }
            return View(prod_Venda);
        }

        // POST: Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prod_Venda prod_Venda = db.Prod_Vendas.Find(id);
            db.Prod_Vendas.Remove(prod_Venda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult VisualizarProdutosVenda()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
            var prod_Vendas = db.Prod_Vendas.Include(p => p.UsuarioAtual);
            return View(prod_Vendas.ToList().Where(u => u.UsuAtualID == idLogado));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
