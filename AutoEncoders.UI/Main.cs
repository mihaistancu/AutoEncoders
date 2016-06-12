using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoEncoders.UI.Data;

namespace AutoEncoders.UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();            
        }

        private void OnStartTraining(object sender, EventArgs e)
        {
            var reader = new MnistReader();
            List<TrainingRecord> trainingSet = reader.GetTrainingSet("MNIST\\train-images.idx3-ubyte", "MNIST\\train-labels.idx1-ubyte");
            List<TrainingRecord> testSet = reader.GetTrainingSet("MNIST\\t10k-images.idx3-ubyte", "MNIST\\t10k-labels.idx1-ubyte");
            
            var network = new NeuralNetwork(new [] { 784, 30, 10 });
            var benchmark = new Benchmark();

            for (double accuracy = 0; accuracy < .9;)
            {
                accuracy = benchmark.GetAccuracy(network, testSet);
                UpdateAccuracy(accuracy);

                Train(network, trainingSet);
            }
        }

        private void Train(NeuralNetwork network, List<TrainingRecord> trainingSet)
        {
            for (int i = 0; i < trainingSet.Count; i++)
            {
                UpdateProgress(i);
                network.Train(trainingSet[i].Input, trainingSet[i].Output);
            }
        }

        private void UpdateProgress(int progress)
        {
            Progress.Text = progress.ToString();
            Progress.Refresh();
        }

        private void UpdateAccuracy(double accuracy)
        {
            Accuracy.Text += "Accuracy: " + accuracy;
            Accuracy.Refresh();
        }
    }
}
