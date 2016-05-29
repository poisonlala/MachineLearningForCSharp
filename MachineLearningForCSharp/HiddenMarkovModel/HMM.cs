using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenMarkovModel
{
    public sealed class HMM
    {
        double[,] A;
        double[,] B;
        double[] Pi;
        double[,] Alpha;
        double[,] Beta;
        private int M;
        private int T;
        private int KindOfState;
        private int KindOfObservation;
        private static int MAX_ITER;

        public HMM(int max_iter = 100)
        {
            MAX_ITER = max_iter;
        }

        public void UnSupervisedLearning(int[,] obvious, int kindOfState, int kindOfObservation)
        {
            Init(obvious,kindOfState,kindOfObservation);
            for (int i = 0; i < M; i++)
            {
                int t = 0;
                while (t < MAX_ITER)
                {
                    
                }
            }
        }

        private void Init(int[,] obvious,int kindOfState, int kindOfObservation)
        {
            KindOfObservation = kindOfObservation;
            KindOfState = kindOfState;
            M = obvious.GetLength(0);
            T = obvious.GetLength(1);
            A = new double[kindOfState, kindOfState];
            B = new double[kindOfState, kindOfObservation];
            Pi = new double[kindOfState];
            for (int i = 0; i < kindOfState; i++)
            {
                for (int j = 0; j < kindOfState; j++)
                {
                    A[i, j] = (double) 1/kindOfState;
                }

                for (int j = 0; j < kindOfObservation; j++)
                {
                    B[i, j] = (double) 1/kindOfObservation;
                }
                Pi[i] = (double)1 /kindOfState;
            }
            Alpha = new double[T,kindOfState];
            Beta = new double[T,kindOfState];
        }

        private void UnSupervisedLearningCore()
        {
            
        }

        private void UnSupervisedLearning_Alpha(int[,] obvious,int row)
        {
            for (int i = 0; i < KindOfState; i++)
            {
                Alpha[0, i] = Pi[i]*B[i, obvious[row, 0]];
            }
            for (int i = 1; i < T; i++)
            {
                for (int j = 0; j < KindOfState; j++)
                {
                    for (int k = 0; k < KindOfState; k++)
                    {
                        Alpha[i, j] += Alpha[i - 1, k] * A[k, j];
                    }
                    Alpha[i, j] *= B[j, obvious[row, i]];
                }
            }
        }

        private void UnSupervisedLearning_Beta(int[,] obvious, int row)
        {
            for (int i = 0; i < KindOfState; i++)
            {
                Beta[T - 1, i] = 1;
            }
            for (int i = T - 2; i >= 0; i--)
            {
                for (int j = 0; j < KindOfState; j++)
                {
                    for (int k = 0; k < KindOfState; k++)
                    {
                        Beta[i, j] += A[j, k]*B[k, obvious[row, i + 1]]*Beta[i + 1, k];
                    }
                }
            }
        }
    }
}
