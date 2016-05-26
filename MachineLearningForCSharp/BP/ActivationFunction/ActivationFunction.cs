using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BP.ActivationFunction
{
    public interface ActivationFunction
    {
        double CalculateY(double x);
        double CalculateDeltaY(double x);
    }
}
