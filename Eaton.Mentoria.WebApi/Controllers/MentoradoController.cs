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
    /// Cadastrar Mentorado utilizando o verbo POST
    /// listar todos mentorados utilizando o verbo GET
    /// Lista o Mentorado por Id
    /// Deleta o Mentorado por Id
    /// Atualiza o Mentorado por Id
    /// </summary>
    [Route("api/[controller]")]
    public class MentoradoController : Controller
    {
        private IAplicacaoRepository aplicacaoRepository;

        public MentoradoController(IAplicacaoRepository aplicacaoRepository)
        {
            this.aplicacaoRepository = aplicacaoRepository;
        }

        /// <summary>
        /// Retorna os Mentorados no formato JSON
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna todos os mentorados no formato JSON</returns>
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