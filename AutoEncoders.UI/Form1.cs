﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace AutoEncoders.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            

            double[][] testMatrix = 
            {
                new double[] {1, 2, 3},
                new double[] {4, 5, 6},
                new double[] {0, 2, 3},
                new double[] {8, 9, 0}
            };

            double[] testVector = {2, 4, 7};
            double[] testVector2 = { 1, 2 };

            var network = new NeuralNetwork(new int[] { 3, 4, 5, 2 });
            //double[] bla = network.Predict(testVector);
            network.Train(testVector, testVector2);
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

        private void button1_Click(object sender, EventArgs e)
        {
            var mnistImageParser = new MnistImageCollection("MNIST\\train-images.idx3-ubyte");
            List<byte[]> images = mnistImageParser.GetImages();

            var mnistLabelParser = new MnistLabelCollection("MNIST\\train-labels.idx1-ubyte");
            List<byte> labels = mnistLabelParser.GetLabels();

            for (int i = 0; i < images.Count; i++)
            {
                digit.Image = BitmapFrom(images[i]);
                digit.Refresh();

                label1.Text = ((int) labels[i]).ToString();
                label1.Refresh();
                Thread.Sleep(2000);
            }            
        }
    }
}
