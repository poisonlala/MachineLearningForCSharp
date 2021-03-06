﻿using System;
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
            double[,] input = new double[,] { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } };
            int[,] label = new int[,] { { 0, 0 }, { 0,1 }, {1, 0 }, { 1, 1 } };
            int[] layers = new int[] { 2 };
            BP bp = new BP(input, label, layers,0.001,100000,0.0005,ActivationFunctionEnum.ReLU);
            bp.Train();
            var result = bp.Predict(new double[] { 0, 1 });
            var result2 = bp.Predict(new double[] { 1, 0 });
            var result1 = bp.Predict(new double[] { 0, 0 });
            var result3 = bp.Predict(new double[] { 1, 1 });
        }
    }
}
