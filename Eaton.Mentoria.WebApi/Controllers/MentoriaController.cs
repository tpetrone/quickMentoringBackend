using System;
using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Eaton.Mentoria.WebApi.Controllers
{
    
     [Route("api/[controller]")]
    public class MentoriaController : Controller
    {
        private IMentoriaRepository _mentoriaRepository;


        public MentoriaController(IMentoriaRepository mentoriaRepository)
        {
            _mentoriaRepository = mentoriaRepository;
        }

        [HttpGet]
        public IActionResult GetAction(){
            return Ok(_mentoriaRepository.Listar(new string[]{"Categoria","Usuario","Usuario.Perfil", "Sede"}));
        } 

        [HttpPost]
        [Authorize("Bearer", Roles="Mentor")]
        public IActionResult Cadastrar([FromBody] MentoriaDomain mentoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   if(_mentoriaRepository.MentoriaExiste(mentoria.UsuarioId,mentoria.CategoriaId, mentoria.SedeId))
                    {
                        return BadRequest("Mentoria já cadastrada");
                    }

                    _mentoriaRepository.Inserir(mentoria);
                    return Ok(mentoria);
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
        public IActionResult Atualizar([FromBody] MentoriaDomain mentoria, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if(_mentoriaRepository.BuscarPorId(id) != null){
                        return NotFound("Mentoria nâo encontrada");
                }

                mentoria.MentoriaId = id;
                _mentoriaRepository.Atualizar(mentoria);
                return Ok(id);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //Busca a mentoria do id passado
                var mentoria = _mentoriaRepository.BuscarPorId(id);

                //Verifica se encontrou a mentoria para o id passado, caso na encontre retorna NotFound
                if(mentoria == null)
                    return NotFound();

                //Caso tenha encontrado a mentoria exclui
                _mentoriaRepository.Deletar(mentoria);
                return Ok(mentoria);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }      
        
    }

}