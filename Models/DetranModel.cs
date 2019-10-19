using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class DetranModel
    {
        [Key]
        [Column("CNPJCPF")]
        public long CNPJCPF { get; set; }

        [Column("RG")]
        [Display(Name = "RG")]
        public string RG { get; set; }

        [Column("Expeditor")]
        [Display(Name = "Expeditor")]
        public string Expeditor { get; set; }

        [Column("Registro")]
        [Display(Name = "Registro")]
        public string Registro { get; set; }

        [Column("Local")]
        [Display(Name = "Local")]
        public string Local { get; set; }

        [Column("PID")]
        [Display(Name = "PID")]
        public string PID { get; set; }

        [Column("EmissaoCnh")]
        [Display(Name = "EmissaoCnh")]
        public string EmissaoCnh { get; set; }

        [Column("Categoria")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Column("PrimeiraHabilitação")]
        [Display(Name = "PrimeiraHabilitação")]
        public string PrimeiraHabilitação { get; set; }

        [Column("StatusCnh")]
        [Display(Name = "StatusCnh")]
        public string StatusCnh { get; set; }

        [Column("Renach")]
        [Display(Name = "Renach")]
        public string Renach { get; set; }

        [Column("EspelhoCnh")]
        [Display(Name = "EspelhoCnh")]
        public string EspelhoCnh { get; set; }

        [Column("ValidadeCnh")]
        [Display(Name = "ValidadeCnh")]
        public string ValidadeCnh { get; set; }

        [Column("Pontuacao")]
        [Display(Name = "Pontuacao")]
        public string Pontuacao { get; set; }

        [Column("NomePai")]
        [Display(Name = "NomePai")]
        public string NomePai { get; set; }

        [Column("NomeMae")]
        [Display(Name = "NomeMae")]
        public string NomeMae { get; set; }
    }
}