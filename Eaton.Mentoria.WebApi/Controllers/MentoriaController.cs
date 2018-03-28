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
    /// O controller Mentoria é responsável por:
    /// Cadastrar Mentoria utilizando o verbo POST
    /// listar todas mentorias utilizando o verbo GET
    /// Lista a Mentoria por Id
    /// Deleta a Mentoria por Id
    /// Atualiza a Mentoria por Id
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

        
        /// <summary>
        /// Retorna as mentorias no formato JSON
        /// </summary>
        /// <param name="id">Recebe o Id da Mentoria</param>
        /// <returns>returns>Retorna todos os dados do perfil no formato JSON</returns>
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
        /// Recebe dados da mentoria e cadastra no banco
        /// </summary>
        /// <param name="mentoria">dado da mentoria a ser cadastrado</param>
        /// <returns>Retorna se a operação deu certo ou não</returns>              
        
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
        /// Atualiza dados da mentoria no banco
        /// </summary>
        /// <param name="mentoria">dado da mentoria a ser atualizado</param>
        /// <param name="id">id da mentoria a ser atualizado</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deleta a Mentoria recebendo os dados no BODY no formato JSON
        /// </summary>
        /// <param name="id">Id do usuario que deve ser deletado</param>
        /// <returns>Se deletado retorna ok(200) ou se não deletou retorna bad request(400)</returns>
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