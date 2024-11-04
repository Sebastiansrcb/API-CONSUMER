using Microsoft.AspNetCore.Mvc;
using static ITsOkay.Utility.SD;

namespace ITsOkay.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object? Data { get; set; }
    }
}
