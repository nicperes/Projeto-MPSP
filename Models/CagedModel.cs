using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("CagedModel")]
    public class CagedModel
    {

        [Key]
        [Column("CnpjCaged")]
        public long CNPJCaged{ get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("Telefone")]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }


        [Column("Ramal")]
        [Display(Name = "Ramal")]
        public string Ramal { get; set; }

        [Column("Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
