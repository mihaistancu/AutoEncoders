using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
            List<TrainingRecord> trainingSet = GetTrainingSet("MNIST\\train-images.idx3-ubyte", "MNIST\\train-labels.idx1-ubyte");
            var network = new NeuralNetwork(new [] { 784, 30, 10 });

            for (int epoch = 0; epoch < 10; epoch++)
            {
                for (int i = 0; i < trainingSet.Count; i++)
                {
                    label1.Text = i.ToString();
                    label1.Refresh();

                    network.Train(trainingSet[i].Input, trainingSet[i].Output);
                }

                label2.Text += "Accuracy epoch" + epoch + ":" + GetAccuracy(network) + Environment.NewLine;
                label2.Refresh();
            }
        }

        private List<TrainingRecord> GetTrainingSet(string inputFile, string outputFile)
        {
            var mnistImageParser = new MnistImageCollection(inputFile);
            List<byte[]> images = mnistImageParser.GetImages();

            var mnistLabelParser = new MnistLabelCollection(outputFile);
            List<byte> labels = mnistLabelParser.GetLabels();

            List<TrainingRecord> trainingSet = new List<TrainingRecord>();
            for (int i = 0; i < images.Count; i++)
            {
                trainingSet.Add(new TrainingRecord
                {
                    Input = ConvertToInput(images[i]),
                    Output = ConvertToOutput(labels[i])
                });
            }
            return trainingSet;
        }

        private double GetAccuracy(NeuralNetwork network)
        {
            List<TrainingRecord> testSet = GetTrainingSet("MNIST\\t10k-images.idx3-ubyte", "MNIST\\t10k-labels.idx1-ubyte");
            int success = 0;

            for (int i = 0; i < testSet.Count; i++)
            {
                double[] output = network.Predict(testSet[i].Input);
                int predictedDigit = Array.IndexOf(output, output.Max());
                int actualDigit = Array.IndexOf(testSet[i].Output, testSet[i].Output.Max());

                if (predictedDigit == actualDigit)
                {
                    success++;
                }
            }

            return (double) success/testSet.Count;
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

        public class TrainingRecord
        {
            public double[] Input;
            public double[] Output;
        }
    }
}
