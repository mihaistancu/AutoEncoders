using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
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
            var mnistImageParser = new MnistImageCollection("MNIST\\train-images.idx3-ubyte");
            List<byte[]> images = mnistImageParser.GetImages();

            var mnistLabelParser = new MnistLabelCollection("MNIST\\train-labels.idx1-ubyte");
            List<byte> labels = mnistLabelParser.GetLabels();

            var network = new NeuralNetwork(new int[] { 784, 30, 10 });

            for (int epoch = 0; epoch < 10; epoch++)
            {
                for (int i = 0; i < images.Count; i++)
                {
                    //digit.Image = BitmapFrom(images[i]);
                    //digit.Refresh();

                    label1.Text = ((int) labels[i]).ToString() + "(" + i + ")";
                    label1.Refresh();

                    network.Train(ConvertToInput(images[i]), ConvertToOutput(labels[i]));
                }

                label2.Text += "Accuracy epoch" + epoch + ":" + GetAccuracy(network) + Environment.NewLine;
                label2.Refresh();
            }
        }

        private double GetAccuracy(NeuralNetwork network)
        {
            var mnistImageParser = new MnistImageCollection("MNIST\\t10k-images.idx3-ubyte");
            List<byte[]> images = mnistImageParser.GetImages();

            var mnistLabelParser = new MnistLabelCollection("MNIST\\t10k-labels.idx1-ubyte");
            List<byte> labels = mnistLabelParser.GetLabels();

            int success = 0;

            for (int i = 0; i < images.Count; i++)
            {
                double[] output = network.Predict(ConvertToInput(images[i]));
                int predictedDigit = Array.IndexOf(output, output.Max());
                int actualDigit = labels[i];

                if (predictedDigit == actualDigit)
                {
                    success++;
                }
            }

            return (double) success/images.Count;
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
