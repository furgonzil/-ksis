namespace MatrixProcessor.Models
{
    public class MultiplicationRequest
    {
        public int[][] MatrixA { get; set; } = Array.Empty<int[]>(); // Инициализация по умолчанию
        public int[][] MatrixB { get; set; } = Array.Empty<int[]>(); // Инициализация по умолчанию
    }
}
