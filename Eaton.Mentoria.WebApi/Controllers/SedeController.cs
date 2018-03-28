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
    /// O controller Sede é responsável por:
    /// Cadastrar Sedes utilizando o verbo POST
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

        
        /// <summary>
        /// Retorna as sedes no formato JSON
        /// </summary>
        /// <returns>Retorna todos os dados da sede no formato JSON</returns>
        [HttpGet]
        public IActionResult GetAction(){
            return Ok(_sedeRepository.Listar());
        }  
        
        
        /// <summary>
        /// Recebe dados da sede e cadastra no banco
        /// </summary>
        /// <param name="sede">dado da sede a ser cadastrado</param>
        /// <returns>Retorna se a operação deu certo ou não</returns>
        
        [HttpPost]
        [Authorize("Bearer", Roles = "Admin")]
        public IActionResult Cadastrar([FromBody] SedeDomain sede)
        {
             try
            {
                if (ModelState.IsValid)
                {
                   if(_sedeRepository.SedeExiste(sede.Nome))
                    {
                        return BadRequest("Nome já cadastrado");
                    }

                    _sedeRepository.Inserir(sede);
                    
                }                

                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                           
                return BadRequest(errors);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        /// <summary>
        /// Atualiza dados da sede no banco
        /// </summary>
        /// <param name="sede">dado da sede a ser atualizado</param>
        /// <param name="id">dado da sede a ser atualizado</param>
        /// <returns>Retorna se a operação deu certo ou não</returns>
        
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
                           .Where(y=>y.Count>0)
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