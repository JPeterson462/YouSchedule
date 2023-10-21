using System;
using System.Collections.Generic;
using System.Linq;
using YouSchedule.Core;
using YouSchedule.Predictor.Algorithm;
using YouSchedule.Predictor.Models;

namespace YouSchedule.Predictor.Services
{
    public class PredictorService
    {
        public List<PredictorDataPoint> DataPoints { get; set; }

        private PredictedSchedule DeriveScheduleFromClusters(List<IntegerGroup> groups, IList<PredictorDataPoint> points)
        {
            PredictedSchedule schedule = new PredictedSchedule();
            schedule.Videos = new List<PredictedVideo>();
            int largestGroup = groups.Max(g => g.Count);
            foreach (var group in groups)
            {
                var pointsInGroup = points.Where(p => group.Contains(p.SecondsSinceSunday));
                schedule.Videos.Add(new PredictedVideo
                {
                    MinimumDurationSeconds = pointsInGroup.Min(p => p.DurationOfVideo),
                    AverageDurationSeconds = (int)Math.Floor(pointsInGroup.Average(p => p.DurationOfVideo)),
                    MaximumDurationSeconds = pointsInGroup.Max(p => p.DurationOfVideo),

                    MinimumPostTime = new RelativeDateTime(pointsInGroup.Min(p => p.SecondsSinceSunday)),
                    AveragePostTime = new RelativeDateTime((int)Math.Floor(pointsInGroup.Average(p => p.SecondsSinceSunday))),
                    MaximumPostTime = new RelativeDateTime(pointsInGroup.Max(p => p.SecondsSinceSunday)),

                    Likelihood = (double)group.Count / largestGroup
                });
            }
            schedule.Videos = schedule.Videos.OrderBy(p => p.MinimumPostTime.SecondsSinceSunday).ToList();
            return schedule;
        }

        public Tuple<PredictedSchedule, PredictedSchedule> FindLongformVideoSchedule()
        {
            List<PredictorDataPoint> points = DataPoints.Where(p => p.DurationOfVideo > 60).ToList();
            Clustering c = new Clustering(points.Select(p => p.SecondsSinceSunday));
            var g1 = c.FindKDiameterGroups(720, (x, y) => Utils.FindModulusDistance(x, y, Utils.SecondsPerWeek));
            var g2 = c.FindKDiameterGroups(60, (x, y) => Utils.FindModulusDistance(x, y, Utils.SecondsPerWeek));
            return new Tuple<PredictedSchedule, PredictedSchedule>(
                DeriveScheduleFromClusters(g1, points),
                DeriveScheduleFromClusters(g2, points)
            );
        }

        public Tuple<PredictedSchedule, PredictedSchedule> FindShortformVideoSchedule()
        {
            List<PredictorDataPoint> points = DataPoints.Where(p => p.DurationOfVideo <= 60).ToList();
            Clustering c = new Clustering(points.Select(p => p.SecondsSinceSunday));
            var g1 = c.FindKDiameterGroups(720, (x, y) => Utils.FindModulusDistance(x, y, Utils.SecondsPerWeek));
            var g2 = c.FindKDiameterGroups(60, (x, y) => Utils.FindModulusDistance(x, y, Utils.SecondsPerWeek));
            return new Tuple<PredictedSchedule, PredictedSchedule>(
                DeriveScheduleFromClusters(g1, points),
                DeriveScheduleFromClusters(g2, points)
            );
        }
    }
}
