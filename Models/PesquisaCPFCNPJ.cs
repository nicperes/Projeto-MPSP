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
        public string Jucesp { get; set; }
    }
}