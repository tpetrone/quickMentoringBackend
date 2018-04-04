using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{
   /// <summary>
    /// O controller Mentorado é responsável por:
    /// Cadastrar Mentorados utilizando o verbo POST
    /// listar todos mentorados utilizando o verbo GET
    /// Lista o mentorado por Id
    /// Deleta o mentorado por Id
    /// Atualiza o mentorado por Id
    /// </summary>

    [Route("api/mentorado")]
    public class MentoradoController : Controller
    {
        private IAplicacaoRepository aplicacaoRepository;

        public MentoradoController(IAplicacaoRepository aplicacaoRepository)
        {
            this.aplicacaoRepository = aplicacaoRepository;
        }      

        /// <summary>
        /// Retorna os mentorados no formato JSON
        /// </summary>
        /// <param name="id">Retorna o Id do mentorado</param>
        /// <returns>Retorna todos os dados do usuário no formato JSON</returns>

        [HttpGet]
        [Route("{id}/aplicacoes")]
        public IActionResult GetAplicacoes([FromRoute]int id)
        {
            var aplicacoes = aplicacaoRepository.
                                                    Listar(new string[] { "Mentorado", "Mentorado.Perfil", "Mentoria" }).
                                                    Where(x => x.MentoradoId == id);

            return Ok(aplicacoes);
        }


    }
}