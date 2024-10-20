using ITsOkayAPI.DataAccess;
using ITsOkayAPI.Models.Dto;
using ITsOkayAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITsOkayAPI.Controllers
{
    [Route("api/[controller]")]
    public class RegistroSintomasController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDto _response;

        public RegistroSintomasController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDto();

        }

        [HttpGet("GetRegistrosSintomas")]
        public ResponseDto GetRegistrosSintomas()
        {
            try
            {
                IEnumerable<RegistroSintomas> registroSintomas = _context.RegistrosSintomas.ToList();
                _response.Data = registroSintomas;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetRegistroSintomaById/{id}")]
        public ResponseDto GetRegistroSintomaById(int id)
        {
            try
            {
                var registroSintoma = _context.RegistrosSintomas.FirstOrDefault(rs => rs.Id == id);
                _response.Data = registroSintoma;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostRegistroSintoma")]
        public ResponseDto PostRegistroSintoma([FromBody] RegistroSintomas registroSintomas)
        {
            try
            {
                _context.RegistrosSintomas.Add(registroSintomas);
                _context.SaveChanges();
                _response.Data = registroSintomas;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("PutRegistroSintomas")]
        public ResponseDto PutRegistroSintomas([FromBody] RegistroSintomas registroSintomas)
        {
            try
            {
                _context.RegistrosSintomas.Update(registroSintomas);
                _context.SaveChanges();
                _response.Data = registroSintomas;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteRegistroSintomas/{id}")]
        public ResponseDto DeleteRegistroSintomas(int id)
        {
            try
            {
                var registroSintomas = _context.RegistrosSintomas.FirstOrDefault(rs => rs.Id == id);
                _context.Remove(registroSintomas);
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
