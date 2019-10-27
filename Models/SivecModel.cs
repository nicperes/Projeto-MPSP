using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    [Table("SivecModel")]
    public class SivecModel
    {

        [Key]
        [Column("CnpjSivec")]
        public long CnpjSivec { get; set; }

        [Column("DataEmissao")]
        [Display(Name = "DataEmissao")]
        public string DataEmissao { get; set; }

        [Column("EstadoCivil")]
        [Display(Name = "EstadoCivil")]
        public string EstadoCivil { get; set; }

        [Column("Naturalizado")]
        [Display(Name = "Naturalizado")]
        public string Naturalizado { get; set; }

        [Column("PostoIdentificacao")]
        [Display(Name = "PostoIdentificacao")]
        public string PostoIdentificacao { get; set; }

        [Column("GrauInstituicao")]
        [Display(Name = "GrauInstituicao")]
        public string GrauInstituicao { get; set; }

        [Column("CorOlho")]
        [Display(Name = "CorOlho")]
        public string CorOlho { get; set; }

        [Column("CorCabelo")]
        [Display(Name = "CorCabelo")]
        public string CorCabelo { get; set; }

        [Column("CorPele")]
        [Display(Name = "CorPele")]
        public string CorPele { get; set; }

        [Column("Profissao")]
        [Display(Name = "Profissao")]
        public string Profissao { get; set; }


    }
}
