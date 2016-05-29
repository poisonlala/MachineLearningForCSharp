using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticRegression
{
    public sealed class LR
    {
        private static double RATE;
        private static double THRESHOLD;
        private double[] _w;
        private double _b;
        private int _count;
        private int _dim;

        public LR(double rate = 0.001, double threshold = 0.001)
        {

            RATE = rate;
            THRESHOLD = threshold;
        }
        private int GetLabel(bool a)
        {
            return a ? 1 : 0;
        }
        private double InnerMult(double[,] samples, int i)
        {
            double result = 0;
            for (int k = 0; k < _dim; k++)
            {
                result += _w[k] * samples[i, k];
            }
            result += _b;
            return result;
        }

        private double InnerMult(double[] point)
        {
            double result = 0;
            for (int k = 0; k < _dim; k++)
            {
                result += _w[k] * point[k];
            }
            result += _b;
            return result;
        }

        private double UpdatePara(double[,] samples, bool[] labels)
        {
            double loss = 0;
            double[] cache = new double[_count];
            for (int i = 0; i < _count; i++)
            {
                cache[i] = GetLabel(labels[i]) - 1 + (1 / (1 + Math.Pow(Math.PI, InnerMult(samples, i))));
            }

            double nb = 0, nw = 0;
            for (int k = 0; k < _dim; k++)
            {
                for (int i = 0; i < _count; i++)
                {
                    nw += samples[i, k] * cache[i];
                }
                loss += Math.Abs(nw);
                _w[k] += RATE * nw;
            }
            nb = cache.Sum();
            loss += Math.Abs(nb);
            _b += RATE * nb;
            return loss / (_count + 1);
        }

        private void Init(double[,] samples)
        {
            _count = samples.GetLength(0);
            _dim = samples.GetLength(1);
            _w = new double[_dim];
            _b = 0;
        }
        public bool Train(double[,] samples, bool[] labels)
        {
            Init(samples);
            if (labels.Length != _count) return false;
            while (UpdatePara(samples, labels) > THRESHOLD) ;
            return true;
        }

        public double IsPositive(double[] point)
        {
            var tmp = Math.Pow(Math.E, InnerMult(point));
            return 1 - (1 / (1 + tmp));
        }
    }
}
