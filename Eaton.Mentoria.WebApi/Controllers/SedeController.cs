using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Sede.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class SedeController : Controller
    {
        private ISedeRepository _sedeRepository;


        public SedeController(ISedeRepository sedeRepository)
        {
            _sedeRepository = sedeRepository;
        }

        [HttpGet]
        public IActionResult GetAction(){
            return Ok(_sedeRepository.Listar());
        }  
        
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