using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameTech.Models
{
    public class Prod_Venda
    {
       

        [Key]
        public int ProdVID { get; set; }
        [Required]
        [Display(Name = "Nome")]
        [StringLength(50, ErrorMessage = "O {0} deve ter no mínimo {2} caracteres", MinimumLength = 3)]
        [Column(TypeName = "VARCHAR")]
        public string ProdVNome { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        [Display(Name = "Plataforma")]
        public string ProdVPlat { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(20)]
        [Display(Name = "Gênero")]
        public string ProdVGen { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Preço")]
        public decimal ProdVPrec { get; set; }
        public int UsuAtualID { get; set; }
        public Usuario UsuarioAtual { get; set; }


    }
}