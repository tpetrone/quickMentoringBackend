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
    /// O controller Mentor é responsável por:
    /// Cadastrar Mentores utilizando o verbo POST
    /// listar todos mentores utilizando o verbo GET
    /// Lista o mentor por Id
    /// Deleta o mentor por Id
    /// Atualiza o mentor por Id
    /// </summary>
    
   [Route("api/[controller]")]
    public class MentorController : Controller
    {
        private IMentoriaRepository mentoriaReprository;

        public MentorController(IMentoriaRepository mentoriaRepository)
        {
            this.mentoriaReprository = mentoriaRepository;
        }
        /// <summary>
        /// Retorna os usuários no formato JSON
        /// </summary>
        /// <param name="id">Retorna o Id da mentor</param>
        /// <returns>Retorna todos os dados do mentor no formato JSON</returns>

        [HttpGet]
        [Route("{id}/mentorias")]
        public IActionResult GetMentorias([FromRoute]int id)
        {
            var mentorias = mentoriaReprository.Listar(new string[] { "Categoria", "Sede" }).Where(mentoria => mentoria.UsuarioId == id);

            mentorias.AsParallel().WithExecutionMode(ParallelExecutionMode.ForceParallelism).ForAll(mentoria => {
                mentoria.Categoria.Mentorias.Clear();
                mentoria.Sede.Mentorias.Clear();
            });

            return Ok(mentorias);

        }
    }
}