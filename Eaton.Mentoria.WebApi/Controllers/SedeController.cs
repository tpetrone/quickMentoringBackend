using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Eaton.Sede.WebApi.Controllers
{  

    /// <summary>
    /// O controller sede é responsável por:
    /// Cadastrar sedes utilizando o verbo POST
    /// listar todas sedes utilizando o verbo GET
    /// Lista a sede por Id
    /// Deleta a sede por Id
    /// Atualiza a sede por Id 
     /// </summary>
    
    [Route("api/[controller]")]
    public class SedeController : Controller
    {
        private ISedeRepository _sedeRepository;


        public SedeController(ISedeRepository sedeRepository)
        {
            _sedeRepository = sedeRepository;
        }

        [HttpGet]
        public IActionResult GetAction()
        {
            return Ok(_sedeRepository.Listar());
        }
        
        /// <summary>
        /// Efetua o Cadastro da Sede        
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Sede
        ///     {
        ///        "SedeId": 7,
        ///        "Nome": "Nome das Sedes"
        ///     }
        ///
        /// </remarks>
        /// <param name="sede">Nome da Sede</param>
        /// <returns></returns>

       [HttpPost]
        [Authorize("Bearer", Roles = "Admin")]
        public IActionResult Cadastrar([FromBody] SedeDomain sede)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_sedeRepository.SedeExiste(sede.Nome))
                    {
                        return BadRequest("Nome já cadastrado");
                    }

                    _sedeRepository.Inserir(sede);

                }

                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                if (errors.Any())
                    return BadRequest(errors);
                else
                    return Ok(sede);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }     
     
         /// <summary>
         /// Para atualizar a sede é necessário passar o id da sede que se deseja atualizar e os dados que serão atualizados da sede no corpo (BODY) no formato JSON
         /// </summary>
         /// <param name="sede">Novos dados que vão para a sede</param>
         /// <param name="id">Id da sede</param>
         /// <response code="200">Retorna um int com o id da sede</response>
         /// <response code="404">Retorna uma string</response>
         /// <response code="400">Retorna uma lista de erros</response>          

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(string), 404)]
        [ProducesResponseType(typeof(List<ModelError>), 400)]
        [Authorize("Bearer", Roles = "Admin")]
        public IActionResult Atualizar([FromBody] SedeDomain sede, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sede.SedeId = id;
                    _sedeRepository.Atualizar(sede);

                }
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                if (errors.Any())
                    return BadRequest(errors);
                else
                    return Ok(sede);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}