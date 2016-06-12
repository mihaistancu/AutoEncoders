using System.Collections.Generic;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace AutoEncoders.UI
{
    public class MnistImageCollection
    {
        private List<byte[]> images = new List<byte[]>();

        public MnistImageCollection(string imagesFile)
        {
            using (var imagesFileReader = new BinaryReader(File.Open(imagesFile, FileMode.Open)))
            {                
                imagesFileReader.ReadInt32();
                int imageCount = ReadBigEndianInt32(imagesFileReader);
                int columns = ReadBigEndianInt32(imagesFileReader);
                int rows = ReadBigEndianInt32(imagesFileReader);
                int imageSize = columns * rows;                
                
                for (int i = 0; i < imageCount; i++)
                {
                    images.Add(imagesFileReader.ReadBytes(imageSize));
                }
            }
        }

        public List<byte[]> GetImages()
        {
            return images;
        }

        private int ReadBigEndianInt32(BinaryReader reader)
        {
            byte[] bytes = reader.ReadBytes(4);
            return ((bytes[3] | (bytes[2] << 8)) | (bytes[1] << 0x10)) | (bytes[0] << 0x18);
        }

    }
}
