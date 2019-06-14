using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameTech.Models
{
    public class Proposta
    {
        [Key]
        [Display(Name = "Código da proposta")]
        public int PropostaID { get; set; }

        public int UsuAtualID { get; set; }
        [Display(Name = "Usuário negociador")]
        public Usuario UsuarioAtual { get; set; }

        //[ForeignKey("UsuarioReceb")]
        //public int UsuarioRecebID { get; set; }
        [Display(Name = "Usuário recebedor")]
        public Usuario UsuarioReceb { get; set; }

        
        public int ProdTAtualID { get; set; }
        //[ForeignKey("ProdTAtualID")]
        [Display(Name = "Produto do UR")]
        public Prod_Troca Prod_Troca { get; set; }

        public int Prod_P_TrocarID { get; set; }
        [Display(Name = "Seu produto")]
        public IList<Prod_Troca> Prod_Para_Trocar { get; set; }


    }
}