using System.IO;
using System.Web.Script.Serialization;

namespace AutoEncoders.UI
{
    public class NeuralNetworkSerializer
    {
        public void Save(NeuralNetwork network, string filename)
        {
            var serializer = new JavaScriptSerializer();
            var serialization = serializer.Serialize(network);
            File.WriteAllText(filename, serialization);
        }

        public NeuralNetwork Load(string filename)
        {
            var serializer = new JavaScriptSerializer();
            var serialization = File.ReadAllText(filename);
            return (NeuralNetwork)serializer.Deserialize(serialization, typeof(NeuralNetwork));
        }
    }
}