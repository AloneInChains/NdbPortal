﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using NdbPortal.Web.Contracts;
using NdbPortal.Web.Models;
using NdbPortal.Entities.Models;
using System.Text;
using NdbPortal.Entities.Dtos.Login;
using System.Text.Json;
using NdbPortal.Entities.Dtos.NormativeDocument;

namespace NdbPortal.Web
{
    public class WebApiClient : IWebApiClient
    {

        private readonly HttpClient _httpClient;

        public WebApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateAsync(LoginInfo loginInfo)
        {
            if (loginInfo == null || string.IsNullOrEmpty(loginInfo.Email) || string.IsNullOrEmpty(loginInfo.Password))
            {
                throw new ArgumentNullException("Not full credentials");
            }

            HttpRequestMessage request = new(HttpMethod.Post, "authenticate")
            {
                Content = new StringContent(JsonSerializer.Serialize(loginInfo), Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var authResult = JsonSerializer.Deserialize<LoginInfoResponse>(await response.Content.ReadAsStringAsync());
            if (authResult != null)
            {
                if (authResult.Code == 0)
                {
                    return authResult.Token;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public async Task<Employee?> GetEmployeeByIdAsync(string token, Guid id)
        {
            try
            {
                HttpRequestMessage request = new(HttpMethod.Get, $"Employees/{id}");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadFromJsonAsync<Employee?>();

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<NormativeDocumentGetWithDetailsDto>?> GetNormativeDocumentsByEmployeeIdAsync(string token, Guid employeeId)
        {
            try
            {
                if (token == null)
                {
                    return null;
                }

                HttpRequestMessage request = new(HttpMethod.Get, $"Misc/GetNormativeDocumentsByEmployeeId?employeeId={employeeId}");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return JsonSerializer.Deserialize<IEnumerable<NormativeDocumentGetWithDetailsDto>>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return new List<NormativeDocumentGetWithDetailsDto>();
            }
        }

        public async Task<Guid?> ValidateTokenAsync(string? token)
        {
            try
            {

                if (token == null)
                {
                    return null;
                }

                HttpRequestMessage request = new(HttpMethod.Post, "validate-token")
                {
                    Content = new StringContent(JsonSerializer.Serialize(new TokenRequest { Token = token }), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string? employee = JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());

                if (employee == null)
                {
                    throw new Exception("Token validation failed. Employee is null");
                }

                return new Guid(employee);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<T?> GetEntityRecordsAsync<T>(string entityPath, string token)
        {
            try
            {

                if (string.IsNullOrEmpty(token))
                {
                    return default(T);
                }

                HttpRequestMessage request = new(HttpMethod.Get, entityPath);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());

            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public async Task AddRecordAsync<T>(string entityName, T record, string token)
        {
            try
            {

                if (string.IsNullOrEmpty(entityName))
                {
                    throw new ArgumentNullException(nameof(entityName));
                }

                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException(nameof(token));
                }

                HttpRequestMessage request = new(HttpMethod.Post, entityName)
                {
                    Content = new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json")
                };
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task EditRecordAsync<T>(string entityName, Guid id, T record, string token)
        {
            try
            {

                if (string.IsNullOrEmpty(entityName))
                {
                    throw new ArgumentNullException(nameof(entityName));
                }

                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException(nameof(token));
                }

                HttpRequestMessage request = new(HttpMethod.Put, $"{entityName}/{id}")
                {
                    Content = new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json")
                };
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

                var response = await _httpClient.SendAsync(request);

            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task DeleteRecordAsync(string entityPath, Guid id, string token)
        {

            if (string.IsNullOrEmpty(entityPath))
            {
                throw new ArgumentNullException(nameof(entityPath));
            }

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            HttpRequestMessage request = new(HttpMethod.Delete, $"{entityPath}/{id}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

            var response = await _httpClient.SendAsync(request);
                
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new ConfictDbDeletionException();
            }

            response.EnsureSuccessStatusCode();

        }

        public async Task<T?> GetPrimitiveValueAsync<T>(string path, string token)
        {
            try
            {

                if (string.IsNullOrEmpty(path))
                {
                    throw new ArgumentNullException(nameof(path));
                }

                if (string.IsNullOrEmpty(token))
                {
                    throw new ArgumentNullException(nameof(token));
                }

                HttpRequestMessage request = new(HttpMethod.Get, path);
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(responseString);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }


    [Serializable]
    public class ConfictDbDeletionException : Exception
    {
        public ConfictDbDeletionException() { }
        public ConfictDbDeletionException(string message) : base(message) { }
        public ConfictDbDeletionException(string message, Exception inner) : base(message, inner) { }
        protected ConfictDbDeletionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}