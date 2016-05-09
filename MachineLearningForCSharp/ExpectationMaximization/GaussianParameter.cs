using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpectationMaximization
{
    class GaussianParameter
    {
        public double Avr { get; set; }
        public double Var { get; set; }

        public double Probability { get; set; }
        public GaussianParameter(double p,double a = 0, double v = 10)
        {
            Avr = a;
            Var = v;
            Probability = p;
        }
    }
}
