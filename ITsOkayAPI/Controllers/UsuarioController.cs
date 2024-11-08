﻿using ITsOkayAPI.DataAccess;
using ITsOkayAPI.Models;
using ITsOkayAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITsOkayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private ResponseDto _response;

        public UsuarioController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _response = new ResponseDto();
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            // Valida las credenciales del usuario
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombre == loginDto.Username && u.Password == loginDto.Password);

            if (usuario == null)
            {
                return Unauthorized(new { Message = "Credenciales inválidas" });
            }

            var token = GenerateJwtToken(usuario.Nombre);
            return Ok(new { Token = token });
        }

        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expirationMinutes = int.Parse(_configuration["Jwt:TokenExpirationInMinutes"]);
            var expiration = DateTime.Now.AddMinutes(expirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // Guarda el token en la base de datos para el usuario
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombre == username);
            if (usuario != null)
            {
                usuario.LastToken = jwtToken;
                _context.SaveChanges();
            }

            return jwtToken;
        }

        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }



    [HttpGet("GetUsuarios")]
        public ResponseDto GetUsuarios()
        {
            try
            {
                IEnumerable<Usuario> usuarios = _context.Usuarios.ToList();
                _response.Data = usuarios;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetUsuarioById/{id}")]
        public ResponseDto GetUsuarioById(int id)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
                _response.Data = usuario;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetUsuarioByUsrName/{usrName}")]
        public ResponseDto GetUsuarioByName(string usrName)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Nombre == usrName);
                _response.Data = usuario;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostUsuario")]
        public ResponseDto PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                _response.Data = usuario;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("PutUsuario")]
        public ResponseDto PutUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                _response.Data = usuario;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteUsuario/{id}")]
        public ResponseDto DeleteUsuario(int id)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
                _context.Remove(usuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
