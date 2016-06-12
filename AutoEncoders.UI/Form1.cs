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
                    Update(label1, i.ToString());

                    network.Train(trainingSet[i].Input, trainingSet[i].Output);
                }

                Update(label2, "Accuracy epoch" + epoch + ":" + GetAccuracy(network, testSet) + Environment.NewLine);
            }
        }

        private void Update(Label label, string text)
        {
            label.Text = text;
            label.Refresh();
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
    }
}
