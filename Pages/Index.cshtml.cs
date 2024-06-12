using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace MatrixProcessor.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public double[][]? Matrix { get; set; }

        [BindProperty]
        public double[][]? MatrixA { get; set; }

        [BindProperty]
        public double[][]? MatrixB { get; set; }

        public class TransposeRequest
        {
            public double[][]? Matrix { get; set; }
        }

        public class DeterminantRequest
        {
            public double[][]? Matrix { get; set; }
        }

        public class MultiplyRequest
        {
            public double[][]? MatrixA { get; set; }
            public double[][]? MatrixB { get; set; }
        }

        public IActionResult OnPostTransposeMatrix([FromBody] TransposeRequest request)
        {
            if (request.Matrix == null || request.Matrix.Length == 0)
            {
                return BadRequest("Invalid matrix data.");
            }

            var transposedMatrix = Transpose(request.Matrix);
            return new JsonResult(transposedMatrix);
        }

        public IActionResult OnPostDeterminantMatrix([FromBody] DeterminantRequest request)
        {
            if (request.Matrix == null || request.Matrix.Length == 0 || request.Matrix.Length != request.Matrix[0].Length)
            {
                return BadRequest("Invalid matrix data.");
            }

            var determinant = CalculateDeterminant(request.Matrix);
            return new JsonResult(new { Determinant = determinant });
        }

        public IActionResult OnPostMultiplyMatrices([FromBody] MultiplyRequest request)
        {
            if (request.MatrixA == null || request.MatrixB == null || request.MatrixA.Length == 0 || request.MatrixB.Length == 0)
            {
                return BadRequest("Invalid matrix data.");
            }

            var resultMatrix = MultiplyMatrices(request.MatrixA, request.MatrixB);
            if (resultMatrix == null)
            {
                return BadRequest("Incompatible matrix dimensions.");
            }

            return new JsonResult(resultMatrix);
        }

        private double[][] Transpose(double[][] matrix)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            var transposedMatrix = new double[cols][];

            for (int i = 0; i < cols; i++)
            {
                transposedMatrix[i] = new double[rows];
                for (int j = 0; j < rows; j++)
                {
                    transposedMatrix[i][j] = matrix[j][i];
                }
            }

            return transposedMatrix;
        }

        private double CalculateDeterminant(double[][] matrix)
        {
            int n = matrix.Length;
            if (n == 1)
            {
                return matrix[0][0];
            }

            double determinant = 0;
            for (int i = 0; i < n; i++)
            {
                determinant += Math.Pow(-1, i) * matrix[0][i] * CalculateDeterminant(Minor(matrix, 0, i));
            }

            return determinant;
        }

        private double[][] Minor(double[][] matrix, int row, int col)
        {
            int n = matrix.Length;
            var minor = new double[n - 1][];
            for (int i = 0; i < n - 1; i++)
            {
                minor[i] = new double[n - 1];
            }

            for (int i = 0, mi = 0; i < n; i++)
            {
                if (i == row) continue;
                for (int j = 0, mj = 0; j < n; j++)
                {
                    if (j == col) continue;
                    minor[mi][mj] = matrix[i][j];
                    mj++;
                }
                mi++;
            }

            return minor;
        }

        private double[][]? MultiplyMatrices(double[][] matrixA, double[][] matrixB)
        {
            int rowsA = matrixA.Length;
            int colsA = matrixA[0].Length;
            int rowsB = matrixB.Length;
            int colsB = matrixB[0].Length;

            if (colsA != rowsB)
            {
                return null;
            }

            var resultMatrix = new double[rowsA][];
            for (int i = 0; i < rowsA; i++)
            {
                resultMatrix[i] = new double[colsB];
                for (int j = 0; j < colsB; j++)
                {
                    resultMatrix[i][j] = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        resultMatrix[i][j] += matrixA[i][k] * matrixB[k][j];
                    }
                }
            }

            return resultMatrix;
        }
    }
}