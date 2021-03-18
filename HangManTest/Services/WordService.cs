using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HangMan.Services
{
    public class WordService : IWordService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;

        public WordService(IHttpClientFactory clientFactory, ILogger<WordService> logger)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<string> GetWord()
        {
            var endPoint = @"https://random-word-api.herokuapp.com/word?number=1"; // todo : add to app settings
            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, endPoint);

                var response = await SendRequest(requestMessage);

                var reponseText = await response.Content.ReadAsStringAsync();

                var word = DeserialiseResponse(reponseText);

                return word.ToUpper();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error getting a new word from {endPoint}", ex);
            }
        }

        private static string DeserialiseResponse(string reponseText)
        {
            var words = JsonConvert.DeserializeObject<string[]>(reponseText);

            return words[0].ToString();
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage requestMessage)
        {
            var client = GetHttpClient();

            return await client.SendAsync(requestMessage);
        }

        private HttpClient GetHttpClient()
        {
            return _clientFactory.CreateClient();
        }
    }
}