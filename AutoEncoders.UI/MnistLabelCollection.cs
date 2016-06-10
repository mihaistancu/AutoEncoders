using System.Collections.Generic;
using System.IO;

namespace AutoEncoders.UI
{
    public class MnistLabelCollection
    {
        private readonly List<byte> labels = new List<byte>();

        public MnistLabelCollection(string imagesFile)
        {
            using (var labelsFileReader = new BinaryReader(File.Open(imagesFile, FileMode.Open)))
            {                
                labelsFileReader.ReadInt32();
                int labelCount = ReadBigEndianInt32(labelsFileReader);              
                
                for (int i = 0; i < labelCount; i++)
                {
                    labels.Add(labelsFileReader.ReadByte());
                }
            }
        }

        public List<byte> GetLabels()
        {
            return labels;
        }

        private int ReadBigEndianInt32(BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);
            return ((bytes[3] | (bytes[2] << 8)) | (bytes[1] << 0x10)) | (bytes[0] << 0x18);
        }
    }
}
