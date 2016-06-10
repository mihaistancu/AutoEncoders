using System.Collections.Generic;
using System.Linq;

namespace AutoEncoders
{
    public class NeuralNetwork
    {
        private List<double[][]> weightMatrices = new List<double[][]>();
        private List<double[]> layerBiases = new List<double[]>();
        private int layerCount ;

        public NeuralNetwork(int[] layerSizes)
        {
            layerCount = layerSizes.Length;

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
        }

        private double[][] FeedForward(double[] input)
        {
            double[][] activations = new double[layerCount][];
            activations[0] = input;

            for (int i = 1; i < layerCount; i++)
            {
                activations[i] = GetActivations(weightMatrices[i - 1], layerBiases[i - 1], activations[i - 1]);
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
