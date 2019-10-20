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

        [Column("Placa")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Column("MunicipioCarro")]
        [Display(Name = "MunicipioCarro")]
        public string MunicipioCarro { get; set; }

        [Column("Renavam")]
        [Display(Name = "Renavam")]
        public string Renavam{ get; set; }

        [Column("Chassi")]
        [Display(Name = "Chassi")]
        public string Chassi { get; set; }

        [Column("NumMotor")]
        [Display(Name = "NumMotor")]
        public string NumMotor { get; set; }

        [Column("DataAltMotor")]
        [Display(Name = "DataAltMotor")]
        public string DataAltMotor { get; set; }

        [Column("Tipo")]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Column("Procedencia")]
        [Display(Name = "Procedencia")]
        public string Procedencia { get; set; }

        [Column("Combustivel")]
        [Display(Name = "Combustivel")]
        public string Combustivel { get; set; }

        [Column("Cor")]
        [Display(Name = "Cor")]
        public string Cor { get; set; }

        [Column("MarcaModelo")]
        [Display(Name = "MarcaModelo")]
        public string MarcaModelo { get; set; }

        [Column("CategoriaAut")]
        [Display(Name = "CategoriaAut")]
        public string CategoriaAut { get; set; }

        [Column("Fabricacao")]
        [Display(Name = "Fabricacao")]
        public string Fabricacao { get; set; }

        [Column("Modelo")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Column("Logradouro")]
        [Display(Name = "Logradouro")]
        public string Logradouro { get; set; }

        [Column("Numero")]
        [Display(Name = "Numero")]
        public string Numero { get; set; }

        [Column("Complemento")]
        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Column("CEP")]
        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Column("Bairro")]
        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Column("Licenciamento")]
        [Display(Name = "Licenciamento")]
        public string Licenciamento { get; set; }

        [Column("DataLicenciamento")]
        [Display(Name = "DataLicenciamento")]
        public string DataLicenciamento { get; set; }

        [Column("DataEmissaoCRV")]
        [Display(Name = "DataEmissaoCRV")]
        public string DataEmissaoCRV { get; set; }
    }
}