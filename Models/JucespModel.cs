using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{

    [Table("JucespModel")]
    public class JucespModel {

        [Key]
        [Column("CnpjJucesp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CNPJJucesp { get; set; }

        [Column("Data")]
        [Display(Name = "Data")]
        public string Data { get; set; }

        [Column("Nome")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("NumMatriz")]
        [Display(Name = "NumMatriz")]
        public string NumMatriz { get; set; }

        [Column("TipoEmpresa")]
        [Display(Name = "TipoEmpresa")]
        public string TipoEmpresa { get; set; }

        [Column("DataConst")]
        [Display(Name = "DataConst")]
        public string DataConst { get; set; }

        [Column("InicioAtiv")]
        [Display(Name = "InicioAtiv")]
        public string InicioAtiv { get; set; }

        [Column("Capital")]
        [Display(Name = "Capital")]
        public string Capital { get; set; }

        [Column("Logradouro")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Column("Numero")]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Column("Complemento")]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Column("Bairro")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Column("Municipio")]
        [Display(Name = "Municipio")]
        public string Municipio { get; set; }

        [Column("Cep")]
        [Display(Name = "Cep")]
        public string Cep { get; set; }

        [Column("Uf")]
        [Display(Name = "Uf")]
        public string Uf { get; set; }
    }

}
