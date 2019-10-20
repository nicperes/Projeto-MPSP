using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class PesquisaCPFCNPJ
    {
        public string CPFCNPJ { get; set; }
        public string Nome { get; set; }
        public string Arpensp { get; set; }
        public string Cadesp { get; set; }
        public string Caged { get; set; }
        public string Jucesp { get; set; }
        public string Detran { get; set; }
        public string Censec { get; set; }

        public ArpenspModel ArpenspModel { get; set; }
        public CadespModel CadespModel { get; set; }
        public CagedModel CagedModel { get; set; }
        public CensecModel CensecModel { get; set; }
        public DetranModel DetranModel { get; set; }
        public JucespModel JucespModel { get; set; }
    }
}