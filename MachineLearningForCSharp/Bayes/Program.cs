using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayes
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] samples = new int[,]
            {
                {1,1,1,1,1,2,2,2,2,2,3,3,3,3,3 }, { 1,2,2,1,1,1,2,2,3,3,3,2,2,3,3}
            };
            int[] label = new int[] { -1, -1, 1, 1, -1, -1, -1, 1, 1, 1, 1, 1, 1, 1, -1 };
            var nb = new NaiveBayes();
            nb.BuildMatrix(samples, label);
            int lb = nb.Predict(new int[] { 2, 1 });
        }
    }
}
