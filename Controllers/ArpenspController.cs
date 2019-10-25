using System.Collections.Generic;
using System.Web.Mvc;
using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Controllers
{
    public class ArpenspController : Controller
    {
        private readonly ArpenspRepository arpenspRepository;
        private readonly CadespRepository cadespRepository;
        private readonly CagedRepository cagedRepository;
        private readonly CensecRepository censecRepository;
        private readonly DetranRepository detranRepository;
        private readonly JucespRepository jucespRepository;

        public ArpenspController()
        {
            arpenspRepository = new ArpenspRepository();
            cadespRepository = new CadespRepository();
            cagedRepository = new CagedRepository();
            censecRepository = new CensecRepository();
            detranRepository = new DetranRepository();
            jucespRepository = new JucespRepository();
        }


        [HttpGet]
        public ActionResult Index()
        {
            List<ArpenspModel> arpensp = arpenspRepository.FindAll();
            return View(arpensp);
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult Create()
        {
            ViewBag.Marcas = cadespRepository.FindAll();
            ViewBag.Categorias = cagedRepository.FindAll();
            ViewBag.Marcas = censecRepository.FindAll();
            ViewBag.Categorias = detranRepository.FindAll();
            ViewBag.Marcas = jucespRepository.FindAll();
            
            return View(new ArpenspModel());
        }

        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(arpenspRepository.FindById(id));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Marcas = cadespRepository.FindAll();
            ViewBag.Categorias = cagedRepository.FindAll();
            ViewBag.Marcas = censecRepository.FindAll();
            ViewBag.Categorias = detranRepository.FindAll();
            ViewBag.Marcas = jucespRepository.FindAll();

            return View(arpenspRepository.FindById(id));
        }

  
        
    }
}