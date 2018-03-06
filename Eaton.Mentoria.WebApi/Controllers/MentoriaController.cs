using Microsoft.AspNetCore.Mvc;

namespace Eaton.Mentoria.WebApi.Controllers
{
    public class MentoriaController : Controller
    {
        private IBaseRepository<MentoriaDomain> _mentoriaReory;


        public MentoriaController(IBaseRepository<MentoriaDomain> mentoriaREpository)
        {
            _mentoriaReory = mentoriaREpository;
        }

        public IActionResult GetAction(){
            return Ok(_mentoriaRepository.Listar());
        }
        
        
    }
}