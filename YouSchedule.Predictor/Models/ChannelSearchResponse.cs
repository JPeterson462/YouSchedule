using System;
using System.Collections.Generic;

namespace YouSchedule.Predictor.Models
{
    public class ChannelSearchResponse : SearchResponse
    {
        public List<ChannelSearchResponseResult> Items { get; set; }
    }

    public class ChannelSearchResponseResult : SearchResponseResult
    {
        public string Id { get; set; }
    }
}
