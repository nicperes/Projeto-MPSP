using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{


    [Table("ArpenspModel")]
    public class ArpenspModel
    {
        [Key]
        [Column("CnpjArpensp")]
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


        //public long Cnpjjucesp { get; set; }
        //[ForeignKey("cnpjjucesp")]
        //public JucespModel JucespModel { get; set; }


        //public long Cnpjcadesp { get; set; }
        //[ForeignKey("cnpjcadesp")]
        //public CadespModel CadespModel { get; set; }


        //public long Cnpjcensec { get; set; }
        //[ForeignKey("cnpjcensec")]
        //public CensecModel CensecModel { get; set; }

        //public long Cnpjcaged { get; set; }
        //[ForeignKey("cnpjcaged")]
        //public CagedModel CagedModel { get; set; }

        //public long Cnpjdetran { get; set; }
        //[ForeignKey("cnpjdetran")]
        //public DetranModel DetranModel { get; set; }

        //public long Cnpjsiel { get; set; }
        //[ForeignKey("cnpjsiel")]
        //public SielModel SielModel { get; set; }

        //public long Cnpjsivec { get; set; }
        //[ForeignKey("cnpjsivec")]
        //public SivecModel SivecModel { get; set; }

        public ArpenspModel()
        {

        }

        //public ArpenspModel(long Cnpjcpf, String Nome)
        //{
        //    this.CNPJCPFArpensp = CNPJCPFArpensp;


        //}


    }

}
