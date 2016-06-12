using System.Collections.Generic;
using System.Linq;

namespace AutoEncoders.UI.Data
{
    public class MnistReader
    {
        public List<TrainingRecord> GetTrainingSet(string inputFile, string outputFile)
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

    public class TrainingRecord
    {
        public double[] Input;
        public double[] Output;
    }
}
