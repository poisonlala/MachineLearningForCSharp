using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticRegression
{
    class Program
    {
        static void Main(string[] args)
        {
            BinomialLR lr = new BinomialLR(0.001,0.008);
            var samples = new double[,]
            {
                {1,2}, {1,3 }, {1,4 }, {2,3 }, {2,4 }, {3,4 }, {3.2,3 },//+1
                {2,1 }, {3,1 }, {3,2 }, {4,1 }, {4,2 }, {4,3 }, {2,2.3 }//0
            };
            var labels = new bool[] {true, true, true, true, true, true, true,
                                        false,false,false,false,false,false,false};
            lr.Train(samples, labels);
           var p1 = lr.IsPositive(new double[] { 1, -1 });
            var p2 = lr.IsPositive(new double[] {1, 10 });
            var p3 = lr.IsPositive(new double[] { 2, 2 });
        }
    }
}
