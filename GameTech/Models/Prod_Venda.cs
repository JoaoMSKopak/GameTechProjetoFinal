using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameTech.Models
{
    public class Prod_Venda
    {
        public Prod_Venda()
        {
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int ProdVID { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR")]
        public string ProdVNome { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string ProdVPlat { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string ProdVGen { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }


    }
}