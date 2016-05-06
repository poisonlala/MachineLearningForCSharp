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
            var samples = new List<double>(new double[] { -67,-48,6,8,14,23,24,28,29,41,49,56,60,75});
            model.BuildGMM(samples);

        }
    }
}
