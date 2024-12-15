using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using DataAccessInterface;
using DataAccessInterface.Gateways;
using DataAccess.DataStructures;
using DataAccess.Enums;

namespace DataAccess.Classes
{
    public class OpenAIAPI : IOpenAIAPI
    {
        private readonly string apiKey;
        private readonly string apiChatEndpoint;
        private readonly string apiEmbeddingEndpoint;
        private IConfig _config { get; set; }

        public OpenAIAPI(IConfig config)
        {
            _config = config;
            apiKey = config.GetOpenAIKey();
            apiChatEndpoint = config.GetOpenAIGenerativeTextEndpoint();
            apiEmbeddingEndpoint = config.GetOpenAIEmbeddingEndpoint();
        }

        public async Task<string> GenerateText(string prompt, int max_tokens = 1000)
        {
            try
            {
                // Confirm this model's availability with OpenAI's documentation
                var result = await GenerateResponse(prompt, "gpt-4o-mini", max_tokens);
                return result;
            }
            catch (Exception ex)
            {
                return $"An error occurred: {ex.Message}";
            }
        }

        public async Task<string[]> GetVectorEmbedding(string input, int max_tokens = 1000)
        {
            try
            {
                // Confirm this model's availability with OpenAI's documentation
                var result = await GetEmbedding(input,"text-embedding-3-large", max_tokens);
                return result;
            }
            catch (Exception ex)
            {
                string[] errorResult = { $"An error occurred: {ex.Message}" };
                return errorResult;
            }
        }

        private async Task<string> GenerateResponse(string prompt, string model, int max_tokens)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                OpenAIMessageContentText system_prompt = new OpenAIMessageContentText("You are a helpful assistant.");
                OpenAIMessage<OpenAIMessageContentText> message0 = new OpenAIMessage<OpenAIMessageContentText>(OpenAIChatRole.System, new OpenAIMessageContentText[]{ system_prompt});

                OpenAIMessageContentText user_prompt = new OpenAIMessageContentText(prompt);
                OpenAIMessage<OpenAIMessageContentText> message1 = new OpenAIMessage<OpenAIMessageContentText>(OpenAIChatRole.User, new OpenAIMessageContentText[] { user_prompt});

                OpenAIMessage<OpenAIMessageContentText>[] message = {message0, message1}; 
                var requestBody = new
                {
                    model = model,
                    messages = message,
                    max_tokens = max_tokens
                };

                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiChatEndpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    var generatedText = doc.RootElement
                                          .GetProperty("choices")[0]
                                          .GetProperty("message")
                                          .GetProperty("content")
                                          .GetString();
                    return generatedText.Trim();
                }
            }
        }

        private async Task<string[]> GetEmbedding(string input, string model, int max_tokens = 1000)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = model, // Confirm this model's availability with OpenAI's documentation
                    input = input,
                    max_tokens = max_tokens
                };

                var jsonContent = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(apiEmbeddingEndpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    //Note! this has not been tested. We probably need to change the path of the json.
                    var generatedText = doc.RootElement
                                          .GetProperty("data")[0]
                                          .GetProperty("embedding")
                                          .GetRawText();
                    return generatedText.Trim().Split(",");
                }
            }
        }
    }
    }
