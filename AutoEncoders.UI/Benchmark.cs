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
            return Array.IndexOf(predicted, predicted.Max()) ==
                   Array.IndexOf(actual, actual.Max());
        }
    }
}
