using System;
using System.Collections.Generic;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{
     [Route("api/[controller")]
    public class PerfilController : Controller
    {
         private IPerfilRepository _perfilRepository;

        public PerfilController(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        [HttpGet]
        public IActionResult GetAction(){
            return Ok(_perfilRepository.Listar(new string[]{"Perfil"}));

        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] PerfilDomain perfil)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(_perfilRepository.PerfilExiste(perfil.Cep, perfil.Foto, perfil.MiniBio))
                    {
                        return BadRequest("Perfil já cadastrado");
                    }
                    _perfilRepository.Inserir(perfil);
                }

                return Ok(perfil);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
            
            [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] PerfilDomain perfil, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if(_perfilRepository.BuscarPorId(id) != null){
                        return NotFound("Usuário nâo encontrado.");
                }

                perfil.PerfilId = id;
                _perfilRepository.Atualizar(perfil);
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
                //Busca o Perfil do id passado
                var perfil = _perfilRepository.BuscarPorId(id);

                //Verifica se encontrou o Perfil para o id passado, caso na encontre retorna NotFound
                if(perfil == null)
                    return NotFound();

                //Caso tenha encontrado exclui o perfil
                _perfilRepository.Deletar(perfil);
                return Ok(perfil);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }      

    }
}