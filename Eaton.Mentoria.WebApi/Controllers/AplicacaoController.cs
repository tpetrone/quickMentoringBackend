using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{
   
  
   [Route("api/[controller]")]
    public class AplicacaoController : Controller
    {
        private IAplicacaoRepository _aplicacaoRepository;

        public AplicacaoController(IAplicacaoRepository aplicacaoRepository)
        {
            _aplicacaoRepository = aplicacaoRepository;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult GetAction(){
            return Ok(_aplicacaoRepository.Listar());
        }  
        
        [HttpGet("{id}")]
        [Authorize("Bearer")]
        public IActionResult GetAction(int id){
            var aplicacao = _aplicacaoRepository.BuscarPorId(id);
            if(aplicacao != null)
                return Ok(aplicacao);
            else
                return NotFound();
        } 

        [HttpGet("mentor/{id}")]
        [Authorize("Bearer")]
        [Route("mentor")]
        public IActionResult GetAplicacaoMentor(int id){
            List<AplicacaoDomain> aplicacoes = _aplicacaoRepository.Listar(new string[]{"Mentorado", "Mentorado.Perfil","Mentoria"}).Where(x => x.Mentoria.UsuarioId == id).ToList();
            if(aplicacoes != null)
                return Ok(aplicacoes);
            else
                return NotFound();
        } 

        [HttpGet("mentorado/{id}")]
        [Authorize("Bearer")]
        [Route("mentorado")]
        public IActionResult GetAplicacaoMentorado(int id){
            List<AplicacaoDomain> aplicacoes = _aplicacaoRepository.Listar(new string[]{"Mentorado", "Mentorado.Perfil","Mentoria"}).Where(x => x.MentoradoId == id).ToList();
            if(aplicacoes != null)
                return Ok(aplicacoes);
            else
                return NotFound();
        } 

        [HttpPost]       
        [Authorize("Bearer")] 
        public IActionResult Cadastrar([FromBody] AplicacaoDomain aplicacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   if(_aplicacaoRepository.AplicacaoExiste(aplicacao.MentoradoId,aplicacao.MentoriaId))
                    {
                        return BadRequest("Solicitação para mentoria já efetuada!");
                    }

                    _aplicacaoRepository.Inserir(aplicacao);
                    return Ok(aplicacao);
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
        public IActionResult Atualizar([FromBody] AplicacaoDomain aplicacao, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     aplicacao.AplicacaoId = id;
                    _aplicacaoRepository.Atualizar(aplicacao);
                    
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