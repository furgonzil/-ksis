namespace MatrixProcessor.Models
{
    public class TransposeResponse
    {
        public int[][] TransposedMatrix { get; set; } = Array.Empty<int[]>(); // Инициализация по умолчанию
    }
}
