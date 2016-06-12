using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            openNetworkDialog.ShowDialog();
        }

        private void OnOpenNetworkDialogFileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var serializer = new NeuralNetworkSerializer();
            network = serializer.Load(openNetworkDialog.FileName);
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

        private void OnOpenImage(object sender, EventArgs e)
        {
            openImageDialog.ShowDialog();
        }

        private void OnOpenImageDialogFileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var image = Image.FromFile(openImageDialog.FileName);
            byte[] bytes = ImageToBytes(image);
            Bitmap bitmap = BitmapFrom(bytes);
            Digit.Image = bitmap;

            var input = bytes.Select(x => (double) x).ToArray();
            double[] output = network.Predict(input);
            Prediction.Text = output.MaxIndex().ToString();
        }

        private byte[] ImageToBytes(Image image)
        {
            var bitmap = new Bitmap(image);
            
            var result = new byte[784];

            for (int i=0;i<28;i++)
                for (int j = 0; j < 28; j++)
                {
                    result[j*28 + i] = bitmap.GetPixel(i, j).G;
                }

            return result;
        }

        private Bitmap BitmapFrom(byte[] bytes)
        {
            var bitmap = new Bitmap(28, 28);
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    byte colorSample = bytes[j * 28 + i];
                    bitmap.SetPixel(i, j, Color.FromArgb(255, colorSample, colorSample, colorSample));
                }
            }

            return bitmap;
        }
    }
}
