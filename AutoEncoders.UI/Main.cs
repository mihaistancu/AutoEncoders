using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AutoEncoders.UI.Data;

namespace AutoEncoders.UI
{
    public partial class Main : Form
    {
        private NeuralNetwork network;
        private readonly List<TrainingRecord> trainingSet;
        private readonly List<TrainingRecord> testSet;
        private readonly Benchmark benchmark;

        public Main()
        {
            InitializeComponent();

            var reader = new MnistReader();
            trainingSet = reader.GetTrainingSet("MNIST\\train-images.idx3-ubyte", "MNIST\\train-labels.idx1-ubyte");
            testSet = reader.GetTrainingSet("MNIST\\t10k-images.idx3-ubyte", "MNIST\\t10k-labels.idx1-ubyte");
            
            network = new NeuralNetwork(new[] { 784, 30, 10 });
            benchmark = new Benchmark();
        }

        private void OnStartTraining(object sender, EventArgs e)
        {
            for (double accuracy = 0; accuracy < .9;)
            {
                accuracy = benchmark.GetAccuracy(network, testSet);
                UpdateAccuracy(accuracy);

                Train();
            }
        }

        private void Train()
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
            Accuracy.Text += Environment.NewLine + accuracy;
            Accuracy.Refresh();
        }

        private void OnOpenNetwork(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void OnOpenFileDialogFileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var serializer = new NeuralNetworkSerializer();
            network = serializer.Load(openFileDialog.FileName);
        }

        private void OnTestAccuracy(object sender, EventArgs e)
        {
            double accuracy = benchmark.GetAccuracy(network, testSet);
            UpdateAccuracy(accuracy);
        }

        private void OnSaveNetwork(object sender, EventArgs e)
        {
            double accuracy = benchmark.GetAccuracy(network, testSet);
            
            var serializer = new NeuralNetworkSerializer();
            serializer.Save(network, accuracy.ToString());
        }
    }
}
