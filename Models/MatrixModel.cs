using System;
using System.Threading.Tasks;

namespace MatrixProcessor.Models
{
    public class MatrixModel
    {
        public int[,] Matrix { get; set; }

        public MatrixModel(int[,] matrix)
        {
            Matrix = matrix ?? throw new ArgumentNullException(nameof(matrix));
        }

        public async Task<int[,]> TransposeAsync()
        {
            return await Task.Run(() =>
            {
                int rows = Matrix.GetLength(0);
                int cols = Matrix.GetLength(1);
                int[,] transposed = new int[cols, rows];

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        transposed[j, i] = Matrix[i, j];
                    }
                }

                return transposed;
            });
        }

        public async Task<int> CalculateDeterminantAsync()
        {
            return await Task.Run(() => CalculateDeterminant(Matrix));
        }

        private int CalculateDeterminant(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n != matrix.GetLength(1))
                throw new InvalidOperationException("Matrix must be square to calculate determinant.");

            if (n == 1)
                return matrix[0, 0];

            if (n == 2)
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];

            int determinant = 0;
            for (int p = 0; p < n; p++)
            {
                int[,] subMatrix = new int[n - 1, n - 1];
                for (int i = 1; i < n; i++)
                {
                    int colIndex = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j == p) continue;
                        subMatrix[i - 1, colIndex] = matrix[i, j];
                        colIndex++;
                    }
                }
                determinant += (p % 2 == 0 ? 1 : -1) * matrix[0, p] * CalculateDeterminant(subMatrix);
            }

            return determinant;
        }

        public async Task<int[,]> MultiplyAsync(int[,] otherMatrix)
        {
            return await Task.Run(() => Multiply(Matrix, otherMatrix));
        }

        private int[,] Multiply(int[,] matrixA, int[,] matrixB)
        {
            int aRows = matrixA.GetLength(0);
            int aCols = matrixA.GetLength(1);
            int bRows = matrixB.GetLength(0);
            int bCols = matrixB.GetLength(1);

            if (aCols != bRows)
                throw new InvalidOperationException("Number of columns in the first matrix must match the number of rows in the second matrix.");

            int[,] result = new int[aRows, bCols];

            for (int i = 0; i < aRows; i++)
            {
                for (int j = 0; j < bCols; j++)
                {
                    for (int k = 0; k < aCols; k++)
                    {
                        result[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }

            return result;
        }
    }
}