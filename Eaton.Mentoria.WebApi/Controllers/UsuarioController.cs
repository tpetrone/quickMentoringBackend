using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using Eaton.Mentoria.Domain.Contracts;
using Eaton.Mentoria.Domain.Entities;
using Eaton.Mentoria.Repository.Context;
using Eaton.Mentoria.WebApi.util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Eaton.Mentoria.WebApi.Controllers
{
    
    /// <summary>
    /// O controller usuário é responsável por:
    /// Cadastrar Usuários utilizando o verbo POST
    /// listar todos usuários utilizando o verbo GET
    /// Lista o usuário por Id
    /// Deleta o usuário por Id
    /// Atualiza o usuário por Id
    /// </summary>
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        
        /// <summary>
        /// Retorna os usuários no formato JSON
        /// </summary>
        /// <returns>Retorna todos os dados do usuário no formato JSON</returns>
        [HttpGet]
        public IActionResult GetAction(){
            return Ok(_usuarioRepository.Listar(new string[]{"Perfil"}));

        }
        
        /// <summary>
        /// Cadastra o usuário recebendo os dados no BODY no formato JSON
        /// </summary>
        /// <param name="usuario">Recebe um objeto usuario</param>
        /// <returns>Se cadastrado retorna ok(200) ou se não cadastrou retorna bad request(400)</returns>
        [HttpPost]
        [Route("Cadastrar")]
        public IActionResult Cadastrar([FromBody] UsuarioDomain usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioDomain retornoUsuario = _usuarioRepository.BuscarPorEmail(usuario.Email);
                    if(retornoUsuario != null)
                    {
                        return BadRequest("Email já cadastrado");
                    }
                    _usuarioRepository.Inserir(usuario);
                    return Ok(usuario);
                }

                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                           
                return BadRequest(errors);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody]UsuarioDomain usuario,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {
            try
            {
                
                UsuarioDomain retornoUsuario = _usuarioRepository.UsuarioExiste(usuario.Email, usuario.Password);
                if(retornoUsuario != null)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(retornoUsuario.UsuarioId.ToString(), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, retornoUsuario.UsuarioId.ToString()),
                        new Claim("Id", retornoUsuario.UsuarioId.ToString()),
                        new Claim("Nome", retornoUsuario.Perfil.Nome),
                    });
               
                    identity.AddClaim(new Claim(ClaimTypes.Role, retornoUsuario.Role));

                    var handler = new JwtSecurityTokenHandler();
                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity
                    });
                    var token = handler.WriteToken(securityToken);

                    var retorno = new
                    {
                        authenticated = true,
                        accessToken = token,
                        message = "OK",
                        usuario = retornoUsuario
                    };

                    return Ok(retorno);
                }
                    
                return NotFound("Email ou senha inválido!");
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }           
        
        
        /// <summary>
        /// Para atualizar o usuário é necessário passar o id do usuário que se deseja atualizar e os dados que serão atualizados do usuário no corpo (BODY) no formato JSON
        /// </summary>
        /// <param name="usuario">Novos dados que vão para o usuario</param>
        /// <param name="id"></param>
        /// <returns>Se atualizado retorna ok(200) ou se não cadastrou retorna bad request(400)</returns>
        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] UsuarioDomain usuario, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y=>y.Count>0)
                           .ToList();
                           
                    return BadRequest(errors);
                }

                if(_usuarioRepository.BuscarPorId(id) != null){
                        return NotFound("Usuário nâo encontrado.");
                }

                usuario.UsuarioId = id;
                _usuarioRepository.Atualizar(usuario);
                return Ok(id);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }        
        
        /// <summary>
        /// Deleta o usuário recebendo os dados no BODY no formato JSON
        /// </summary>
        /// <param name="id">id dO usuario que deve ser deletado</param>
        /// <returns>Se deletado retorna ok(200) ou se não deletou retorna bad request(400)</returns>
        [HttpDelete("{id}")]
        public IActionResult Excluir(int id)
        {
            try
            {
                //Busca o usuário do id passado
                var usuario = _usuarioRepository.BuscarPorId(id);

                //Verifica se encontrou o usuário para o id passado, caso na encontre retorna NotFound
                if(usuario == null)
                    return NotFound();

                //Caso tenha encontrado o usuario exclui
                _usuarioRepository.Deletar(usuario);
                return Ok(usuario);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }  
        
    }
}