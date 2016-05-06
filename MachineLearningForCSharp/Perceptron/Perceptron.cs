using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    public sealed class Perceptron
    {
        private double[] _w;
        private double _b;
        private double _r;
        private readonly int MAX = 2000;
        public double[] W { get { return _w; } }
        public double B { get { return _b; } }

        public Perceptron(double learningRate)
        {
            _r = learningRate;
        }
        private void Init(int length)
        {
            _w = new double[length];
            _b = 0;
        }
        /// <summary>
        /// 分类函数
        /// </summary>
        /// <param name="training"></param>
        /// <param name="answer"></param>
        /// <returns></returns>
        public bool Classify(double[,] training, bool[] answer)
        {
            int size = training.GetLength(0);
            int dimension = training.GetLength(1);
            if (size != answer.Length)
                return false;
            Init(dimension);

            for (int c = 0; c < MAX; c++)
            {
                bool done = true;
                for (int i = 0; i < size; i++)
                {
                    bool y = Sign(CalculateLoss(training, i));
                    if (y != answer[i])
                    {
                        done = false;
                        UpdateWB(training, i,answer[i]);
                    }
                }
                if (done) return true;
            }
            return false;
        }

        /// <summary>
        /// sign函数
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private bool Sign(double d)
        {
            return d == 0 || d > 0;
        }

        /// <summary>
        /// 损失函数
        /// </summary>
        /// <param name="coor"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private double CalculateLoss(double[,] coor, int line)
        {
            return VectorInnerMult(coor, line, _w) + _b;
        }


        /// <summary>
        /// 梯度随机下降修改权值和偏置
        /// </summary>
        /// <param name="coor"></param>
        /// <param name="line"></param>
        /// <param name="answer"></param>
        private void UpdateWB(double[,] coor, int line, bool answer)
        {
            for (int i = 0; i < coor.GetLength(1); i++)
                _w[i] += _r * coor[line, i] * (answer == true ? 1 : -1);
            _b += _r * (answer == true ? 1 : -1);
        }

        /// <summary>
        /// 向量内积
        /// </summary>
        /// <param name="coor"></param> 
        /// <param name="line"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private double VectorInnerMult(double[,] coor, int line, double[] b)
        {
            double result = 0;
            for (int i = 0; i < coor.GetLength(1); i++)
                result += coor[line, i] * b[i];
            return result;
        }
    }
}
