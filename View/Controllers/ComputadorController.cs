using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.Interfaces;

namespace View.Controllers
{
    [Route("computador/")]
    public class ComputadorController : Controller
    {
        private IComputadorRepository reposirory;

        public ComputadorController(IComputadorRepository reposirory)
        {
            this.reposirory = reposirory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("cadastro")]
        public ActionResult Cadastro([FromForm]Computador computador)
        {
            var id = reposirory.Inserir(computador);
            return RedirectToAction("Editar", new { id = id });
        }

        [HttpPost, Route("alterar")]
        public JsonResult Alterar([FromForm]Computador computador)
        {
            var alterou = reposirory.Alterar(computador);
            return Json(new { status = alterou });
        }

        [HttpGet, Route("apagar")]
        public JsonResult Apagou(int id)
        {
            var apagou = reposirory.Apagar(id);
            return Json(new { status = apagou });
        }

        [HttpGet, Route("obterpeloid")]
        public ActionResult ObterPeloId(int id)
        {
            var computador = reposirory.ObterPeloId(id);
            if(computador == null) return NotFound();
            return Json(computador);
        }

        [HttpGet, Route("obtertodos")]
        public ActionResult ObterTodos()
        {
            return Json(reposirory.ObterTodos());
        }

        [HttpGet, Route("cadastro")]
        public ActionResult Cadastro()
        {
            return View();
        }
    }
}