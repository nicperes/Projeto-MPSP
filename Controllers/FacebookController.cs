using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class FacebookController : ApiController
    {

        FacebookModel facebookModel = new FacebookModel();

        [HttpGet]
        [Route("rola")]
        public string Get()
        {
        return "Rola";
        }

        [HttpGet]
        [Route("rola/{id}")]
        public List<String> Get(int id)
        {
            return new List<string>
            {
                "Data 1",
                "Data 2"
            };
        }

    }
}
