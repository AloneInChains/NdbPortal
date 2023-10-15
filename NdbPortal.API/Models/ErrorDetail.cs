using Newtonsoft.Json;

namespace NdbPortal.API.Models
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } = null;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
