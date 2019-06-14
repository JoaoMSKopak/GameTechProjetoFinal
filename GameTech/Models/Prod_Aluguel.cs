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
        [Key]
        public int ProdAID { get; set; }
        [Required]
        [Display(Name = "Nome")]
        [Column(TypeName = "VARCHAR")]
        [StringLength(50, ErrorMessage = "O {0} deve ter no mínimo {2} caracteres", MinimumLength = 3)]
        public string ProdANome { get; set; }
        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Plataforma")]
        [StringLength(20)]
        public string ProdAPlat { get; set; }
        [Display(Name = "Gênero")]
        [StringLength(20)]
        [Column(TypeName = "VARCHAR")]
        public string ProdAGen { get; set; }
        public bool Alugado { get; set; }
        public int UsuAtualID { get; set; }
        public Usuario UsuarioAtual { get; set; }
        [Display(Name = "Duração da locação")]
        public string DuracLoc { get; set; }


    }
}