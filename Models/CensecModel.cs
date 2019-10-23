using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{

    [Table("CensecModel")]
    public class CensecModel
    {

        [Key]
        [Column("CNPJCensec")]
        public long CNPJCensec { get; set; }


        [Column("Carga")]
        [Display(Name = "Carga")]
        public string Carga { get; set; }

        [Column("Mes")]
        [Display(Name = "Mes")]
        public string Mes { get; set; }

        [Column("Ano")]
        [Display(Name = "Ano")]
        public string Ano { get; set; }

        [Column("Ato")]
        [Display(Name = "Ato")]
        public string Ato { get; set; }

        [Column("DiaAto")]
        [Display(Name = "DiaAto")]
        public string DiaAto { get; set; }

        [Column("MesAto")]
        [Display(Name = "MesAto")]
        public string MesAto { get; set; }

        [Column("AnoAto")]
        [Display(Name = "AnoAto")]
        public string AnoAto { get; set; }

        [Column("Livro")]
        [Display(Name = "Livro")]
        public string Livro { get; set; }

        [Column("Folha")]
        [Display(Name = "Folha")]
        public string Folha { get; set; }

        [Column("NomesPartes")]
        [Display(Name = "NomesPartes")]
        public String NomesPartes { get; set; }

        [Column("CpfCnpjPartes")]
        [Display(Name = "CpfCnpjPartes")]
        public String CpfCnpjPartes { get; set; }

        [Column("QualidadePartes")]
        [Display(Name = "QualidadePartes")]
        public String QualidadePartes { get; set; }

        [Column("UF")]
        [Display(Name = "UF")]
        public String UF { get; set; }

        [Column("Municipio")]
        [Display(Name = "Municipio")]
        public String Municipio { get; set; }

        [Column("Cartorio")]
        [Display(Name = "Cartorio")]
        public String Cartorio { get; set; }

        [Column("TelefoneCartorio")]
        [Display(Name = "TelefoneCartorio")]
        public String TelefoneCartorio { get; set; }

        [Column("TipoTelefoneCartorio")]
        [Display(Name = "TipoTelefoneCartorio")]
        public String TipoTelefoneCartorio { get; set; }

        [Column("ContatoCartorio")]
        [Display(Name = "ContatoCartorio")]
        public String ContatoCartorio { get; set; }

        [Column("StatusCartorio")]
        [Display(Name = "StatusCartorio")]
        public String StatusCartorio { get; set; }

    }


    }