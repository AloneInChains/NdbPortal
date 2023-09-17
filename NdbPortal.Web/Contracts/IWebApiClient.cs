using NdbPortal.Entities.Dtos.Login;
using NdbPortal.Entities.Dtos.NormativeDocument;
using NdbPortal.Entities.Models;

namespace NdbPortal.Web.Contracts
{
    public interface IWebApiClient
    {
        Task<string> AuthenticateAsync(LoginInfo loginInfo);

        Task<Guid?> ValidateTokenAsync(string? token);

        Task<Employee?> GetEmployeeByIdAsync(string token, Guid id);

        Task<IEnumerable<NormativeDocumentGetWithDetailsDto>?> GetNormativeDocumentsByEmployeeIdAsync(string token, Guid employeeId);

        Task<T?> GetEntityRecordsAsync<T>(string entityPath, string token);

        Task AddRecordAsync<T>(string entityName, T record, string token);

        Task EditRecordAsync<T>(string entityName, Guid id, T record, string token);

        Task DeleteRecordAsync(string entityPath, Guid id, string token);

        Task<T?> GetPrimitiveValueAsync<T>(string path, string token);


    }
}
