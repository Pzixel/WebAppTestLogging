using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebAppTestLogging.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly GoogleClient _googleClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, GoogleClient googleClient)
        {
            _logger = logger;
            _googleClient = googleClient;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var response = await _googleClient.Hello();
            return response;
        }
    }
}
