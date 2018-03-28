using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class MentorController : Controller
    {
        private IMentoriaRepository mentoriaReprository;

        public MentorController(IMentoriaRepository mentoriaRepository)
        {
            this.mentoriaReprository = mentoriaRepository;
        }

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