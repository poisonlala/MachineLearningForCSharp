using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    class Program
    {
        static void Main(string[] args)
        {
            var matrix = new double[,] { { 3, 3 }, { 4, 3 }, { 1, 2 } };
            var answer = new bool[] { true, true, false };
            var p = new Perceptron(1);
            var canclassify = p.Classify(matrix,answer);
        }
    }
}
