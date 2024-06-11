namespace MatrixProcessor.Models
{
    public class TransposeRequest
    {
        public int[][] Matrix { get; set; } = Array.Empty<int[]>(); // Инициализация по умолчанию
    }
}
