using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AutoEncoders
{
    public class NeuralNetwork
    {
        List<double[][]> weightMatrices = new List<double[][]>();                   

        public NeuralNetwork(int[] layerSizes)
        {
            for (int i = 1; i < layerSizes.Length; i++)
            {
                int currentLayerSize = layerSizes[i];
                int previousLayerSize = layerSizes[i - 1];

                weightMatrices.Add(GetWeightMatrix(currentLayerSize, previousLayerSize));
            }
        }

        public double[] FeedForward(double[] input)
        {
            return null;
        }

        public void Train(double[] input, double[] output)
        {

        }

        private static double[][] GetWeightMatrix(int rows, int columns)
        {
            double[][] weightMatrix = new double[rows][];
            
            for (int i = 0; i < rows; i++)
            {
                weightMatrix[i] = GetArrayWithRandomWeights(columns);
            }

            return weightMatrix;
        }

        private static double[] GetArrayWithRandomWeights(int arraySize)
        {                        
            double[] randomWeightsArray = new double[arraySize];

            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < arraySize; i++)
            {
                randomWeightsArray[i] = random.NextDouble();
            }

            return randomWeightsArray;
        }
    }
}
