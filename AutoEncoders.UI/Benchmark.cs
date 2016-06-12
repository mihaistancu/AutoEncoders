using System;
using System.Collections.Generic;
using System.Linq;
using AutoEncoders.UI.Data;

namespace AutoEncoders.UI
{
    public class Benchmark
    {
        public double GetAccuracy(NeuralNetwork network, List<TrainingRecord> testSet)
        {
            int success = testSet.Count(t => Match(network.Predict(t.Input), t.Output));
            return (double)success / testSet.Count;
        }

        private bool Match(double[] predicted, double[] actual)
        {
            return MaxIndex(predicted) == MaxIndex(actual);
        }

        private int MaxIndex(double[] vector)
        {
            return Array.IndexOf(vector, vector.Max());
        }
    }
}
