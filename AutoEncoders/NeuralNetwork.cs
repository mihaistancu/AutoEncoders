using System.Collections.Generic;
using System.Linq;

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
            return null;
        }

        public void Train(double[] input, double[] output)
        {

        }
    }
}
