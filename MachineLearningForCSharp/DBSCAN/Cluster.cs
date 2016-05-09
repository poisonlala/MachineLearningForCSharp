using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSCAN
{
    class Cluster
    {
        private PointD center;
        private double sumX;
        private double sumY;
        private List<PointD> cluster;

        public List<PointD> Points => cluster;

        public Cluster()
        {
            center = new PointD(0, 0);
            cluster = new List<PointD>();
            sumX = 0;
            sumY = 0;
        }

        public Cluster(PointD p) : this()
        {
            Add(p);
        }

        public void Add(PointD p)
        {
            cluster.Add(p);
            sumX += p.X;
            sumY += p.Y;
            center.X = sumX / cluster.Count;
            center.Y = sumY / cluster.Count;
        }

        public void Merge(Cluster c)
        {
            foreach (PointD p in c.cluster)
            {
                Add(p);
            }
        }

        public int Count => cluster.Count;

        public PointD Center => center;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            PointAppend(sb, center);
            sb.AppendLine();
            foreach (var p in cluster)
            {
                PointAppend(sb, p);
            }

            return sb.ToString();
        }

        private void PointAppend(StringBuilder sb, PointD p)
        {
            sb.Append(p.X);
            sb.Append(",");
            sb.Append(p.Y);
            sb.AppendLine();
        }
    }
}
