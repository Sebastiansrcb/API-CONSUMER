using ITsOkay.Models;
using ITsOkayAPI.Models;

namespace ITsOkay.Services.IServices
{
    public interface IUsuarioService
    {
        Task<ResponseDto?> GetUsuariosAsync();
        Task<ResponseDto?> GetUsuarioByIdAsync(int id);
        Task<ResponseDto?> GetUsuarioByNameAsync(string usrName);
        Task<ResponseDto?> PostUsuarioAsync(Usuario usuario);
        Task<ResponseDto?> PutUsuarioAsync(Usuario usuario);
        Task<ResponseDto?> DeleteUsuarioAsync(int id);
    }
}
