using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenMarkovModel
{
    class HMM
    {
        double[,] A;
        double[,] B;
        double[] Pi;
        double[,] Alpha;
        double[,] Beta;
        private static int MAX_ITER;

        public HMM(int max_iter = 100)
        {
            MAX_ITER = max_iter;
        }

        public void UnSupervisedLearning(int[,] obvious, int kindOfState, int kindOfObservation)
        {
            Init(kindOfState,kindOfObservation);

        }

        private void Init(int kindOfState, int kindOfObservation)
        {
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
        }
    }
}
