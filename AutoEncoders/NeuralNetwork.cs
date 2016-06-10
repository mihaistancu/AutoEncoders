using System.Collections.Generic;

namespace AutoEncoders
{
    public class NeuralNetwork
    {
        private List<double[][]> weightMatrices = new List<double[][]>();
        private List<double[]> layerBiases = new List<double[]>();

        public NeuralNetwork(int[] layerSizes)
        {
            for (int i = 1; i < layerSizes.Length; i++)
            {
                int currentLayerSize = layerSizes[i];
                int previousLayerSize = layerSizes[i - 1];

                weightMatrices.Add(Matrix.CreateRandom(currentLayerSize, previousLayerSize));
                layerBiases.Add(Matrix.CreateRandomVector(currentLayerSize));
            }
        }

        public double[] FeedForward(double[] input)
        {    
            double[] activations = input;

            for (int i = 0; i < weightMatrices.Count; i++)
            {
                activations = GetActivations(weightMatrices[i], layerBiases[i], activations);
            }

            return activations;
        }

        public void Train(double[] input, double[] output)
        {
            
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
