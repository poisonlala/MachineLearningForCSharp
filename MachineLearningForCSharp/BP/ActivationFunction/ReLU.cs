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
            return Math.Max(0, x);
        }

        public double CalculateDeltaY(double x)
        {
            return x <= 0 ? 0 : 1;
        }
    }
}
