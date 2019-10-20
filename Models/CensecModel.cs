using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class CensecModel
    {
        public string Carga { get; set; }
        public string Mes { get; set; }
        public string Ano { get; set; }
        public string Ato { get; set; }
        public string DiaAto { get; set; }
        public string MesAto { get; set; }
        public string AnoAto { get; set; }
        public string Livro { get; set; }
        public string Folha { get; set; }
        public string NomesPartes { get; set; }
        public string CpfCnpjPartes { get; set; }
        public string QualidadePartes { get; set; }
        public string UF { get; set; }
        public string Municipio { get; set; }
        public string Cartorio { get; set; }
        public string TelefoneCartorio { get; set; }
        public string TipoTelefoneCartorio { get; set; }
        public string ContatoCartorio { get; set; }
        public string StatusCartorio { get; set; }
    }
}