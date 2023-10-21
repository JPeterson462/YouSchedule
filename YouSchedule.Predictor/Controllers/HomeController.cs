using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YouSchedule.Predictor.Algorithm;
using YouSchedule.Core;
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
            /*
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

            var testSet1 = string.Join(", ", data.Select(p => "new Tuple<int, int>(" + p.Item1 + ", " + p.Item2 + ")"));
            */
            var testSet1Mapped = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(241204, 54), new Tuple<int, int>(154810, 33), new Tuple<int, int>(586804, 56), new Tuple<int, int>(154809, 49), new Tuple<int, int>(61227, 2164), new Tuple<int, int>(320432, 2509), new Tuple<int, int>(61213, 2714), new Tuple<int, int>(61211, 3312), new Tuple<int, int>(320432, 2827), new Tuple<int, int>(430644, 5686), new Tuple<int, int>(61214, 2441), new Tuple<int, int>(431435, 6561), new Tuple<int, int>(586801, 55), new Tuple<int, int>(493214, 1856), new Tuple<int, int>(320428, 2418), new Tuple<int, int>(345614, 17), new Tuple<int, int>(21, 34), new Tuple<int, int>(345602, 21), new Tuple<int, int>(586814, 53), new Tuple<int, int>(154806, 58), new Tuple<int, int>(154811, 19), new Tuple<int, int>(241230, 21), new Tuple<int, int>(414012, 22), new Tuple<int, int>(345606, 55), new Tuple<int, int>(586804, 47), new Tuple<int, int>(493222, 1852), new Tuple<int, int>(430703, 5732), new Tuple<int, int>(493213, 3736), new Tuple<int, int>(414014, 44), new Tuple<int, int>(320410, 6004), new Tuple<int, int>(241210, 28), new Tuple<int, int>(345614, 53), new Tuple<int, int>(431702, 6738), new Tuple<int, int>(241231, 43)
            };

            PredictorService predictor = new PredictorService();
            predictor.DataPoints = testSet1Mapped
                .Select(t => new PredictorDataPoint() { SecondsSinceSunday = t.Item1, DurationOfVideo = t.Item2 }).ToList();

            var prediction1 = predictor.FindLongformVideoSchedule();
            var prediction2 = predictor.FindShortformVideoSchedule();

            return Json(new CompositePredictedSchedule
            {
                HighPredictionLongform = prediction1.Item2,
                LowPredictionLongform = prediction1.Item1,
                HighPredictionShortform = prediction2.Item2,
                LowPredictionShortform = prediction2.Item1
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
