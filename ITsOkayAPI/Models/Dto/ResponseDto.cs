namespace ITsOkayAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public bool IsSucces {  get; set; } = true;
        public string Message { get; set; } = "";
    }
}
