using ITsOkayAPI.DataAccess;
using ITsOkayAPI.Models;
using ITsOkayAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITsOkayAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class PacientesController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDto _response;

        public PacientesController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDto();

        }

        [HttpGet("GetPacientes")]
        public ResponseDto GetPacientes()
        {
            try
            {
                IEnumerable<Pacientes> pacientes = _context.Pacientes.ToList();
                _response.Data = pacientes;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetPacienteById/{id}")]
        public ResponseDto GetPacienteById(int id)
        {
            try
            {
                var usuario = _context.Pacientes.FirstOrDefault(p => p.IdRelacion == id);
                _response.Data = usuario;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostPaciente")]
        public ResponseDto PostPaciente([FromBody] Pacientes pacientes)
        {
            try
            {
                _context.Pacientes.Add(pacientes);
                _context.SaveChanges();
                _response.Data = pacientes;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("PutPaciente")]
        public ResponseDto PutPaciente([FromBody] Pacientes pacientes)
        {
            try
            {
                _context.Pacientes.Update(pacientes);
                _context.SaveChanges();
                _response.Data = pacientes;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeletePaciente/{id}")]
        public ResponseDto DeletePaciente(int id)
        {
            try
            {
                var pacientes = _context.Pacientes.FirstOrDefault(p => p.IdRelacion == id);
                _context.Remove(pacientes);
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
