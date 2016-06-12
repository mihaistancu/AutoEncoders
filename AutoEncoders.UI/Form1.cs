using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AutoEncoders.UI.Data;

namespace AutoEncoders.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var reader = new MnistReader();
            List<TrainingRecord> trainingSet = reader.GetTrainingSet("MNIST\\train-images.idx3-ubyte", "MNIST\\train-labels.idx1-ubyte");
            List<TrainingRecord> testSet = reader.GetTrainingSet("MNIST\\t10k-images.idx3-ubyte", "MNIST\\t10k-labels.idx1-ubyte");
            
            var network = new NeuralNetwork(new [] { 784, 30, 10 });

            for (int epoch = 0; epoch < 10; epoch++)
            {
                for (int i = 0; i < trainingSet.Count; i++)
                {
                    label1.Text = i.ToString();
                    label1.Refresh();

                    network.Train(trainingSet[i].Input, trainingSet[i].Output);
                }

                label2.Text += "Accuracy epoch" + epoch + ":" + GetAccuracy(network, testSet) + Environment.NewLine;
                label2.Refresh();
            }
        }

        private double GetAccuracy(NeuralNetwork network, List<TrainingRecord> testSet)
        {
            int success = testSet.Count(t => Match(network.Predict(t.Input), t.Output));
            return (double) success/testSet.Count;
        }

        private bool Match(double[] predicted, double[] actual)
        {
            return Array.IndexOf(predicted, predicted.Max()) ==
                   Array.IndexOf(actual, actual.Max());
        }

        private double[] ConvertToInput(byte[] image)
        {
            return image.Select(x => (double)x).ToArray();
        }

        private double[] ConvertToOutput(byte label)
        {
            return Enumerable.Range(0, 10)
                .Select(x => x == label ? 1.0 : 0.0).ToArray();
        }
    }
}
