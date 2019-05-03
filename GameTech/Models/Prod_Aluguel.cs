using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameTech.Models
{
    public class Prod_Aluguel
    {
        public Prod_Aluguel()
        {
            Usuarios = new HashSet<Usuario>();
        }
        [Key]
        public int ProdAID { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR")]
        public string ProdANome { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string ProdAPlat { get; set; }
        [Column(TypeName = "VARCHAR")]
        public string ProdAGen { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }

    }
}