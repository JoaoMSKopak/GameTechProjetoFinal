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

        [Key]
        public int ProdTID { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "O {0} deve ter no mínimo {2} caracteres", MinimumLength = 3)]
        public string ProdTNome { get; set; }
        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Plataforma")]
        [StringLength(20)]
        public string ProdTPlat { get; set; }
        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Gênero")]
        [StringLength(20)]
        public string ProdTGen { get; set; }
        public bool Trocar { get; set; }
        public int UsuAtualID { get; set; }
        public Usuario UsuarioAtual { get; set; }

        public ICollection<Proposta> Propostas { get; set; }


    }
}