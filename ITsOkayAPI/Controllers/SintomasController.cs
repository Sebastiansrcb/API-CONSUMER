using ITsOkayAPI.DataAccess;
using ITsOkayAPI.Models;
using ITsOkayAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ITsOkayAPI.Controllers
{
    [Route("api/[controller]")]
    public class SintomasController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDto _response;

        public SintomasController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDto();

        }

        [HttpGet("GetSintomas")]
        public ResponseDto GetSintomas() 
        {
            try
            {
                IEnumerable<Sintoma> sintomas = _context.Sintomas.ToList();
                _response.Data = sintomas;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetSintomaById/{id}")]
        public ResponseDto GetSintomaById(int id)
        {
            try
            {
                var sintoma = _context.Sintomas.FirstOrDefault(s => s.IdSintoma == id);
                _response.Data = sintoma;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetSintomaByUsrName/{Sintoma}")]
        public ResponseDto GetSintomaByName(string Sintoma)
        {
            try
            {
                var sintoma = _context.Sintomas.FirstOrDefault(s => s.Nombre == Sintoma);
                _response.Data = sintoma;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostSintoma")]
        public ResponseDto PostSintoma([FromBody] Sintoma sintoma)
        {
            try
            {
                _context.Sintomas.Add(sintoma);
                _context.SaveChanges();
                _response.Data = sintoma;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("PutSintoma")]
        public ResponseDto PutSintoma([FromBody] Sintoma sintoma)
        {
            try
            {
                _context.Sintomas.Update(sintoma);
                _context.SaveChanges();
                _response.Data = sintoma;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteSintoma/{id}")]
        public ResponseDto DeleteSintoma(int id)
        {
            try
            {
                var sintoma = _context.Sintomas.FirstOrDefault(s => s.IdSintoma == id);
                _context.Remove(sintoma);
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
