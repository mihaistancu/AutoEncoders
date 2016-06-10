using System.Collections.Generic;

namespace AutoEncoders
{
    public class NeuralNetwork
    {
        private List<double[][]> weightMatrices = new List<double[][]>();                   

        public NeuralNetwork(int[] layerSizes)
        {
            for (int i = 1; i < layerSizes.Length; i++)
            {
                int currentLayerSize = layerSizes[i];
                int previousLayerSize = layerSizes[i - 1];

                weightMatrices.Add(Matrix.CreateRandom(currentLayerSize, previousLayerSize));
            }
        }

        public double[] FeedForward(double[] input)
        {
            return null;
        }

        public void Train(double[] input, double[] output)
        {

        }
    }
}
