using ITsOkay.Models;
using ITsOkay.Services.IServices;
using ITsOkay.Utility;
using ITsOkayAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace ITsOkay.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IHttpClientFactory _clientFactory;
        public UsuarioService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            var response =new ResponseDto();
            try
            {
                HttpClient client = _clientFactory.CreateClient("ITsOkayAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDto.Url);

                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                switch (requestDto.ApiType)
                {
                    case SD.ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                HttpResponseMessage apiResponse = await client.SendAsync(message);

                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        response.IsSuccess = false;
                        response.Message = "Not Found";
                        break;
                    case System.Net.HttpStatusCode.Unauthorized:
                        response.IsSuccess = false;
                        response.Message = "Unauthorized";
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        response.IsSuccess = false;
                        response.Message = "Access Denied";
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        response.IsSuccess = false;
                        response.Message = "Internal Server Error";
                        break;
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        break;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto?> GetUsuariosAsync()
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ItsOkayApiBase + "api/Usuario/GetUsuarios",
            });
        }

        public async Task<ResponseDto?> GetUsuarioByIdAsync(int id)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ItsOkayApiBase + $"api/Usuario/GetUsuarioById/{id}",
            });
        }

        public async Task<ResponseDto?> GetUsuarioByNameAsync(string usrName)
        {
            return await SendAsync(new RequestDto() 
            { 
                ApiType = SD.ApiType.GET,
                Url = SD.ItsOkayApiBase + $"api/Usuario/GetUsuarioByUsrName/{usrName}" 
            });
        }

        public async Task<ResponseDto?> PostUsuarioAsync(Usuario usuario)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.ItsOkayApiBase + "api/Usuario/PostUsuario",
                Data = usuario,
            });
        }

        public async Task<ResponseDto?> PutUsuarioAsync(Usuario usuario)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Url = SD.ItsOkayApiBase + "api/Usuario/PutUsuario",
                Data = usuario,
            });
        }

        public async Task<ResponseDto?> DeleteUsuarioAsync(int id)
        {
            return await SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ItsOkayApiBase + $"api/Usuario/DeleteUsuario/{id}",
            });
        }


    }
}
