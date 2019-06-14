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
    public class TrocaController : Controller
    {
        private EFContext db = new EFContext();
        public Prod_Troca prod = new Prod_Troca();
        public Proposta prop = new Proposta();

        // GET: Troca
        public ActionResult Index()
        {
            var prod_Trocas = db.Prod_Trocas.Include(p => p.UsuarioAtual);
            return View(prod_Trocas.ToList());
        }

        // GET: Troca/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            Prod_Troca prod_Troca = db.Prod_Trocas.Find(id);
            prod_Troca.UsuarioAtual = db.Usuarios.FirstOrDefault(u => u.UsuarioId == prod_Troca.UsuAtualID);
            if (prod_Troca == null)
            {
                return HttpNotFound();
            }

            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu", prod_Troca.UsuAtualID);
            return View(prod_Troca);
        }

        // GET: Troca/Create
        public ActionResult Create()
        {
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu");
            return View();
        }

        // POST: Troca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdTID,ProdTNome,ProdTPlat,ProdTGen,Trocar,UsuAtualID")] Prod_Troca prod_Troca)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);


                prod_Troca.UsuAtualID = idLogado;
                db.Prod_Trocas.Add(prod_Troca);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu", prod_Troca.UsuAtualID);
            return View(prod_Troca);
        }

        // GET: Troca/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod_Troca prod_Troca = db.Prod_Trocas.Find(id);
            if (prod_Troca == null)
            {
                return HttpNotFound();
            }
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu", prod_Troca.UsuAtualID);
            return View(prod_Troca);
        }

        // POST: Troca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdTID,ProdTNome,ProdTPlat,ProdTGen")] Prod_Troca prod_Troca)
        {
            if (ModelState.IsValid)
            {
                var identity = (ClaimsIdentity)User.Identity;
                int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);


                prod_Troca.UsuAtualID = idLogado;
                db.Entry(prod_Troca).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UsuAtualID = new SelectList(db.Usuarios, "UsuarioId", "NomeUsu", prod_Troca.UsuAtualID);
            return View(prod_Troca);
        }

        // GET: Troca/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prod_Troca prod_Troca = db.Prod_Trocas.Find(id);
            if (prod_Troca == null)
            {
                return HttpNotFound();
            }
            return View(prod_Troca);
        }

        // POST: Troca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prod_Troca prod_Troca = db.Prod_Trocas.Find(id);
            db.Prod_Trocas.Remove(prod_Troca);
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
        public ActionResult Trocar(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
            Prod_Troca prod_Troca = db.Prod_Trocas.Find(id);
            var iddono = prod_Troca.UsuAtualID;
            Proposta proposta = new Proposta();
            proposta.UsuAtualID = idLogado;
            proposta.ProdTAtualID = prod_Troca.ProdTID;
            proposta.Prod_Troca = prod_Troca;
            proposta.UsuarioReceb = db.Usuarios.Where(u => u.UsuarioId == iddono).FirstOrDefault();
            proposta.Prod_Para_Trocar = db.Prod_Trocas.Where(p => p.UsuAtualID == idLogado).ToList();

            return View(proposta);
        }

        public ActionResult FazerProposta(int idProdProposta, int idUsuLog, int idProdTroca, int idUsuarioTroca)
        {
            //var identity = (ClaimsIdentity)User.Identity;

            //Prod_Troca prod_Troca = db.Prod_Trocas.Find(id);
            //proposta.Prod_Para_Trocar.Where(p => p.ProdTID == id).FirstOrDefault().Trocar = true;
            //db.Propostas.Add(proposta);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            Proposta proposta = new Proposta();
            proposta.UsuAtualID = idUsuLog;
            proposta.ProdTAtualID = idProdTroca;
            proposta.UsuarioReceb = db.Usuarios.Where(u => u.UsuarioId == idUsuarioTroca).FirstOrDefault();
            proposta.UsuarioReceb.UsuarioId = idUsuarioTroca;
            proposta.Prod_Para_Trocar = new List<Prod_Troca>();

            Prod_Troca pt = db.Prod_Trocas.Where(p => p.ProdTID == idProdProposta).FirstOrDefault();
            pt.Trocar = true;
            proposta.Prod_Para_Trocar.Add(pt);
            //proposta.Prod_Para_Trocar.Add(db.Prod_Trocas.ToList().FirstOrDefault(p => p.ProdTID == idProdProposta));
            proposta.Prod_P_TrocarID = idProdProposta;
            db.Propostas.Add(proposta);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult VisualizarProdutosTroca()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
            var prod_Trocas = db.Prod_Trocas.Include(p => p.UsuarioAtual);
            return View(prod_Trocas.ToList().Where(u => u.UsuAtualID == idLogado));
        }

        public ActionResult VisualizarProposta()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);

            List<Proposta> propostas = db.Propostas.Where(p => p.UsuarioReceb.UsuarioId == idLogado).ToList();

            foreach (Proposta prop in propostas)
            {
                Prod_Troca prodTroca = db.Database.SqlQuery<Prod_Troca>("select prd.ProdTID, prd.ProdTNome, prd.ProdTPlat, prd.ProdTGen, prd.Trocar, prd.UsuAtualID from Prod_Troca prd where prd.ProdTID=" + prop.Prod_P_TrocarID + " and prd.UsuAtualID =" + prop.UsuAtualID).FirstOrDefault();
                if (prodTroca != null)
                {
                    prop.Prod_Troca = prodTroca;
                    prop.ProdTAtualID = prodTroca.ProdTID;
                }

                Prod_Troca prodOutro = db.Database.SqlQuery<Prod_Troca>("select prd.ProdTID, prd.ProdTNome, prd.ProdTPlat, prd.ProdTGen, prd.Trocar, prd.UsuAtualID from  Propostas p, Prod_Troca prd where p.ProdTAtualID = prd.ProdTID and prd.UsuAtualID = p.UsuarioReceb_UsuarioId and p.PropostaID=" + prop.PropostaID).FirstOrDefault();
                if (prodOutro != null)
                {
                    prop.Prod_Para_Trocar = new List<Prod_Troca>() { prodOutro };
                    prop.Prod_P_TrocarID = prodOutro.ProdTID;
                }

                prop.UsuarioReceb = db.Usuarios.Where(u => u.UsuarioId == idLogado).FirstOrDefault();
                prop.UsuarioAtual = db.Database.SqlQuery<Usuario>("select u.UsuarioId, u.NomeUsu, u.Email, u.Endereco, u.Tel, u.DataNasc, u.ReturnURL,'xxxx' as Senha from Propostas p, Usuarios u where u.UsuarioId = p.UsuAtualID and p.PropostaID=" + prop.PropostaID).FirstOrDefault();

            }


            return View(propostas);

            //var identity = (ClaimsIdentity)User.Identity;
            //int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);
            //var propostas = db.Propostas.Include(u =>u.UsuarioAtual).Include(ur=>ur.UsuarioReceb).Include(p=>p.Prod_Troca)
            //    .Include(pt=>pt.Prod_Para_Trocar).Where(u => u.UsuarioReceb.UsuarioId == idLogado);
            ////var prop = db.Propostas.Where(u => u.UsuAtualID == idLogado).
            ////    Include(u => u.UsuAtualID == idLogado)/*.Include(p => p.Prod_Troca).Include(pt=>pt.Prod_Para_Trocar)*/;

            ////return View(propostas/*.FirstOrDefault()*/);
            //return View(propostas.ToList().Where(u => u.UsuarioReceb.UsuarioId == idLogado));
        }

        public ActionResult VisualizarPropostaFeita()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int idLogado = int.Parse(identity.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value);

            List<Proposta> propostas = db.Propostas.Where(p => p.UsuAtualID == idLogado).ToList();

            foreach (Proposta prop in propostas)
            {
                Prod_Troca prodTroca = db.Database.SqlQuery<Prod_Troca>("select prd.ProdTID, prd.ProdTNome, prd.ProdTPlat, prd.ProdTGen, prd.Trocar, prd.UsuAtualID from Prod_Troca prd where prd.ProdTID=" + prop.Prod_P_TrocarID + " and prd.UsuAtualID =" + prop.UsuAtualID).FirstOrDefault();
                if (prodTroca != null)
                {
                    prop.Prod_Para_Trocar = new List<Prod_Troca>() { prodTroca };
                    prop.Prod_P_TrocarID = prodTroca.ProdTID;
                }

                Prod_Troca prodOutro = db.Database.SqlQuery<Prod_Troca>("select prd.ProdTID, prd.ProdTNome, prd.ProdTPlat, prd.ProdTGen, prd.Trocar, prd.UsuAtualID from  Propostas p, Prod_Troca prd where p.ProdTAtualID = prd.ProdTID and prd.UsuAtualID = p.UsuarioReceb_UsuarioId and p.PropostaID=" + prop.PropostaID).FirstOrDefault();
                if (prodOutro != null)
                {
                    prop.Prod_Troca = prodOutro;
                    prop.ProdTAtualID = prodOutro.ProdTID;
                }

                prop.UsuarioAtual = db.Usuarios.Where(u => u.UsuarioId == idLogado).FirstOrDefault();
                prop.UsuarioReceb = db.Database.SqlQuery<Usuario>("select u.UsuarioId, u.NomeUsu, u.Email, u.Endereco, u.Tel, u.DataNasc, u.ReturnURL,'xxxx' as Senha from Propostas p, Usuarios u where u.UsuarioId = p.UsuarioReceb_UsuarioId and p.PropostaID=" + prop.PropostaID).FirstOrDefault();

            }


            return View(propostas);
        }

        public ActionResult AceitarProposta(int idProposta, int idUsuarioAt, int idUsuRec, int idProdTroc, int idProdProp)
        {
            Prod_Troca prod1 = db.Prod_Trocas.Where(p => p.ProdTID == idProdProp).FirstOrDefault();
            Prod_Troca prod2 = db.Prod_Trocas.Where(pt => pt.ProdTID == idProdTroc).FirstOrDefault();

            prod1.UsuAtualID = idUsuRec;
            prod2.UsuAtualID = idUsuarioAt;
            prod1.Trocar = false;
            prod2.Trocar = false;
            db.SaveChanges();
            return RedirectToAction("VisualizarProdutosTroca");
        }

        public ActionResult RecusarProposta(int idProposta, int idUsuarioAt, int idUsuRec, int idProdTroc, int idProdProp)
        {
            Prod_Troca prod1 = db.Prod_Trocas.Where(p => p.ProdTID == idProdProp).FirstOrDefault();
            Prod_Troca prod2 = db.Prod_Trocas.Where(pt => pt.ProdTID == idProdTroc).FirstOrDefault();


            prod1.Trocar = false;
            prod2.Trocar = false;
            return RedirectToAction("VisualizarProdutosTroca");
        }
    }
}