using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameTech.Models
{
    public class Prod_Troca
    {
        public Prod_Troca()
        {
            Usuarios = new HashSet<Usuario>();
        }

        [Key]
        public int ProdTID { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR")]
        public string ProdTNome { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string ProdTPlat { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string ProdTGen { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

    }
}