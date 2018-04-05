using System.Collections.Generic;
using System.Linq;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{

    /// <summary>
    /// O controller Aplicação é responsável por:
    /// Cadastrar Aplicações utilizando o verbo POST
    /// listar todas aplicações utilizando o verbo GET
    /// Lista a aplicação por Id
    /// Deleta a aplicação por Id
    /// Atualiza a aplicação por Id 
    /// </summary>
    [Route("api/aplicacao")]
    public class AplicacaoController : Controller
    {
        private IAplicacaoRepository _aplicacaoRepository;

        public AplicacaoController(IAplicacaoRepository aplicacaoRepository)
        {
            _aplicacaoRepository = aplicacaoRepository;
        }

        /// <summary>
        /// Retorna a aplicação no formato JSON
        /// </summary>
        /// <returns>Retorna todaos os dados da aplicação no formato JSON</returns>
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult GetAction()
        {
            return Ok(_aplicacaoRepository.Listar());
        }

        [HttpGet("{id}")]
        [Authorize("Bearer")]
        public IActionResult GetAction(int id)
        {
            var aplicacao = _aplicacaoRepository
                .Listar(new[] { "Mentorado", "Mentorado.Perfil", "Mentoria", "Mentoria.Categoria", "Mentoria.Sede" })
                .Select(obj => new
                {
                    id = obj.AplicacaoId,
                    usuario = new
                    {
                        id = obj.Mentorado.UsuarioId,
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
                        id = obj.Mentoria.MentoriaId,
                        nome = obj.Mentoria.Nome,
                        categoria = new
                        {
                            nome = obj.Mentoria.Categoria.Nome,
                            id = obj.Mentoria.Categoria.CategoriaId,
                        },
                        sede = new
                        {
                            id = obj.Mentoria.Sede.SedeId,
                            nome = obj.Mentoria.Sede.Nome
                        }
                    },
                    justificativa = obj.justificativa
                })
                .First(app => app.id == id);

            if (aplicacao != null)
                return Ok(aplicacao);
            else
                return NotFound();
        }

        /// <summary>
        /// Cadastra a aplicação recebendo os dados no BODY no formato JSON
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/aplicacao
        ///     {
        ///        "Justificativa": "aprender mais",
        ///        "Aceite": "0"
        ///     }
        ///
        /// </remarks>
        /// <param name="aplicacao">Recebe um objeto aplicação</param>
        /// <returns>Se cadastrado retorna ok(200) ou se não cadastrou retorna bad request(400)</returns>
        [HttpPost]
        [Authorize("Bearer")]
        public IActionResult Cadastrar([FromBody] AplicacaoDomain aplicacao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_aplicacaoRepository.AplicacaoExiste(aplicacao.MentoradoId, aplicacao.MentoriaId))
                    {
                        return BadRequest("Solicitação para mentoria já efetuada!");
                    }

                    _aplicacaoRepository.Inserir(aplicacao);
                    return Ok(aplicacao);
                }

                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();

                if (errors.Any())
                    return BadRequest(errors);
                else
                    return Ok(aplicacao);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Para atualizar a aplicação é necessário passar o id da aplicação que se deseja atualizar e os dados que serão atualizados da aplicação no corpo (BODY) no formato JSON
        /// </summary>
        /// <param name="aplicacao">Novos dados que vão para a aplicação</param>
        /// <param name="id">Se atualizado retorna ok(200) ou se não cadastrou retorna bad request(400</param>
        /// <response code="200">Retorna um int com o id da aplicação</response>
        /// <response code="404">Retorna uma string</response>
        /// <response code="400">Retorna uma lista de erros</response> 
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
                           .Where(y => y.Count > 0)
                           .ToList();


                if (errors.Any())
                    return BadRequest(errors);
                else
                    return Ok(aplicacao);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}