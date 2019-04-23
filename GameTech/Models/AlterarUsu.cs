using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameTech.Models
{
    public class AlterarUsu
    {
        [Required]
        [Display(Name = "Nome de Usuário")]
        public string NomeUsu { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Tel { get; set; }

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DataNasc { get; set; }
    }
}