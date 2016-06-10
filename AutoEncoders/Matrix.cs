using System;
using System.Diagnostics;

namespace AutoEncoders
{
    public class Matrix
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

        public static double[] Multiply(double[][] matrix, double[] vector)
        {
            int rows = matrix.Length;
            int columns = matrix[0].Length;
            
            Debug.Assert(columns == vector.Length);

            double[] result = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i] += matrix[i][j]*vector[j];
                }
            }

            return result;
        }

        public static double[] Add(double[] firstVector, double[] secondVector)
        {
            Debug.Assert(firstVector.Length == secondVector.Length);

            double[] result = new double[firstVector.Length];

            for (int i = 0; i < firstVector.Length; i++)
            {
                result[i] = firstVector[i] + secondVector[i];
            }

            return result;
        }
    }
}
