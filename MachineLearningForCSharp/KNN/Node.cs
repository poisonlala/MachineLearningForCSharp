using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNN
{
    class Node
    {
        public double[] Val { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Parent { get; set; }
        public int Dimension { get; set; }
        public Node(double[] n)
        {
            Val = n;
        }
    }

    class Compare : IComparer<double[]>
    {
        private int index;
        public Compare(int i) : base()
        {
            index = i;
        }

        int IComparer<double[]>.Compare(double[] x, double[] y)
        {
            return x[index] > y[index] ? 1 : -1;
        }
    }
}
