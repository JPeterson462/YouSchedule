using System;
using System.Collections.Generic;
using YouSchedule.Predictor.Models;

namespace YouSchedule.Predictor.Algorithm
{
    public class Clustering
    {
        public IntegerGroup Data { get; set; }

        public Clustering(IEnumerable<int> data)
        {
            Data = new IntegerGroup(data);
        }

        public List<IntegerGroup> FindKDiameterGroups(uint k, Func<int, int, int> distanceFn)
        {
            IntegerGroup unassigned = Data.CreateCopy();
            unassigned.Sort();

            List<IntegerGroup> groups = new List<IntegerGroup>();

            // while points remain unassigned,
            while (unassigned.Count > 0)
            {
                List<int> indices = new List<int>();
                // For each point P
                for (int i = 0; i < unassigned.Count; i++)
                {
                    // draw circle C with radius k and count the points inside
                    List<int> Cindices = new List<int>();
                    for (int j = 0; j < unassigned.Count; j++)
                    {
                        if (Math.Abs(distanceFn(unassigned[i], unassigned[j])) <= k)
                        {
                            Cindices.Add(j);
                        }
                    }
                    // track which circle has the most points
                    if (Cindices.Count > indices.Count)
                    {
                        indices = Cindices;
                    }
                }
                IntegerGroup g = new IntegerGroup();
                for (int x = indices.Count - 1; x >= 0; x--)
                {
                    g.Add(unassigned[indices[x]]);
                    unassigned.RemoveAt(indices[x]);
                }

                groups.Add(g);
            }

            return groups;
        }
    }
}
