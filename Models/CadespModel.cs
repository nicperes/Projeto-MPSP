using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("CadespModel")]
    public class CadespModel
    {
        [Key]
        [Column("CnpjCadesp")]
        public long CNPJ { get; set; }

        [Column("IE")]
        [Display(Name = "IE")]
        public string IE { get; set; }

        [Column("Situacao")]
        [Display(Name = "Situacao")]
        public string Situacao { get; set; }

        [Column("DataInscricao")]
        [Display(Name = "DataInscricao")]
        public string DataInscricao { get; set; }


        [Column("NomeEmpresarial")]
        [Display(Name = "NomeEmpresarial")]
        public string NomeEmpresarial { get; set; }


        [Column("RegimeEstadual")]
        [Display(Name = "RegimeEstadual")]
        public string RegimeEstadual { get; set; }


        [Column("DRT")]
        [Display(Name = "DRT")]
        public string DRT { get; set; }


        [Column("PostoFiscal")]
        [Display(Name = "PostoFiscal")]
        public string PostoFiscal { get; set; }


        [Column("Nire")]
        [Display(Name = "Nire")]
        public string Nire { get; set; }

        [Column("OcorrenciaFiscal")]
        [Display(Name = "OcorrenciaFiscal")]
        public string OcorrenciaFiscal { get; set; }


        [Column("TipoUnidade")]
        [Display(Name = "TipoUnidade")]
        public string TipoUnidade { get; set; }


        [Column("FormasAtuacao")]
        [Display(Name = "FormasAtuacao")]
        public string FormasAtuacao { get; set; }
    }
}