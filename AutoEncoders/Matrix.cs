using System;
using System.Diagnostics;
using System.Linq;

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
                randomArray[i] = random.NextDouble() - .5;
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

        public static double[][] Multiply(double[][] matrix, double constant)
        {
            int rows = matrix.Length;         

            double[][] result = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[matrix[i].Length];
                for (int j = 0; j < matrix[i].Length; j++)
                {                    
                    result[i][j] = matrix[i][j] * constant;
                }
            }

            return result;
        }

        public static double[][][] Multiply(double[][][] matrix, double constant)
        {
            int layers = matrix.Length;

            double[][][] result = new double[layers][][];

            for (int i = 0; i < layers; i++)
            {
                result[i] = new double[matrix[i].Length][];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    result[i][j] = new double[matrix[i][j].Length];
                    for (int k = 0; k < matrix[i][j].Length; k++)
                    {
                        result[i][j][k] = matrix[i][j][k] * constant;
                    }
                }
            }

            return result;
        }

        public static double[][] Transpose(double[][] matrix)
        {
            int rows = matrix.Length;
            int columns = matrix[0].Length;

            double[][] transpose = new double[columns][];
            
            for (int i = 0; i < columns; i++)
            {
                transpose[i] = new double[rows];
                
                for (int j = 0; j < rows; j++)
                {
                    transpose[i][j] = matrix[j][i];
                }
            }

            return transpose;
        }

        public static double[] Multiply(double[] firstVector, double[] secondVector)
        {
            Debug.Assert(firstVector.Length == secondVector.Length);

            double[] result = new double[firstVector.Length];

            for (int i = 0; i < firstVector.Length; i++)
            {
                result[i] = firstVector[i] * secondVector[i];
            }

            return result;
        }

        public static double[] GetIdentity(int size)
        {
            return Enumerable.Repeat(1.0, size).ToArray();
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

        public static double[] Subtract(double[] firstVector, double[] secondVector)
        {
            Debug.Assert(firstVector.Length == secondVector.Length);

            double[] result = new double[firstVector.Length];

            for (int i = 0; i < firstVector.Length; i++)
            {
                result[i] = firstVector[i] - secondVector[i];
            }

            return result;
        }

        public static double[][] Subtract(double[][] firstMatrix, double[][] secondMatrix)
        {
            Debug.Assert(firstMatrix.Length == secondMatrix.Length);

            int rows = firstMatrix.Length;        

            double[][] result = new double[rows][];

            for (int i = 0; i < rows; i++)
            {
                result[i] = new double[firstMatrix[i].Length];
                for (int j = 0; j < firstMatrix[i].Length; j++)
                {
                    result[i][j] = firstMatrix[i][j] - secondMatrix[i][j];
                }
            }

            return result;
        }

        public static double[][][] Subtract(double[][][] firstMatrix, double[][][] secondMatrix)
        {
            Debug.Assert(firstMatrix.Length == secondMatrix.Length);

            int layers = firstMatrix.Length;

            double[][][] result = new double[layers][][];

            for (int i = 0; i < layers; i++)
            {
                result[i] = new double[firstMatrix[i].Length][];
                for (int j = 0; j < firstMatrix[i].Length; j++)
                {
                    result[i][j] = new double[firstMatrix[i][j].Length];
                    for (int k = 0; k < firstMatrix[i][j].Length; k++)
                    {
                        result[i][j][k] = firstMatrix[i][j][k] - secondMatrix[i][j][k];
                    }
                }
            }

            return result;
        }

        public static double[] Sigmoid(double[] vector)
        {
            double[] result = new double[vector.Length];

            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = 1/(1 + Math.Exp(-vector[i]));
            }

            return result;
        }
    }
}
