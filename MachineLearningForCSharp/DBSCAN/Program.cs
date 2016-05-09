using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSCAN
{
    class Program
    {
        static void Main(string[] args)
        {
            List<PointD> list = new List<PointD>();
            list.Add(new PointD(1,1));
            list.Add(new PointD(2,2));
            list.Add(new PointD(3,3));
            list.Add(new PointD(-1,-1));
            list.Add(new PointD(-2,-2));
            list.Add(new PointD(-3,-3));

            DBSCAN dbscan = new DBSCAN(1,1.5);
            var clusters = dbscan.Cluster(list);
        }
    }
}
