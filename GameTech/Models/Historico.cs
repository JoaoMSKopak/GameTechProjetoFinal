using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameTech.Models
{
    public class Historico
    {
        public Historico()
        {
            Usuarios = new HashSet<Usuario>();       
        }

        [Key]
        public int HistID { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

        public virtual ICollection<Prod_Venda> Prod_Vendas { get; set; }
    }
}