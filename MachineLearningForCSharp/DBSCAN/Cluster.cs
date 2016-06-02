using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSCAN
{
    public sealed class Cluster
    {
        private double[] center;
        private double[] sum;
        private List<double[]> cluster;

        public List<double[]> Points => cluster;

        public Cluster()
        {
        }

        public Cluster(double[] p) : this()
        {
            center = new double[p.Length];
            sum = new double[p.Length];
            cluster = new List<double[]>();
            Add(p);
        }

        public void Add(double[] p)
        {
            cluster.Add(p);
            for(int i=0; i < p.Length; i++)
            {
                sum[i] += p[i];
            }
            for(int i = 0; i < p.Length; i++)
            {
                center[i] = sum[i] / cluster.Count;
            }
        }

        public int Count => cluster.Count;

        public double[] Center => center;
    }
}
