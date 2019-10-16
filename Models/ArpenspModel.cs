using java.sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ArpenspModel
    {
        public string CartorioRegistro { get; set; }
        public double NumCNS { get; set; }
        public string UF { get; set; }
        public string NomeConj { get; set; }
        public string NovoNomeConj { get; set; }
        public string NomeConj2 { get; set; }
        public string NovoNomeConj2 { get; set; }
        public string DataCasamento { get; set; }
        public double Matricula { get; set; }
        public string DataEntrada { get; set; }
        public string DataRegistro { get; set; }
    }
}