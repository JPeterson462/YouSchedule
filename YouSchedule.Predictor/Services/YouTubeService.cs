using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YouSchedule.Predictor.Models;

namespace YouSchedule.Predictor.Services
{
    public class YouTubeService
    {
        private string ApiKey = "<API KEY>";

        private string BaseUrl = "https://youtube.googleapis.com/youtube/v3";
        private HttpClient httpClient;

        public YouTubeService()
        {
            httpClient = new HttpClient()
            {
                //BaseAddress = new Uri(BaseUrl),
                Timeout = TimeSpan.FromMinutes(1)
            };
        }

        public async Task<string> FindChannelIdByUsername(string username)
        {
            //channels?part=id&forUsername=SmoshGames&key=[YOUR_API_KEY]
            var response = await httpClient.GetAsync(BaseUrl + "/channels?part=id&forUsername=" + username + "&key=" + ApiKey);
            var message = await response.Content.ReadAsStringAsync();
            ChannelSearchResponse result = JsonConvert.DeserializeObject<ChannelSearchResponse>(message);
            if (result?.Items?.Count == 1)
            {
                return result.Items[0].Id;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<Video>> FindVideosByChannel(string id, DateTimeOffset start, DateTimeOffset? end)
        {
            //search?part=snippet&key=[YOUR_API_KEY]
            string publishDateFilter = "publishedAfter=" + start.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            if (end != null)
            {
                publishDateFilter += "&publishedBefore=" + end.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ");
            }
            string queryUrl = $"/search?part=snippet&channelId={id}&maxResults=50&{publishDateFilter}&type=video&key=" + ApiKey;
            var response = await httpClient.GetAsync(BaseUrl + queryUrl);
            var message = await response.Content.ReadAsStringAsync();
            List<VideoSearchResponseResult> results = new List<VideoSearchResponseResult>();
            VideoSearchResponse result = JsonConvert.DeserializeObject<VideoSearchResponse>(message);
            results.AddRange(result.Items);
            while (!string.IsNullOrWhiteSpace(result.NextPageToken))
            {
                response = await httpClient.GetAsync(BaseUrl + queryUrl + "&pageToken=" + result.NextPageToken);
                message = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<VideoSearchResponse>(message);
            }
            return results.Select(r => new Video
            {
                Id = r.Id.VideoId,
                Title = r.Snippet.Title,
                DatePublished = r.Snippet.PublishTime
            }).Distinct();
        }

        public async Task<Video> GetVideoDetails(Video video)
        {
            //videos?part=contentDetails&id=tZbfxNUSDk8&key=[YOUR_API_KEY]
            string queryUrl = $"/videos?part=contentDetails&id={video.Id}&key=" + ApiKey;
            var response = await httpClient.GetAsync(BaseUrl + queryUrl);
            var message = await response.Content.ReadAsStringAsync();
            VideoDetailsSearchResponse result = JsonConvert.DeserializeObject<VideoDetailsSearchResponse>(message);
            var details = result.Items.Single();
            video.Duration = details.ContentDetails.Duration;
            return video;
        }
    }
}