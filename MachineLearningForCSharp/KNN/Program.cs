using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            var points = new List<double[]>();
            points.Add(new double[] { 2, 3 });
            points.Add(new double[] { 5, 4 });
            points.Add(new double[] { 9, 6 });
            points.Add(new double[] { 4, 7 });
            points.Add(new double[] { 8, 1 });
            points.Add(new double[] { 7, 2 });

            var knn = new KNN();
            knn.BuildKNN(points);
            var result = knn.SearchClosest(new double[] { 2, 4.5 });
            var result1 = knn.SearchClosest(new double[] { 5, 3 });
        }
    }
}
