using System;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Eaton.Mentoria.WebApi.Controllers
{
    public class MentoriaController : Controller
    {
        private IBaseRepository<MentoriaDomain> _mentoriaRepository;


        public MentoriaController(IBaseRepository<MentoriaDomain> mentoriaRepository)
        {
            _mentoriaRepository = mentoriaRepository;
        }

        public IActionResult GetAction(){
            return Ok(_mentoriaRepository.Listar());
        }       
        
    }

}