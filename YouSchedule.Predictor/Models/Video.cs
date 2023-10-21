using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace YouSchedule.Predictor.Models
{
    public class Video : IEquatable<Video>
    {
        public string Id { get; set; }
        public DateTimeOffset DateRecordCreated { get; set; }
        public string Title { get; set; }
        public DateTimeOffset DatePublished { get; set; }
        public string Duration { get; set; }
        public int DurationSeconds { get; set; }

        public void Calculate()
        {
            // PT_M_S or PT_S
            var match = Regex.Match(Duration, "PT(\\d+)S");
            if (match != null && match.Success)
            {
                DurationSeconds = int.Parse(match.Groups[1].Value);
            }
            else
            {
                match = Regex.Match(Duration, "PT(\\d+)M(\\d+)S");
                if (match != null && match.Success)
                {
                    DurationSeconds = int.Parse(match.Groups[1].Value) * 60 + int.Parse(match.Groups[2].Value);
                }
                else
                {
                    match = Regex.Match(Duration, "PT(\\d+)H(\\d+)M(\\d+)S");
                    if (match != null && match.Success)
                    {
                        DurationSeconds = int.Parse(match.Groups[1].Value) * 60*60 + int.Parse(match.Groups[2].Value) * 60 + int.Parse(match.Groups[3].Value);
                    }
                }
            }
        }

        public bool Equals([AllowNull] Video other)
        {
            return other != null && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is Video v && v.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id?.GetHashCode() ?? 0;
        }
    }
}
