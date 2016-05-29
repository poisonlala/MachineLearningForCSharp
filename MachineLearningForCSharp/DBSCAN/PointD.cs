using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSCAN
{
    public sealed class PointD
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static PointD operator -(PointD a, PointD b)
        {
            return new PointD(a.X - b.X, a.Y - b.Y);
        }
        public double Dot(PointD p)
        {
            return X * p.X + Y * p.Y;
        }

        public override bool Equals(object obj)
        {
            PointD p = obj as PointD;
            if (p == null) return false;
            return Math.Abs(p.X - X) < 0.00001 && Math.Abs(p.Y - Y) < 0.00001;
        }

        public override int GetHashCode()
        {
            return (int)((X - (int)X) * 100000) + (int)((Y - (int)Y) * 10000);
        }
    }
}
