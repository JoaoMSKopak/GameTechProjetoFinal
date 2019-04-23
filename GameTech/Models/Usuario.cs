using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Mvc;

namespace GameTech.Models
{
   

    public class Usuario
    {
      

        

        public int UsuarioId { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(20,ErrorMessage = "O {0} deve ter no mínimo {2} caracteres e no máximo {1} caracteres", MinimumLength = 6)]
        [Display(Name = "Nome de Usuário")]
        public string NomeUsu { get; set; }
        [Column(TypeName = "VARCHAR")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50, ErrorMessage = "O {0} deve ter no mínimo {2} caracteres", MinimumLength = 6)]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Senha")]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres e no máximo {1} caracteres", MinimumLength = 6)]
        [Column(TypeName = "VARCHAR")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "Senha de Confirmação")]
        [Column(TypeName = "VARCHAR")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres e no máximo {1} caracteres", MinimumLength = 6)]
        [System.ComponentModel.DataAnnotations.Compare("Senha", ErrorMessage = "A senha e a senha de confirmação não correspondem.")]
        public string ConSenha { get; set; }

        //[Required]
        [Column(TypeName = "VARCHAR")]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }
        //[Required]
        [Display(Name = "Telefone")]
        [DisplayFormat(DataFormatString = "{0:(##) ####-####}", ApplyFormatInEditMode = true)]
        public long Tel { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Column(TypeName = "DATE")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]

        public DateTime DataNasc { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
        
    }    
}