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
    /// <summary>
    /// O controller mentoria é responsável por:
    /// Cadastrar Mentorias utilizando o verbo POST
    /// listar todas mentorias utilizando o verbo GET
    /// Lista a mentoria por Id
    /// Deleta a mentoria por Id
    /// Atualiza a mentoria por Id 
    /// </summary>
    
    [Route("api/[controller]")]
    public class MentoriaController : Controller
    {
        private IMentoriaRepository _mentoriaRepository;
        private IAplicacaoRepository aplicacaoRepository;

        public MentoriaController(IMentoriaRepository mentoriaRepository, IAplicacaoRepository aplicacaoRepository)
        {
            _mentoriaRepository = mentoriaRepository;
            this.aplicacaoRepository = aplicacaoRepository;
        }

        [HttpGet]
        [Route("{id}/aplicacoes")]
        public IActionResult GetAplicacoes([FromRoute]int id)
        {
            var aplicacoes = aplicacaoRepository.Listar(new string[] { "Mentorado", "Mentorado.Perfil", "Mentoria" }).Where(aplicacao => aplicacao.Mentoria.MentoriaId == id);

            return Ok(aplicacoes);
        }


        [HttpGet]
        public IActionResult GetAction()
        {

            IEnumerable<MentoriaDomain> lsMentoria = _mentoriaRepository.Listar(new string[] { "Categoria", "Sede", "Usuario", "Usuario.Perfil" });

            var resultado = lsMentoria.Select(x => new
            {
                id = x.MentoriaId,
                nome = x.Nome,
                categoria = new
                {
                    nome = x.Categoria.Nome,
                    id = x.Categoria.CategoriaId
                },
                sede = new
                {
                    nome = x.Sede.Nome,
                    id = x.Sede.SedeId,
                },
                usuarioid = x.Usuario.UsuarioId,
                usuarionome = x.Usuario.Perfil is null ? "" : x.Usuario.Perfil.Nome
            });

            return Ok(resultado);
        }
         /// <summary>
         /// Efetua a mentoria
         /// </summary>
         /// <remarks>
         /// Sample request:
         ///
         ///     POST /api/mentoria
         ///     {
         ///        "ativa": "1",
         ///        "Online": "1",
         ///        "Nome": "Nome da Mentoria"
         ///     }
         ///
         /// </remarks>
         /// <param name="mentoria">Dados da mentoria</param>
         /// <returns></returns>
        [HttpPost]
        [Authorize("Bearer", Roles = "Mentor")]
        public IActionResult Cadastrar([FromBody] MentoriaDomain mentoria)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_mentoriaRepository.MentoriaExiste(mentoria.UsuarioId, mentoria.CategoriaId, mentoria.SedeId))
                    {
                        return BadRequest("Mentoria já cadastrada");
                    }

                    _mentoriaRepository.Inserir(mentoria);
                    return Ok(mentoria);
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
        /// Para atualizar a mentoria é necessário passar o id da mentoria que se deseja atualizar e os dados que serão atualizados da mentoria no corpo (BODY) no formato JSON
        /// </summary>
        /// <param name="mentoria">Novos dados que vão para a mentoria</param>
        /// <param name="id"></param>
        /// <response code="200">Retorna um int com o id da mentoria</response>
        /// <response code="404">Retorna uma string</response>
        /// <response code="400">Retorna uma lista de erros</response>  
        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] MentoriaDomain mentoria, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (_mentoriaRepository.BuscarPorId(id) != null)
                {
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
                if (mentoria == null)
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