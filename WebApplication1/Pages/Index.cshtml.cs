using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public WeatherForecast[] WeatherForecasts { get; set; }
        public string? ErrorMessage { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet([FromServices] WeatherClient client)
        {
            WeatherForecasts = await client.GetWeatherForecastAsync();

            if (WeatherForecasts.Count() == 0)
                ErrorMessage = "Try again tomorrow.";
            else
                ErrorMessage = string.Empty;
        }
    }
}