using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YouSchedule.Predictor.Algorithm;
using YouSchedule.Predictor.Models;
using YouSchedule.Predictor.Services;

namespace YouSchedule.Predictor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Predict(string channel)
        {
            YouTubeService service = new YouTubeService();

            // find the videos
            string channelId = await service.FindChannelIdByUsername("SmoshGames");
            IEnumerable<Video> videos = await service.FindVideosByChannel(channelId, DateTimeOffset.UtcNow.AddDays(-28), null);
            // pull more detailed statistics
            List<Video> videosWithStatistics = new List<Video>();
            foreach (var v in videos)
            {
                var video = await service.GetVideoDetails(v);
                video.Calculate();
                videosWithStatistics.Add(video);
            }

            // map the videos to (seconds since sunday, seconds duration)
            var data = videosWithStatistics
                .Select(Mappers.MapVideoToOffsetDuration);

            var testSet1 = string.Join(", ", data.Select(p => "(" + p.Item1 + ", " + p.Item2 + ")"));

            // clustering...

            return Json("");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
