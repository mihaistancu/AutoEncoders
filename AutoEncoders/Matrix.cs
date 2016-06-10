using System;

namespace AutoEncoders
{
    class Matrix
    {
        public static double[][] CreateRandom(int rows, int columns)
        {
            double[][] matrix = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                matrix[i] = CreateRandomVector(columns);
            }

            return matrix;
        }

        public static double[] CreateRandomVector(int arraySize)
        {
            double[] randomArray = new double[arraySize];

            var random = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < arraySize; i++)
            {
                randomArray[i] = random.NextDouble();
            }

            return randomArray;
        }
    }
}
