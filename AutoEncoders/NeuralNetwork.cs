using System.Collections.Generic;
using System.Linq;

namespace AutoEncoders
{
    public class NeuralNetwork
    {
        private readonly List<double[][]> weightMatrices = new List<double[][]>();
        private readonly List<double[]> layerBiases = new List<double[]>();
        private readonly int layerCount;

        public NeuralNetwork(int[] layerSizes)
        {
            layerCount = layerSizes.Length;

            weightMatrices.Add(new double[0][]);
            layerBiases.Add(new double[0]);

            for (int i = 1; i < layerCount; i++)
            {
                int currentLayerSize = layerSizes[i];
                int previousLayerSize = layerSizes[i - 1];

                weightMatrices.Add(Matrix.CreateRandom(currentLayerSize, previousLayerSize));
                layerBiases.Add(Matrix.CreateRandomVector(currentLayerSize));
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
            
            double[][][] weightGradient = GetWeightGradient(activationLayers, layerErrors);
        }

        private static double[][][] GetWeightGradient(double[][] activationLayers, double[][] layerErrors)
        {
            double[][][] weightGradient = new double[activationLayers.Length][][];

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
            layerErros[layerCount - 1] = GetLastLayerErrors(activationLayers.Last(), output);

            for (int l = layerCount - 2; l > 0; l--)
            {
                double[] factor = Matrix.Multiply(Matrix.Transpose(weightMatrices[l + 1]), layerErros[l + 1]);
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
                activations[i] = GetActivations(weightMatrices[i], layerBiases[i], activations[i - 1]);
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
