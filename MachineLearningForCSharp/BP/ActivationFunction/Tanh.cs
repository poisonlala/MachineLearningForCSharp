using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.ActivationFunction
{
    public sealed class Tanh : ActivationFunction
    {
        public double CalculateY(double x)
        {
            double up = Math.Pow(Math.E, x);
            double down = Math.Pow(Math.E, -x);
            return (up - down)/(up + down);
        }

        public double CalculateDeltaY(double x)
        {
            return 1 - Math.Pow(CalculateY(x), 2);
        }
    }
}
