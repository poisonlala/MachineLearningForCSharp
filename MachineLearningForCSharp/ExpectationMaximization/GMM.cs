using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpectationMaximization
{
    public sealed class GMM
    {
        private static double CONSTANT = 1 / (Math.Sqrt(2 * Math.PI));
        private double[,] Q;
        private int dim;
        private int count;
        private double MINIMUM;
        private GaussianParameter[] p;
        public GaussianParameter[] GaussianPara { get { return p; } }

        public GMM(int n, double threhold = 0.01)
        {
            dim = n;
            MINIMUM = threhold;
            p = new GaussianParameter[n];
            for (int i = 0; i < n; i++)
                p[i] = new GaussianParameter((double)1 / n);
        }
        public GMM(int[,] para, double threhold = 0.01)
        {
            dim = para.GetLength(1);
            MINIMUM = threhold;
            p = new GaussianParameter[dim];
            for (int i = 0; i < dim; i++)
                p[i] = new GaussianParameter(para[0,i],para[1,i],para[2,i]);
        }
        private double GaussianDistribution(GaussianParameter p, double x)
        {
            var result = (CONSTANT / Math.Sqrt(p.Var)) * Math.Pow(Math.E, (-Math.Pow(x - p.Avr, 2) / (2 * p.Var)));
            return result;
        }
        private void Init(IList<double> samples)
        {
            double min = samples.Min();
            double max = samples.Max();
            double derta = (max - min) / (dim + 1);
            for (int i = 0; i < dim; i++)
            {
                p[i].Avr += min + derta * (i + 1);
            }
        }

        public void BuildGMM(IList<double> samples)
        {
            Init(samples);
            count = samples.Count;
            Q = new double[count, dim];
            double[] variance = new double[dim];
            double[] averange = new double[dim];
            do
            {
                for (int i = 0; i < dim; i++)
                {
                    variance[i] = p[i].Var;
                    averange[i] = p[i].Avr;
                }
                E_Step(samples);
                M_Step(samples);
            }
            while (!Converge(variance, averange));
        }

        private bool Converge(double[] variance, double[] averange)
        {
            double result = 0;
            for (int i = 0; i < dim; i++)
            {
                if (!double.IsNaN(p[i].Var))
                    result += Math.Abs(variance[i] - p[i].Var);
                if (!double.IsNaN(p[i].Var))
                    result += Math.Pow(averange[i] - p[i].Avr, 2);
            }
            return result <= MINIMUM;
        }
        private void E_Step(IList<double> samples)
        {
            for (int i = 0; i < count; i++)
            {
                double sum = 0;
                double[] cache = new double[dim];
                for (int j = 0; j < dim; j++)
                {
                    cache[j] = p[j].Probability * GaussianDistribution(p[j], samples[i]);
                    sum += cache[j];
                }
                for (int j = 0; j < dim; j++)
                {
                    Q[i, j] = cache[j] / sum;
                }
            }
        }

        private void M_Step(IList<double> samples)
        {
            double[] k = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    k[i] += Q[j, i];
                }
                p[i].Probability = k[i] / count;
            }

            for (int i = 0; i < dim; i++)
            {
                double average = 0, variance = 0;
                for (int j = 0; j < count; j++)
                {
                    average += Q[j, i] * samples[j];
                    variance += Q[j, i] * (Math.Pow(samples[j] - p[i].Avr, 2));
                }
                p[i].Avr = average / k[i];
                p[i].Var = variance / k[i];
            }
        }

        public double Predict(double num)
        {
            double result = 0;
            foreach (var para in p)
            {
                result += para.Probability * GaussianDistribution(para, num);
            }
            return result;
        }
    }
}
