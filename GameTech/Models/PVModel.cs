using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameTech.Models
{
    public class PVModel
    {
        private List<Prod_Venda> prodsv;

        public PVModel()
        {
            prodsv = new List<Prod_Venda>();
        }

        public Prod_Venda achar(string id)
        {
            return prodsv.Single(p => p.ProdVID.Equals(id));
        }

        public List<Prod_Venda> acharTodos()
        {
            return prodsv;
        }
    }

    
}