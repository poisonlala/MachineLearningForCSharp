using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayes
{
    class NaiveBayes
    {
        private Dictionary<int, Dictionary<int,double>>[] _matrix;
        private Dictionary<int, double> _y;
        private int _count;
        private int _dimension;
        public NaiveBayes()
        {
            _y = new Dictionary<int, double>();
            _count = 0;
            _dimension = 0;
        }

        public bool BuildMatrix(int[,] data, int[] label)
        {
            _matrix = new Dictionary<int, Dictionary<int, double>>[_dimension] ;
            _count = data.GetLength(0);
            _dimension = data.GetLength(1);
            if (label.Length != _dimension) return false;




            return true;
        }
    }
}
