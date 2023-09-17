using System.Text.Json.Serialization;

namespace NdbPortal.Entities.Dtos.Login
{
    public class LoginInfoResponse
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("employeeId")]
        public Guid EmployeeId { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
    }
}
