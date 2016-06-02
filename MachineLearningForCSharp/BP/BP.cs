using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using BP.ActivationFunction;

namespace BP
{
    public sealed class BP
    {
        private ActivationFunctionEnum Activation;
        private double RATE;
        private double[][] X;//hidden layers
        private double[][] Y;//hidden layers
        private double[] XOUT;//output layers
        private double[] YOUT;//output layers
        private double[][] E;//error of hidden layers and output layer
        private double[][][] W;//weight
        private double[][] Theta;//hidden layers and output layer
        private int Levels;
        private double ERROR;
        private int ITER;
        private double[,] Input;
        private int[,] Answer;
        private double CurrentE;
        private ActivationFunction.ActivationFunction Function;

        public BP(double[,] input, int[,] label, int[] numberOfProsInEachLayer, double error = 0.001, int iter = 1000000, double learningrate = 0.9, ActivationFunctionEnum kindOfActivationFunction = ActivationFunctionEnum.Sigmoid)
        {
            Activation = kindOfActivationFunction;
            RATE = learningrate;
            Levels = numberOfProsInEachLayer.Length;
            ERROR = error;
            ITER = iter;
            Input = input;
            Answer = label;
            X = new double[Levels][];
            Y = new double[Levels][];
            E = new double[Levels + 1][];
            W = new double[Levels + 1][][];
            Theta = new double[Levels + 1][];
            XOUT = new double[label.GetLength(1)];
            YOUT = new double[label.GetLength(1)];
            Init(input.GetLength(1), label.GetLength(1), numberOfProsInEachLayer);
        }

        private void Init(int inputDim, int outputDim, int[] numberOfProsInEachLayer)
        {
            Random r = new Random();
            //X,Y
            for (int i = 0; i < Levels; i++)
            {
                X[i] = new double[numberOfProsInEachLayer[i]];
                Y[i] = new double[numberOfProsInEachLayer[i]];
            }
            //W
            W[0] = new double[inputDim][];
            for (int j = 0; j < inputDim; j++)
            {
                W[0][j] = new double[X[0].Length];
                for (int k = 0; k < X[0].Length; k++)
                {
                    W[0][j][k] = r.NextDouble() / 10 - 0.05;
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
                        W[i][j][k] = r.NextDouble() / 10 - 0.05;
                    }
                }
            }
            W[Levels] = new double[X[Levels - 1].Length][];
            for (int j = 0; j < X[Levels - 1].Length; j++)
            {
                W[Levels][j] = new double[outputDim];
                for (int k = 0; k < outputDim; k++)
                {
                    W[Levels][j][k] = r.NextDouble() / 10 - 0.05;
                }
            }
            //Theta
            for (int i = 0; i < Levels; i++)
            {
                Theta[i] = new double[X[i].Length];
                for (int j = 0; j < X[i].Length; j++)
                    Theta[i][j] = r.NextDouble() / 10 - 0.05;
            }
            Theta[Levels] = new double[outputDim];
            for (int j = 0; j < outputDim; j++)
            {
                Theta[Levels][j] = r.NextDouble() / 10 - 0.05;
            }
            //E
            for (int i = 0; i < Levels; i++)
            {
                E[i] = new double[X[i].Length];
            }
            E[Levels] = new double[outputDim];
            //Function
            switch (Activation)
            {
                case ActivationFunctionEnum.Sigmoid:
                    Function = new Sigmoid();
                    break;
                case ActivationFunctionEnum.Tanh:
                    Function = new Tanh();
                    break;
                case ActivationFunctionEnum.ReLU:
                    Function = new ReLU();
                    break;
                default:
                    Function = new Sigmoid();
                    break;
            }
        }

        public void Train()
        {
            int iter = 0;
            do
            {
                CurrentE = 0;
                for (int i = 0; i < Input.GetLength(0); i++)
                {
                    InitalizeX();
                    InitalizeE();
                    ForwardLearning(i);
                    BackwardLearning(i);
                    BackwardUpdate(i);
                    CurrentE /= 2;
                }
                iter++;
            } while (iter < ITER && CurrentE > ERROR);
        }

        /**
        i-1    >   i
        0 o       0 o
        1 o
        2 o       1 o
        ...       ...
        k o       j o
        **/
        private void ForwardLearning(int sample)
        {
            //input layer -> hidden layer0
            for (int j = 0; j < X[0].Length; j++)
            {
                for (int k = 0; k < Input.GetLength(1); k++)
                {
                    X[0][j] += W[0][k][j] * Input[sample, k];
                }
                X[0][j] += Theta[0][j];
                Y[0][j] = Function.CalculateY(X[0][j]);
            }
            //hidden layer 1 -> hiddenlayer n
            for (int i = 1; i < Levels; i++)
            {
                for (int j = 0; j < X[i].Length; j++)
                {
                    for (int k = 0; k < Y[i - 1].Length; k++)
                    {
                        X[i][j] += W[i][k][j] * Y[i - 1][k];
                    }
                    X[i][j] += Theta[i][j];
                    Y[i][j] = Function.CalculateY(X[i][j]);
                }
            }
            //hidden layer n -> output layer
            for (int j = 0; j < Answer.GetLength(1); j++)
            {
                for (int k = 0; k < Y[Levels - 1].Length; k++)
                {
                    XOUT[j] += W[Levels][k][j] * Y[Levels - 1][k];
                }
                XOUT[j] += Theta[Levels][j];
                YOUT[j] = Function.CalculateY(XOUT[j]);
            }
        }

        /**
        i-1   <    i
        0 o       0 o
        1 o
        2 o       1 o
        ...       ...
        k o       j o
        **/
        private void BackwardLearning(int sample)
        {
            //output layer  <-  actual
            for (int j = 0; j < Answer.GetLength(1); j++)
            {
                double error = Answer[sample, j] - YOUT[j];
                E[Levels][j] = error * Function.CalculateDeltaY(XOUT[j]);
                CurrentE += error * error;
            }
            //hiddenlayer n <- output layer
            for (int k = 0; k < X[Levels - 1].Length; k++)
            {
                for (int j = 0; j < XOUT.Length; j++)
                {
                    E[Levels - 1][k] += W[Levels][k][j] * E[Levels][j];
                }
                E[Levels - 1][k] *= Function.CalculateDeltaY(X[Levels - 1][k]);
            }
            //hidden layer i-1 <- hidden layer i
            for (int i = Levels - 1; i > 0; i--)
            {
                for (int k = 0; k < X[i - 1].Length; k++)
                {
                    for (int j = 0; j < X[i].Length; j++)
                    {
                        E[i - 1][k] += W[i][k][j] * E[i][j];
                    }
                    E[i - 1][k] *= Function.CalculateDeltaY(X[i - 1][k]);
                }
            }
        }

        /**
        i-1   <    i
        0 o       0 o
        1 o
        2 o       1 o
        ...       ...
        k o       j o
        **/
        private void BackwardUpdate(int sample)
        {
            //update W to output layer
            for (int j = YOUT.Length - 1; j >= 0; j--)
            {
                for (int k = X[Levels - 1].Length - 1; k >= 0; k--)
                {
                    W[Levels][k][j] += RATE * E[Levels][j] * Y[Levels - 1][k];
                }
                Theta[Levels][j] += RATE * E[Levels][j];
            }

            //update W link hidden layer i-1 with hidden layer i , 0 < i < Levels
            for (int i = Levels - 1; i > 0; i--)
            {
                for (int j = X[i].Length - 1; j >= 0; j--)
                {
                    for (int k = X[i - 1].Length - 1; k >= 0; k--)
                    {
                        W[i][k][j] += RATE * E[i][j] * Y[i - 1][k];
                    }
                    Theta[i][j] += RATE * E[i][j];
                }
            }

            //update W link input layer with hidden layer 0
            for (int j = X[0].Length - 1; j >= 0; j--)
            {
                for (int k = Input.GetLength(1) - 1; k >= 0; k--)
                {
                    W[0][k][j] += RATE * E[0][j] * Input[sample, k];
                }
                Theta[0][j] += RATE * E[0][j];
            }
        }

        public double[] Predict(double[] x)
        {
            InitalizeX();
            //input layer -> hidden layer0
            for (int j = 0; j < X[0].Length; j++)
            {
                for (int k = 0; k < Input.GetLength(1); k++)
                {
                    X[0][j] += W[0][k][j] * x[k];
                }
                X[0][j] += Theta[0][j];
                Y[0][j] = Function.CalculateY(X[0][j]);
            }
            //hidden layer 1 -> hiddenlayer n
            for (int i = 1; i < Levels; i++)
            {
                for (int j = 0; j < X[i].Length; j++)
                {
                    for (int k = 0; k < Y[i - 1].Length; k++)
                    {
                        X[i][j] += W[i][k][j] * Y[i - 1][k];
                    }
                    X[i][j] += Theta[i][j];
                    Y[i][j] = Function.CalculateY(X[i][j]);
                }
            }
            //hidden layer n -> output layer
            for (int j = 0; j < Answer.GetLength(1); j++)
            {
                for (int k = 0; k < Y[Levels - 1].Length; k++)
                {
                    XOUT[j] += W[Levels][k][j] * Y[Levels - 1][k];
                }
                XOUT[j] += Theta[Levels][j];
                YOUT[j] = Function.CalculateY(XOUT[j]);
            }
            double[] result = new double[YOUT.Length];
            Array.Copy(YOUT, result, YOUT.Length);
            return result;
        }

        private void InitalizeX()
        {
            for (int i = 0; i < X.Length; i++)
            {
                for (int j = 0; j < X[i].Length; j++)
                {
                    X[i][j] = 0;
                }
            }
            for (int i = 0; i < XOUT.Length; i++)
            {
                XOUT[i] = 0;
            }
        }

        private void InitalizeE()
        {
            for (int i = 0; i < E.Length; i++)
            {
                for (int j = 0; j < E[i].Length; j++)
                {
                    E[i][j] = 0;
                }
            }
        }

    }
}
