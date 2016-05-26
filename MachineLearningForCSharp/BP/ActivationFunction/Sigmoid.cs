using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.ActivationFunction
{
    public sealed class Sigmoid : ActivationFunction
    {
        public double CalculateY(double x)
        {
            return 1/(1 + Math.Pow(Math.E, -x));
        }

        public double CalculateDeltaY(double x)
        {
            double y = CalculateY(x);
            return y*(1 - y);
        }
    }
}
