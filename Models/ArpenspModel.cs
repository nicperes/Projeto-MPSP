using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{


    [Table("ArpenspModel")]
    public class ArpenspModel
    {
        [Key]
        [Column("CNPJCPF")]
        public long CNPJCPFArpensp { get; set; }


        [Column("CartorioRegistro")]
        [Display(Name = "CartorioRegistro")]
        public string CartorioRegistro { get; set; }


        [Column("NumCNS")]
        [Display(Name = "NumCNS")]
        public string NumCNS { get; set; }


        [Column("UF")]
        [Display(Name = "UF")]
        public string UF { get; set; }

        [Column("NomeConj")]
        [Display(Name = "NomeConj")]
        public string NomeConj { get; set; }


        [Column("NovoNomeConj")]
        [Display(Name = "NovoNomeConj")]
        public string NovoNomeConj { get; set; }



        [Column("NomeConj2")]
        [Display(Name = "NomeConj2")]
        public string NomeConj2 { get; set; }


        [Column("NovoNomeConj2")]
        [Display(Name = "NovoNomeConj2")]
        public string NovoNomeConj2 { get; set; }


        [Column("DataCasamento")]
        [Display(Name = "DataCasamento")]
        public string DataCasamento { get; set; }


        [Column("Matricula")]
        [Display(Name = "Matricula")]
        public string Matricula { get; set; }


        [Column("DataEntrada")]
        [Display(Name = "DataEntrada")]
        public string DataEntrada { get; set; }


        [Column("DataRegistro")]
        [Display(Name = "DataRegistro")]
        public string DataRegistro { get; set; }


        public long CnpjJucesp { get; set; }
        [ForeignKey("CnpjJucesp")]
        public JucespModel JucespModel { get; set; }


        public long CnpjCadesp { get; set; }
        [ForeignKey("CnpjCadesp")]
        public CadespModel CadespModel { get; set; }


        public long CnpjCensec { get; set; }
        [ForeignKey("CnpjCensec")]
        public CensecModel CensecModel { get; set; }

        public long CnpjCaged { get; set; }
        [ForeignKey("CnpjCaged")]
        public CagedModel CagedModel { get; set; }

        public long CnpjDetran { get; set; }
        [ForeignKey("CnpjDetran")]
        public DetranModel DetranModel { get; set; }

        public long CnpjSiel { get; set; }
        [ForeignKey("CnpjSiel")]
        public SielModel SielModel { get; set; }

        public long CnpjSivec { get; set; }
        [ForeignKey("CnpjSivec")]
        public SivecModel SivecModel { get; set; }

        public ArpenspModel()
        {

        }

        public ArpenspModel(long Cnpjcpf, String Nome)
        {
            this.CNPJCPFArpensp = CNPJCPFArpensp;


        }


    }

}
