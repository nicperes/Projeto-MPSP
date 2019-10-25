using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("SielModel")]
    public class SielModel
    {

        [Key]
        [Column("CNPJSiel")]
        public long CNPJCaged { get; set; }

        [Column("Titulo ")]
        [Display(Name = "Titulo ")]
        public string Titulo { get; set; }

        [Column("Zona ")]
        [Display(Name = "Zona ")]
        public string Zona { get; set; }


        [Column("DataDomicilio ")]
        [Display(Name = "DataDomicilio ")]
        public string DataDomicilio { get; set; }

       
    }
}
