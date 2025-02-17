﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.Interfaces;

namespace View.Controllers
{
    [Route("peca/")]
    public class PecaController : Controller
    {
        private IPecaRepository repository;

        public PecaController(IPecaRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, Route("inserir")]
        public JsonResult Inserir([FromForm] Peca peca)
        {
            var id = repository.Inserir(peca);
            var resultado = new { id = id };
            return Json(resultado);
        }

        [HttpPost, Route("alterar")]
        public JsonResult Alterar([FromForm]Peca peca)
        {
            var alterado = repository.Alterar(peca);
            var resultado = new { status = alterado };
            return Json(resultado);
        }

        [HttpGet, Route("apagar")]
        public JsonResult Apagar(int id)
        {
            var apagou = repository.Apagar(id);
            var resultado = new { status = apagou };
            return Json(resultado);
        }

        [HttpGet, Route("obtertodos")]
        public JsonResult ObterTodos()
        {
            var pecas = repository.ObterTodos();
            return Json(new { data = pecas });
        }

        [HttpGet, Route("obterpeloid")]
        public ActionResult ObterPeloId(int id)
        {
            var peca = repository.ObterPeloId(id);
            if (peca == null)
                return NotFound();

            return Json(peca);
        }

        [HttpGet, Route("obtertodosselect2")]
        public JsonResult ObterTodosSelect2(string term = "")
        {
            term = term == null ? "" : term;

            var registros = repository.ObterTodos();

            registros = registros.Where(x => x.Nome.Contains(term)).ToList();
            var pecasSelect2 = new List<object>();

            foreach (var peca in registros)
            {
                pecasSelect2.Add(new
                {
                    id = peca.Id,
                    text = peca.Nome
                });
            }
            return Json(new { results = pecasSelect2 });
        }
    }
}