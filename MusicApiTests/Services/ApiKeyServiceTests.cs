using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MusicApi.Services;

namespace MusicApiTests.Services
{
    public class ApiKeyServiceTests
    {
        private readonly IApiKeyService apiKeyService = new ApiKeyService();

        [Fact]
        public void Generate_ReturnStringLengthEqual40()
        {
            string key = apiKeyService.Generate();

            Assert.Equal(40, key.Length);
        }
    }
}
