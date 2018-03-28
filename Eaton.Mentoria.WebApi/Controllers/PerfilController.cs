using System;
using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{
     
    /// <summary>
    /// O controller Perfil é responsável por:
    /// Cadastrar Perfis utilizando o verbo POST
    /// listar todos Perfis utilizando o verbo GET
    /// Lista o Perfil por Id
    /// Deleta o Perfil por Id
    /// Atualiza o Perfil por Id
    /// </summary>
     
     [Route("api/[controller]")]
    public class PerfilController : Controller
    {
         private IPerfilRepository _perfilRepository;

        public PerfilController(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

       
       /// <summary>
       /// Retorna os perfis no formato JSON
       /// </summary>
       /// <returns>Retorna todos os dados do perfil no formato JSON</returns>        
        [HttpGet]
        public IActionResult GetAction(){
            return Ok(_perfilRepository.Listar(new string[]{"Perfil"}));

        }

        /// <summary>
        /// Recebe dados da Perfil e cadastra no banco
        /// </summary>
        /// <param name="perfil">dado do perfil a ser cadastrado</param>
        /// <returns>Retorna se a operação deu certo ou não</returns>
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
        /// Para atualizar o perfil é necessário passar o id do perfil que se deseja atualizar e os dados que serão atualizados do perfil no corpo (BODY) no formato JSON
        /// </summary>
        /// <param name="perfil">Novos dados que vão para o perfil</param>
        /// <param name="id"></param>
        /// <returns>Se atualizado retorna ok(200) ou se não cadastrou retorna bad request(400)</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] PerfilDomain perfil, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                           
                    return BadRequest(errors);
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

        
        
        /// <summary>
        ///  Para deletar o perfil é necessário passar o id do perfil que se deseja atualizar e os dados que serão atualizados do perfil no corpo (BODY) no formato JSON
        /// </summary>
        /// <param name="id">Id do perfil a ser apagado</param>
        /// <returns>Se atualizado retorna ok(200) ou se não cadastrou retorna bad request(400)</returns>
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