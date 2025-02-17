﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Repository.Interfaces;

namespace View.Controllers
{
    public class CategoriaController : Controller
    {
        private ICategoriaRepository repository;

        public CategoriaController(ICategoriaRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet, Route("categoria/obtertodosselect2")]
        public JsonResult ObterTodosSelect2(string term = "")

        {
            term = term == null? "" : term;
            var categorias = repository.ObterTodosSelect2(term);

            List<object> categoriasSelect2 = new List<object>();
            foreach(Categoria categoria in categorias)
            {
                categoriasSelect2.Add(new
                {
                    id = categoria.Id,
                    text = categoria.Nome
                });
                
            }
            return Json(new { results = categoriasSelect2 });
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método que permitira ter acesso aos dados das categorias,
        /// filtrando, ordenando e paginando as informações de acordo 
        /// com os parametros
        /// </summary>
        /// <param name="busca"></param>
        /// <param name="quantidade"></param>
        /// <param name="pagina"></param>
        /// <param name="colunaOrdem"></param>
        /// <param name="ordem"></param>
        /// <returns>Retorna as Categorias</returns>        
        [HttpGet, Route("categoria/obtertodos")]
        public JsonResult ObterTodos(
            Dictionary<string, string> search, int quantidade = 10, int pagina = 0,
            string colunaOrdem = "nome", string ordem = "ASC")
        {
            string busca = search["value"];


            if (busca == null)
                busca = "";

            List<Categoria> categorias = repository.ObterTodos(quantidade, pagina, busca, colunaOrdem, ordem);

            return Json(new { data = categorias });
        }

        [HttpPost]
        public JsonResult Cadastrar([FromForm]Categoria categoria)
        {
            categoria.RegistroAtivo = true;
            int id = repository.Inserir(categoria);
            // OBJETO ANONIMO
            var retorno = new { id = id };
            return Json(retorno);
        }

        [HttpPost, Route("categoria/alterar")]
        public JsonResult Alterar([FromForm]Categoria categoria)
        {
            categoria.RegistroAtivo = true;
            bool alterado = repository.Alterar(categoria);
            var resultado = new { status = alterado };
            return Json(resultado);

        }

        [HttpGet]
        // categoria/apagar?id=2
        public JsonResult Apagar(int id)
        {
            bool apagou = repository.Apagar(id);
            var resultado = new { status = apagou };
            return Json(resultado);
        }

        [HttpGet, Route("categoria/obterpeloid")]
        public JsonResult ObterPeloId(int id)
        {
            return Json(repository.ObterPeloId(id));
        }
    }
}