using System;
using System.Linq;

namespace AutoEncoders
{
    [Serializable]
    public class NeuralNetwork
    {
        public double[][][] Weights;
        public double[][] Biases;
        public int layerCount;
        public double learningRate;

        public NeuralNetwork()
        {   
        }

        public NeuralNetwork(int[] layerSizes)
        {
            layerCount = layerSizes.Length;
            learningRate = 0.01;

            Weights = new double[layerCount][][];
            Weights[0] = new double[0][];
            
            Biases = new double[layerCount][];
            Biases[0] = new double[0];

            for (int i = 1; i < layerCount; i++)
            {
                int currentLayerSize = layerSizes[i];
                int previousLayerSize = layerSizes[i - 1];

                Weights[i] = Matrix.CreateRandom(currentLayerSize, previousLayerSize);
                Biases[i] = Matrix.CreateRandomVector(currentLayerSize);
            }
        }

        public double[] Predict(double[] input)
        {
            double[][] activationLayers = FeedForward(input);
            
            return activationLayers.Last();
        }

        public void Train(double[] input, double[] output)
        {
            double[][] activationLayers = FeedForward(input);
            double[][] layerErrors = GetLayerErrors(output, activationLayers);
            double[][] biasGradient = layerErrors;
            double[][][] weightGradient = GetWeightGradient(activationLayers, layerErrors);

            UpdateNetwork(biasGradient, weightGradient);
        }

        private void UpdateNetwork(double[][] biasGradient, double[][][] weightGradient)
        {
            Biases = Matrix.Subtract(
                Biases,
                Matrix.Multiply(
                    biasGradient,
                    learningRate));

            Weights = Matrix.Subtract(
                Weights,
                Matrix.Multiply(
                    weightGradient,
                    learningRate));
        }

        private static double[][][] GetWeightGradient(double[][] activationLayers, double[][] layerErrors)
        {
            double[][][] weightGradient = new double[activationLayers.Length][][];
            weightGradient[0] = new double[0][];

            for (int l = 1; l < activationLayers.Length; l++)
            {
                weightGradient[l] = new double[activationLayers[l].Length][];
                for (int j = 0; j < activationLayers[l].Length; j++)
                {
                    weightGradient[l][j] = new double[activationLayers[l-1].Length];
                    for (int k = 0; k < activationLayers[l - 1].Length; k++)
                    {
                        weightGradient[l][j][k] = activationLayers[l - 1][k]*layerErrors[l][j];
                    }
                }
            }

            return weightGradient;
        }

        private double[][] GetLayerErrors(double[] output, double[][] activationLayers)
        {
            double[][] layerErros = new double[layerCount][];
            layerErros[0] = new double[0];
            layerErros[layerCount - 1] = GetLastLayerErrors(activationLayers.Last(), output);

            for (int l = layerCount - 2; l > 0; l--)
            {
                double[] factor = Matrix.Multiply(Matrix.Transpose(Weights[l + 1]), layerErros[l + 1]);
                double[] sigmaDerivative = GetSigmaDerivativeFor(activationLayers[l]);

                layerErros[l] = Matrix.Multiply(factor, sigmaDerivative);
            }

            return layerErros;
        }

        private static double[] GetLastLayerErrors(double[] lastActivationLayer, double[] output)
        {
            double[] costGradientForActivations = Matrix.Subtract(lastActivationLayer, output);
            double[] sigmaDerivative = GetSigmaDerivativeFor(lastActivationLayer);

            return Matrix.Multiply(costGradientForActivations, sigmaDerivative);
        }

        private static double[] GetSigmaDerivativeFor(double[] activationLayer)
        {
            return Matrix.Multiply(
                activationLayer,
                Matrix.Subtract(
                    Matrix.GetIdentity(activationLayer.Length),
                    activationLayer));
        }

        private double[][] FeedForward(double[] input)
        {
            double[][] activations = new double[layerCount][];
            activations[0] = input;

            for (int i = 1; i < layerCount; i++)
            {
                activations[i] = GetActivations(Weights[i], Biases[i], activations[i - 1]);
            }

            return activations;
        }

        private static double[] GetActivations(double[][] weight, double[] layerBias, double[] input)
        {
            return Matrix.Sigmoid(
                Matrix.Add(
                    Matrix.Multiply(weight, input),
                    layerBias)
                );
        }
    }
}
