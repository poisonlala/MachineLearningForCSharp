using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] input = new double[,] { {-1,-1}, {1,-1}, {-1,1}, {1,1} };
            int[,] label = new int[,] { {1}, {-1}, {-1}, {1} };
            int[] layers = new int[] {2,2};
            BP bp = new BP(input,label,layers);
        }
    }
}
