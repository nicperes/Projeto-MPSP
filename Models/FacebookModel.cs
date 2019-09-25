using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class FacebookModel
    {

        int id;
        string nome;

        public FacebookModel(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
        }

        public FacebookModel()
        {
        }

        public int Id { get; private set; }

        public string Nome { get; private set; }
    }
}