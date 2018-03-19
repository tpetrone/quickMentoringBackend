using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
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

                return Ok(sede);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] SedeDomain sede, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     sede.SedeId = id;
                    _sedeRepository.Atualizar(sede);
                    
                }
                return Ok(id);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }   

        
    }
}