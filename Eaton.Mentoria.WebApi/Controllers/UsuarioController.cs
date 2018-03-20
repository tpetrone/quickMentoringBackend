using System;
using System.Collections.Generic;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{
    [Route("api/[controller")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult GetAction(){
            return Ok(_usuarioRepository.Listar(new string[]{"Usuario"}));

        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] UsuarioDomain usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(_usuarioRepository.UsuarioExiste(usuario.Email, usuario.Password, usuario.Role))
                    {
                        return BadRequest("Usuario já cadastrado");
                    }
                    _usuarioRepository.Inserir(usuario);
                }

                return Ok(usuario);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
            
            [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] UsuarioDomain usuario, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if(_usuarioRepository.BuscarPorId(id) != null){
                        return NotFound("Usuário nâo encontrado.");
                }

                usuario.UsuarioId = id;
                _usuarioRepository.Atualizar(usuario);
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
                //Busca o usuário do id passado
                var usuario = _usuarioRepository.BuscarPorId(id);

                //Verifica se encontrou o usuário para o id passado, caso na encontre retorna NotFound
                if(usuario == null)
                    return NotFound();

                //Caso tenha encontrado o usuario exclui
                _usuarioRepository.Deletar(usuario);
                return Ok(usuario);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }  
        
    }
}