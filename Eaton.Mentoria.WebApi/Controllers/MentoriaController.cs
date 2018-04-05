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

    [Route("api/mentoria")]
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
        /// <param name="id">Id da Mentoria</param>
        /// <returns>Retorna todas as mentorias no formato JSON</returns>

        [HttpGet]
        [Route("{id}/aplicacoes")]
        public IActionResult GetAplicacoes([FromRoute]int id)
        {
            var aplicacoes = aplicacaoRepository
                            .Listar(new string[] { "Mentorado", "Mentorado.Perfil", "Mentoria", "Mentoria.Categoria" })
                            .Where(aplicacao => aplicacao.Mentoria.MentoriaId == id)
                            .Select(obj => new
                            {
                                id = obj.AplicacaoId,
                                usuario = new
                                {
                                    usuarioId = obj.Mentorado.UsuarioId,
                                    email = obj.Mentorado.Email,
                                    password = "",
                                    role = obj.Mentorado.Role,
                                    ativo = obj.Mentorado.Ativo,
                                    perfil = new
                                    {
                                        id = obj.Mentorado.UsuarioId,
                                        nome = obj.Mentorado.Perfil?.Nome,
                                        miniBio = obj.Mentorado.Perfil?.MiniBio,
                                        foto = obj.Mentorado.Perfil?.Foto,
                                        cep = obj.Mentorado.Perfil?.Cep,
                                        sedeId = obj.Mentorado.Perfil?.SedeId
                                    }
                                },
                                mentoria = new
                                {
                                    id = obj.MentoriaId,
                                    nome = obj.Mentoria.Nome,
                                    categoria = new
                                    {
                                        nome = obj.Mentoria.Categoria.Nome,
                                        id = obj.Mentoria.CategoriaId
                                    },
                                },
                                justificativa = obj.justificativa,
                                aceite = obj.Aceite

                            });

            return Ok(aplicacoes);
        }

        /// <summary>
        /// Retorna as mentorias no formato JSON
        /// </summary>
        /// <returns>Retorna todas as mentorias no formato JSON</returns>
        [HttpGet]
        public IActionResult GetAction()
        {

            var mentorias = _mentoriaRepository
                                .Listar(new string[] { "Categoria", "Sede", "Usuario", "Usuario.Perfil" })
                                .Select(obj => new
                                {
                                    id = obj.MentoriaId,
                                    ativa = obj.Ativa,
                                    nome = obj.Nome,
                                    categoria = new
                                    {
                                        nome = obj.Categoria.Nome,
                                        id = obj.CategoriaId
                                    },
                                    sede = new
                                    {
                                        nome = obj.Sede.Nome,
                                        id = obj.SedeId
                                    },
                                    usuario = new
                                    {
                                        id = obj.Usuario.UsuarioId,
                                        email = obj.Usuario.Email,
                                        password = "",
                                        role = obj.Usuario.Role,
                                        ativo = obj.Usuario.Ativo,
                                        perfil = new
                                        {
                                            id = obj.Usuario.UsuarioId,
                                            nome = obj.Usuario.Perfil?.Nome,
                                            miniBio = obj.Usuario.Perfil?.MiniBio,
                                            foto = obj.Usuario.Perfil?.Foto,
                                            cep = obj.Usuario.Perfil?.Cep,
                                            sedeId = obj.Usuario.Perfil?.SedeId
                                        }
                                    }
                                });

            return Ok(mentorias);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetMentoria([FromRoute] int id)
        {

            var mentoria = _mentoriaRepository
                                .Listar(new string[] { "Categoria", "Sede", "Usuario", "Usuario.Perfil" })
                                .FirstOrDefault(user => user.MentoriaId == id);
            if (mentoria is null)
                return NotFound("Mentoria não encontrada");

            var resultado = new
            {
                id = mentoria.MentoriaId,
                nome = mentoria.Nome,
                categoria = new
                {
                    nome = mentoria.Categoria.Nome,
                    id = mentoria.Categoria.CategoriaId
                },
                sede = new
                {
                    nome = mentoria.Sede.Nome,
                    id = mentoria.Sede.SedeId,
                },
                usuarioid = mentoria.Usuario.UsuarioId,
                usuarionome = mentoria.Usuario.Perfil?.Nome ?? ""
            };

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

                if (errors.Any())
                    return BadRequest(errors);
                else
                    return Ok(mentoria);
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