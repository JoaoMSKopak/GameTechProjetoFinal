using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameTech.Contexts;
using GameTech.Models;

namespace GameTech.Controllers
{
    public class CarrinhoController : Controller
    {
        //Instânciação de uma variável do tipo EFContext
        private EFContext db = new EFContext();


        // GET: Carrinho
        public ActionResult Index()
        {
            return View();
        }

        //GET: Comprar
        public ActionResult Comprar(int id)
        {
            //Se o valor da sessão for nulo
            if(Session["cart"] == null)
            {
                //Processo para adicionar um produto ao carrinho
                PVModel pVModel = new PVModel();
                List<Item> carrinho = new List<Item>();
                Item item = new Item();
                Prod_Venda prod_Venda = db.Prod_Vendas.Find(id);
                item.Prod_Venda = prod_Venda;
                item.Quantidade += 1;
                carrinho.Add(item);
                Session["cart"] = carrinho;
            }
            //Do contrário
            else
            {
                //Processo para aumentar a quantidade do produto
                PVModel pVModel = new PVModel();
                List<Item> carrinho = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    carrinho[index].Quantidade++;
                }
                else
                {
                    Item item = new Item();
                    Prod_Venda prod_Venda = db.Prod_Vendas.Find(id);
                    item.Prod_Venda = prod_Venda;
                    item.Quantidade += 1;
                    carrinho.Add(item);
                }
                Session["cart"] = carrinho;
            }
            return RedirectToAction("Index");
        }
        //GET: Remover
        public ActionResult Remover(int id)
        {
            //Procura pelo produto no carrinho pela posição no array e se achar remove o jogo do carrinho
            List<Item> carrinho = (List<Item>)Session["cart"];
            int index = isExist(id);
            carrinho.RemoveAt(index);
            Session["cart"] = carrinho;
            return RedirectToAction("Index");
        }

        //Método de verificação da existência do jogo no carrinho
        private int isExist(int id)
        {
            int i = 0;

            List<Item> carrinho = (List<Item>)Session["cart"];
            for (i=0; i < carrinho.Count; i++)
                if (carrinho[i].Prod_Venda.ProdVID.Equals(id))
                    return i;
            return -1;
        }
    }
}