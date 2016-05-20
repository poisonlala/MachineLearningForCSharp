using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpectationMaximization
{
    class Program
    {
        static void Main(string[] args)
        {
            var model = new GMM(3,15);
            var samples = new List<double>(new double[] {1,1,1,1,1,1,1,55,55,55,55,55,55,55});
            model.BuildGMM(samples);
        }
    }
}
