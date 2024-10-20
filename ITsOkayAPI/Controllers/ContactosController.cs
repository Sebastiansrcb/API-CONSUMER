using ITsOkayAPI.DataAccess;
using ITsOkayAPI.Models.Dto;
using ITsOkayAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITsOkayAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContactosController : Controller
    {
        private readonly AppDbContext _context;
        private ResponseDto _response;

        public ContactosController(AppDbContext context)
        {
            _context = context;
            _response = new ResponseDto();

        }

        [HttpGet("GetContactos")]
        public ResponseDto GetUsuarios()
        {
            try
            {
                IEnumerable<Contactos> contactos = _context.Contactos.ToList();
                _response.Data = contactos;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetContactoById/{id}")]
        public ResponseDto GetContactoById(int id)
        {
            try
            {
                var contacto = _context.Contactos.FirstOrDefault(c => c.IdContacto == id);
                _response.Data = contacto;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost("PostContacto")]
        public ResponseDto PostContacto([FromBody] Contactos contacto)
        {
            try
            {
                _context.Contactos.Add(contacto);
                _context.SaveChanges();
                _response.Data = contacto;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut("PutContacto")]
        public ResponseDto PutContacto([FromBody] Contactos contacto)
        {
            try
            {
                _context.Contactos.Update(contacto);
                _context.SaveChanges();
                _response.Data = contacto;
            }
            catch (Exception ex)
            {
                _response.IsSucces = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete("DeleteContacto/{id}")]
        public ResponseDto DeleteContacto(int id)
        {
            try
            {
                var contacto = _context.Usuarios.FirstOrDefault(c => c.Id == id);
                _context.Remove(contacto);
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
