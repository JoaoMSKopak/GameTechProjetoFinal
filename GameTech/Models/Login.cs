using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameTech.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Nome de Usuário")]
        [StringLength(20, ErrorMessage = "O {0} deve ter no mínimo {2} caracteres e no máximo {1} caracteres", MinimumLength = 6)]
        public string NomeUsu { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "A {0} deve ter no mínimo {2} caracteres e no máximo {1} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}