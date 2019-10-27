using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("ConsultaAnterior")]
    public class PesquisaCPFCNPJ
    {
        [Key]
        [Column("CPFCNPJ")]
        public long CPFCNPJ { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("Processo")]
        [Display(Name = "Processo")]
        public string Processo { get; set; }

        [Column("Arpensp")]
        [Display(Name = "Arpensp")]
        public string Arpensp { get; set; }

        [Column("Cadesp")]
        [Display(Name = "Cadesp")]
        public string Cadesp { get; set; }

        [Column("Caged")]
        [Display(Name = "Caged")]
        public string Caged { get; set; }

        [Column("Jucesp")]
        [Display(Name = "Jucesp")]
        public string Jucesp { get; set; }

        [Column("Detran")]
        [Display(Name = "Detran")]
        public string Detran { get; set; }

        [Column("Censec")]
        [Display(Name = "Censec")]
        public string Censec { get; set; }

        [Column("Siel")]
        [Display(Name = "Siel")]
        public string Siel { get; set; }

        [Column("Sivec")]
        [Display(Name = "Sivec")]
        public string Sivec { get; set; }

        [NotMapped]
        public ArpenspModel ArpenspModel { get; set; }
        [NotMapped]
        public CadespModel CadespModel { get; set; }
        [NotMapped]
        public CagedModel CagedModel { get; set; }
        [NotMapped]
        public CensecModel CensecModel { get; set; }
        [NotMapped]
        public DetranModel DetranModel { get; set; }
        [NotMapped]
        public JucespModel JucespModel { get; set; }
        [NotMapped]
        public SielModel SielModel { get; set; }
        [NotMapped]
        public SivecModel SivecModel { get; set; }
    }
}