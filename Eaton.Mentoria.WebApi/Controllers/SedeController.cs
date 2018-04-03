using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
       /// Cadastra a sede recebendo os dados no BODY no formato JSON
       /// </summary>
       /// <param name="sede">Recebe um objeto usuario</param>
       /// <returns>Se cadastrado retorna ok(200) ou se não cadastrou retorna bad request(400)</returns>

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

                return BadRequest(errors);
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
        /// <param name="id">Id da sede a ser atualizada</param>
        /// <returns>Se atualizado retorna ok(200) ou se não cadastrou retorna bad request(400)</returns>

        [HttpPut("{id}")]
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

                return BadRequest(errors);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}