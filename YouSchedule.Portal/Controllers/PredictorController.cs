using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using YouSchedule.Core;

namespace YouSchedule.Portal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictorController : ControllerBase
    {
        private readonly ILogger<PredictorController> _logger;

        public PredictorController(ILogger<PredictorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<CompositePredictedSchedule> Get()
        {
            //https://localhost:5003/Home/predict?channel=@SmoshGames
            HttpClient client = new HttpClient();
            var response = await client.PostAsync("https://localhost:5003/Home/predict?channel=@SmoshGames", new StringContent(""));
            var jsonAsString = await response.Content.ReadAsStringAsync();
            CompositePredictedSchedule schedule = JsonConvert.DeserializeObject<CompositePredictedSchedule>(jsonAsString);
            return schedule;
        }
    }
}
