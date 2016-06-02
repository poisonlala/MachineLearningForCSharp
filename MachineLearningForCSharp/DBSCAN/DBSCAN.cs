using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSCAN
{
    public sealed class DBSCAN
    {
        private List<Cluster> clusters;
        protected List<double[]> points;
        private BitArray flag;
        private readonly int MIN_POINTS = 0;
        private readonly double MIN_DISTANCE = 0;
        private DBSCAN()
        {
            clusters = new List<Cluster>();
        }

        public DBSCAN(int minPoints, double minDistance) : this()
        {
            MIN_POINTS = minPoints;
            MIN_DISTANCE = minDistance;
        }
        public List<Cluster> Cluster(List<double[]> pts)
        {
            points = pts;
            flag = new BitArray(points.Count);
            DoCluster();
            Filter();
            return clusters;
        }

        private void DoCluster()
        {
            for (int i = 0; i < flag.Count; i++)
            {
                if (!flag[i])
                {
                    Cluster c = new Cluster(points[i]);
                    flag[i] = true;
                    ExpandNodes(c, i);
                    clusters.Add(c);
                }
            }
        }

        private void ExpandNodes(Cluster c, int index)
        {
            double[] p = points[index];
            for (int i = 0; i < flag.Count; i++)
            {
                if (!flag[i] && Distance(p, points[i]) <= MIN_DISTANCE)
                {
                    flag[i] = true;
                    c.Add(points[i]);
                    ExpandNodes(c, i);
                }
            }
        }

        private double Distance(double[] center, double[] p)
        {
            double sum = 0;
            for(int i = 0; i < center.Length; i++)
            {
                sum += Math.Pow(center[i] - p[i], 2);
            }
            return Math.Sqrt(sum);
        }

        private void Filter()
        {
            for (int i = 0; i < clusters.Count;)
            {
                if (clusters[i].Count < MIN_POINTS)
                    clusters.RemoveAt(i);
                else
                    i++;
            }
        }
    }
}
