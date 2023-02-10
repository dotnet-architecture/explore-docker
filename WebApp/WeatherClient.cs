using System.Text.Json;

namespace WebApp
{
    public class WeatherClient
    {
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        private readonly HttpClient client;
        private readonly ILogger<WeatherClient> _logger;

        public WeatherClient(HttpClient client, ILogger<WeatherClient> logger)
        {
            this.client = client;
            _logger = logger;
        }

        public async Task<WeatherForecast[]> GetWeatherForecastAsync()
        {
            try
            {
                var responseMessage = await this.client.GetAsync("/weatherforecast");
                if (responseMessage != null)
                {
                    var stream = await responseMessage.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<WeatherForecast[]>(stream, options);
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
            return new WeatherForecast[] { };
        }
    }
}
