using System.Security.Cryptography;

using MusicApi.DTOs;

namespace MusicApi.Services
{
    public class ApiKeyService : IApiKeyService
    {
        public string Generate()
        {
            byte[] bytes = RandomNumberGenerator.GetBytes(30);
            return Convert.ToBase64String(bytes);
        }
    }
}
