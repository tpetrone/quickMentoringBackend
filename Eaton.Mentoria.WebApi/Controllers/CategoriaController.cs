using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private ICategoriaRepository _categoriaRepository;


        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            return Ok(_categoriaRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetAction(int id)
        {
            var categoria = _categoriaRepository.BuscarPorId(id);
            if (categoria != null)
                return Ok(categoria);
            else
                return NotFound();
        }


        [HttpPost]
        public IActionResult Cadastrar([FromBody] CategoriaDomain categoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_categoriaRepository.CategoriaExiste(categoria.Nome))
                    {
                        return BadRequest("Nome já cadastrado");
                    }

                    _categoriaRepository.Inserir(categoria);

                }

                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                return BadRequest(errors);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] CategoriaDomain categoria, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoria.CategoriaId = id;
                    _categoriaRepository.Atualizar(categoria);

                }
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                return BadRequest(errors);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }




    }
}