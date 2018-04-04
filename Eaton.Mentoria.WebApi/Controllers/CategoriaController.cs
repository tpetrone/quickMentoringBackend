using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{   
   
     /// <summary>
     /// O controller categoria é responsável por:
     /// Cadastrar Categoria utilizando o verbo POST
     /// listar todas categorias utilizando o verbo GET
     /// Lista a categoria  por Id
     /// Atualiza a categoria por Id     
     /// </summary>
    [Route("api/categoria")]
    public class CategoriaController : Controller
    {
        private ICategoriaRepository _categoriaRepository;


        public CategoriaController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        
         /// <summary>
         /// Retorna todas as categorias no formato JSON
         /// </summary>
         /// <returns>Retorna todos os dados da categoria no formato JSON</returns>
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
      
        /// <summary>
        /// Cadastra a categoria recebendo os dados no BODY no formato JSON
        /// </summary>
        /// <param name="categoria">Recebe um objeto Categoria</param>
        /// <returns>Se cadastrado retorna ok(200) ou se não cadastrou retorna bad request(400)</returns>
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

                if (errors.Any())
                    return BadRequest(errors);
                else
                    return Ok(categoria);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }     
 
        /// <summary>
        /// Para atualizar a categoria é necessário passar o id da categoria que se deseja atualizar e os dados que serão atualizados da categoria no corpo (BODY) no formato JSON
        /// </summary>
        /// <param name="categoria"></param>
        /// <param name="id">id da categoria a se atualizado</param>Categoria a ser atualizada
        /// <response code="200">Retorna um int com o id da categoria</response>
        /// <response code="404">Retorna uma string</response>
        /// <response code="400">Retorna uma lista de erros</response> 
         
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

                if (errors.Any())
                    return BadRequest(errors);
                else
                    return Ok(categoria);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}