using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.ActivationFunction
{
    public sealed class ReLU : ActivationFunction
    {
        public double CalculateY(double x)
        {
            return x > 0 ? x : 0.01 * x;
        }

        public double CalculateDeltaY(double x)
        {
            return x <= 0 ? 0.01 : 1;
        }
    }
}
