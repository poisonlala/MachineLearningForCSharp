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
            var model = new GMM(2);
            var samples = new List<double>(new double[] { -12,-14,-14,-14,-14,-14,14,14,12,-15,15,14,14,14});
            model.BuildGMM(samples);
        }
    }
}
