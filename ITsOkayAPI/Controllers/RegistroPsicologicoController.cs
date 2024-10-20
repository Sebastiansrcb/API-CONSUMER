using ITsOkayAPI.DataAccess;
using ITsOkayAPI.Models;
using ITsOkayAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ITsOkayAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegistroPsicologicoController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDto _response;

        public RegistroPsicologicoController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDto();

        }
        [HttpGet("GetRegistrosPsicologicos")]
        public ResponseDto GetRegistrosPsicologicos()
        {
            try
            {
                IEnumerable<RegistroPsicologico> registroPsicologicos = _context.RegistrosPsicologicos.ToList();
                _response.Data = registroPsicologicos;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetRegistroPsicologicoById/{id}")]
        public ResponseDto GetRegistroPsicologicoById(int id)
        {
            try
            {
                var registroPsicologico = _context.RegistrosPsicologicos.FirstOrDefault(rp => rp.IdRegistro == id);
                _response.Data = registroPsicologico;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostRegistroPsicologico")]
        public ResponseDto PostRegistroPsicologico([FromBody] RegistroPsicologico registroPsicologico)
        {
            try
            {
                _context.RegistrosPsicologicos.Add(registroPsicologico);
                _context.SaveChanges();
                _response.Data = registroPsicologico;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("PutRegistroPsicologico")]
        public ResponseDto PutRegistroPsicologico([FromBody] RegistroPsicologico registroPsicologico)
        {
            try
            {
                _context.RegistrosPsicologicos.Update(registroPsicologico);
                _context.SaveChanges();
                _response.Data = registroPsicologico;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteRegistroPsicologico/{id}")]
        public ResponseDto DeleteRegistroPsicologico(int id)
        {
            try
            {
                var registroPsicologico = _context.RegistrosPsicologicos.FirstOrDefault(rp => rp.IdRegistro == id);
                _context.Remove(registroPsicologico);
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
