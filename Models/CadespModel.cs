using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class CadespModel
    {
        public string IE { get; set; }
        public string Situacao { get; set; }
        public string CNPJ { get; set; }
        public string DataInscricao { get; set; }
        public string NomeEmpresarial { get; set; }
        public string RegimeEstadual { get; set; }
        public string DRT { get; set; }
        public string PostoFiscal { get; set; }
        public string Nire { get; set; }
        public string OcorrenciaFiscal { get; set; }
        public string TipoUnidade { get; set; }
        public string FormasAtuacao { get; set; }
    }
}