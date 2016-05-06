using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpectationMaximization
{
    class GMM
    {
        private static double CONSTANT = 1 / (Math.Sqrt(2 * Math.PI));
        private double[,] Q;
        private int dim;
        private int count;
        private static double MINIMUM = 0.00001;
        private GaussianParameter[] p;
        public GaussianParameter[] GaussianPara { get { return p; } }

        public GMM(int n)
        {
            dim = n;
            p = new GaussianParameter[n];
            for (int i = 0; i < n; i++)
                p[i] = new GaussianParameter((double)1 / n);
        }
        private double GaussianDistribution(GaussianParameter p, double x)
        {
            var result = (CONSTANT / Math.Sqrt(p.Var)) * Math.Pow(Math.E, (-Math.Pow(x - p.Avr, 2) / (2 * p.Var)));
            return result;
        }

        public void BuildGMM(IList<double> samples)
        {
            count = samples.Count;
            Q = new double[count,dim];
            int i = 100;
            while (i>0)
            {
                E_Step(samples);
                M_Step(samples);
            }
        }
        private void E_Step(IList<double> samples)
        {
            for(int i = 0; i < count; i++)
            {
                double sum = 0;
                double[] cache = new double[dim];
                for(int j = 0; j < dim; j++)
                {
                    cache[j] = p[j].Probability * GaussianDistribution(p[j], samples[i]);
                    sum += cache[j];
                }
                for(int j = 0; j < dim; j++)
                {
                    Q[i, j] = cache[j] / sum;
                }
            }
        }

        private void M_Step(IList<double> samples)
        {
            double[] k = new double[dim];
            for(int i = 0; i < dim; i++)
            {
                for(int j = 0; j < count; j++)
                {
                    k[i] += Q[j, i];
                }
                p[i].Probability = k[i] / count;
            }

            for(int i = 0; i < dim; i++)
            {
                double average = 0, variance = 0;
                for(int j = 0; j < count; j++)
                {
                    average += Q[j, i] * samples[j];
                }
                p[i].Avr = average / k[i];

                for(int j = 0; j < count; j++)
                {
                    variance += Q[j,i]*(Math.Pow(samples[i]-p[i].Avr,2));
                }
                p[i].Var = variance / k[i];
            }
        } 
    }
}
