using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GameTech.Contexts;
using GameTech.Models;

namespace GameTech.Controllers
{
    public class Prod_VendaController : Controller
    {
        private EFContext db = new EFContext();

        // GET: Prod_Venda
        public ActionResult Index()
        {
            return View(db.Prod_Vendas.ToList());
        }

        // GET: Prod_Venda/Details/5
        public ActionResult Details(int? id)
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

        // GET: Prod_Venda/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prod_Venda/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdVID,ProdVNome,ProdVPlat,ProdVGen")] Prod_Venda prod_Venda)
        {
            if (ModelState.IsValid)
            {
                Usuario usr = db.Usuarios.Where(u => u.UsuarioId == 1).FirstOrDefault();
                prod_Venda.Usuarios.Add(usr);
                usr.Prod_Vendas.Add(prod_Venda);
                db.Prod_Vendas.Add(prod_Venda);
                db.Usuarios.Add(usr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prod_Venda);
        }

        // GET: Prod_Venda/Edit/5
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
            return View(prod_Venda);
        }

        // POST: Prod_Venda/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdVID,ProdVNome,ProdVPlat,ProdVGen")] Prod_Venda prod_Venda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prod_Venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prod_Venda);
        }

        // GET: Prod_Venda/Delete/5
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

        // POST: Prod_Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prod_Venda prod_Venda = db.Prod_Vendas.Find(id);
            db.Prod_Vendas.Remove(prod_Venda);
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
    }
}
