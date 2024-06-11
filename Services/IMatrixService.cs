using System.Threading.Tasks;

namespace MatrixProcessor.Services
{
    public interface IMatrixService
    {
        Task<int[][]> TransposeAsync(int[][] matrix);
        Task<double> CalculateDeterminantAsync(int[][] matrix);
        Task<int[][]> MultiplyAsync(int[][] matrixA, int[][] matrixB);
    }
}
