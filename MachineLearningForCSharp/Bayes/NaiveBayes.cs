using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayes
{
    public sealed class NaiveBayes
    {
        private Dictionary<int, Dictionary<int, double>>[] _matrix;
        private Dictionary<int, Dictionary<int, int>>[] _fmatrix;
        private Dictionary<int, int> _labelmatrix;
        private Dictionary<int, double> _y;
        private int _count;
        private int _dimension;
        public NaiveBayes()
        {
            _y = new Dictionary<int, double>();
            _count = 0;
            _dimension = 0;
        }

        private void Init(int[,] data, int[] label)
        {
            _count = data.GetLength(1);
            _dimension = data.GetLength(0);
            _matrix = new Dictionary<int, Dictionary<int, double>>[_dimension];
            _fmatrix = new Dictionary<int, Dictionary<int, int>>[_dimension];
            _labelmatrix = new Dictionary<int, int>();
        }

        public bool BuildMatrix(int[,] data, int[] label)
        {
            Init(data, label);
            if (label.Length != _count) return false;
            BuildFMatrix(data, label);
            BuildLabelMatrix(label);
            BuildY();
            BuildMatrix();

            return true;
        }

        private void BuildFMatrix(int[,] data, int[] label)
        {
            for (int i = 0; i < _dimension; i++)
            {
                _fmatrix[i] = new Dictionary<int, Dictionary<int, int>>();
                for (int j = 0; j < _count; j++)
                {
                    if (!_fmatrix[i].ContainsKey(data[i, j]))
                        _fmatrix[i].Add(data[i, j], new Dictionary<int, int>());
                    if (!_fmatrix[i][data[i, j]].ContainsKey(label[j]))
                        _fmatrix[i][data[i, j]].Add(label[j], 0);
                    _fmatrix[i][data[i, j]][label[j]]++;
                }
            }
        }

        private void BuildLabelMatrix(int[] label)
        {
            for (int j = 0; j < _count; j++)
            {
                if (!_labelmatrix.ContainsKey(label[j]))
                    _labelmatrix.Add(label[j], 0);
                _labelmatrix[label[j]]++;
            }
        }

        private void BuildMatrix()
        {
            for (int i = 0; i < _dimension; i++)
            {
                _matrix[i] = new Dictionary<int, Dictionary<int, double>>();
                foreach (var dict in _fmatrix[i])
                {
                    if (!_matrix[i].ContainsKey(dict.Key))
                        _matrix[i].Add(dict.Key, new Dictionary<int, double>());
                    foreach (var pair in dict.Value)
                    {
                        _matrix[i][dict.Key][pair.Key] = ((double)pair.Value + 1) / (_labelmatrix[pair.Key] + _fmatrix[i].Count);
                    }
                }
            }
        }
        private void BuildY()
        {
            foreach (var pair in _labelmatrix)
            {
                _y.Add(pair.Key, (double)(pair.Value + 1) / (_count + _labelmatrix.Count));
            }
        }

        public int Predict(int[] p)
        {
            int result = Int32.MinValue;
            double max = -99;
            foreach (var pair in _y)
            {
                double cur = pair.Value;
                for (int i = 0; i < _dimension; i++)
                {
                    cur *= _matrix[i][p[i]][pair.Key];
                }
                if(cur > max)
                {
                    max = cur;
                    result = pair.Key;
                }
            }
            return result;
        }
    }
}
