using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BP.ActivationFunction;

namespace BP
{
    public sealed class BP
    {
        private ActivationFunctionEnum Activation;
        private double RATE;
        private double[][] X;//hidden layers
        private double[] Y;//output layer
        private double[][] E;//hidden layers and output layer
        private double[][][] W;//all
        private double[][] Theta;//hidden layers and output layer
        private int Levels;
        private double ERROR;
        private int ITER;
        private double[,] Input;
        private int[,] Answer;
        private double CurrentE;
        private ActivationFunction.ActivationFunction Function;

        public BP(double[,] input, int[,] label, int[] numberOfProsInEachLayer, double error = 0.01, int iter = 1000000, double learningrate = 0.01, ActivationFunctionEnum kindOfActivationFunction = ActivationFunctionEnum.Sigmoid)
        {
            Activation = kindOfActivationFunction;
            RATE = learningrate;
            Levels = numberOfProsInEachLayer.Length;
            ERROR = error;
            ITER = iter;
            Input = input;
            Answer = label;
            X = new double[Levels][];
            E = new double[Levels + 1][];
            W = new double[Levels + 1][][];
            Theta = new double[Levels + 1][];
            Y = new double[label.GetLength(1)];
            Init(input.GetLength(1), label.GetLength(1), numberOfProsInEachLayer);
        }

        private void Init(int inputDim, int outputDim, int[] numberOfProsInEachLayer)
        {
            Random r = new Random();
            //X
            for (int i = 0; i < Levels; i++)
            {
                X[i] = new double[numberOfProsInEachLayer[i]];
            }
            //W
            W[0] = new double[inputDim][];
            for (int j = 0; j < inputDim; j++)
            {
                W[0][j] = new double[X[0].Length];
                for (int k = 0; k < X[0].Length; k++)
                {
                    W[0][j][k] = r.NextDouble();
                }
            }
            for (int i = 1; i < Levels; i++)
            {
                W[i] = new double[X[i - 1].Length][];
                for (int j = 0; j < X[i - 1].Length; j++)
                {
                    W[i][j] = new double[X[i].Length];
                    for (int k = 0; k < X[i].Length; k++)
                    {
                        W[i][j][k] = r.NextDouble();
                    }
                }
            }
            W[Levels] = new double[X[Levels - 1].Length][];
            for (int j = 0; j < X[Levels - 1].Length; j++)
            {
                W[Levels][j] = new double[outputDim];
                for (int k = 0; k < outputDim; k++)
                {
                    W[Levels][j][k] = r.NextDouble();
                }
            }
            //Theta
            for (int i = 0; i < Levels; i++)
            {
                Theta[i] = new double[X[i].Length];
                for (int j = 0; j < X[i].Length; j++)
                    Theta[i][j] = r.NextDouble();
            }
            Theta[Levels] = new double[outputDim];
            for (int j = 0; j < outputDim; j++)
            {
                Theta[Levels][j] = r.NextDouble();
            }
            //E
            for (int i = 0; i < Levels; i++)
            {
                E[i] = new double[X[i].Length];
                for (int j = 0; j < X[i].Length; j++)
                {
                    E[i][j] = ERROR;
                    CurrentE += E[i][j];
                }
            }
            E[Levels] = new double[outputDim];
            for (int j = 0; j < outputDim; j++)
            {
                E[Levels][j] = ERROR;
                CurrentE += E[Levels][j];
            }
            //Function
            switch (Activation)
            {
                case ActivationFunctionEnum.Sigmoid:
                    Function = new Sigmoid();
                    break;
                case ActivationFunctionEnum.Linear:
                    Function = null;
                    break;
                default:
                    Function = new Sigmoid();
                    break;
            }
        }

        public void Train()
        {
            int iter = 0;
            while (iter < ITER && CurrentE > ERROR)
            {
                for (int i = 0; i < Input.GetLength(0); i++)
                {
                    CalculateX(i);
                }
                iter++;
            }
        }


        /**
        i-1  --->  i
        0 o       0 o
        1 o
        2 o       1 o
        ...       ...
        k o       j o
        **/
        private void CalculateX(int sample)
        {
            //input layer -> hidden layer0
            for (int j = 0; j < X[0].Length; j++)
            {
                for (int k = 0; k < Input.GetLength(1); k++)
                {
                    X[0][j] += W[0][k][j] *Input[sample, k];
                }
                X[0][j] -= Theta[0][j];
                X[0][j] = Function.CalculateY(X[0][j]);
            }
            //hidden layer 1 -> hiddenlayer n
            for (int i = 1; i < Levels; i++)
            {
                for (int j = 0; j < X[i].Length; j++)
                {
                    for (int k = 0; k < X[i - 1].Length; k++)
                    {
                        X[i][j] += W[i][k][j]*X[i - 1][k];
                    }
                    X[i][j] -= Theta[i][j];
                    X[i][j] = Function.CalculateY(X[i][j]);
                }
            }
            //hidden layer n -> output layer
            for (int j = 0; j < Answer.GetLength(1); j++)
            {
                for (int k = 0; k < X[Levels - 1].Length; k++)
                {
                    Y[j] += W[Levels][k][j]*X[Levels - 1][k];
                }
                Y[j] -= Theta[Levels][j];
                Y[j] = Function.CalculateY(Y[j]);
            }
        }
    }
}
