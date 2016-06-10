using System;
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
            var network = new NeuralNetwork(new int[] {3, 4, 2});
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
            var mnistParser = new MnistImageCollection("MNIST\\train-images.idx3-ubyte");
            List<byte[]> images = mnistParser.GetImages();

            foreach (byte[] image in images)
            {
                digit.Image = BitmapFrom(image);
                digit.Refresh();
                Thread.Sleep(2000);
            }         
        }
    }
}
