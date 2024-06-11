using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MatrixProcessor.Models; // Импорт вашего пространства имен для моделей

namespace MatrixProcessor.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostProcessMatrixAsync()
        {
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                var json = await reader.ReadToEndAsync();
                var request = JsonSerializer.Deserialize<MatrixProcessor.Models.TransposeRequest>(json); // Полный путь к вашей модели TransposeRequest

                // Логика обработки матрицы и формирования ответа
                var result = ProcessMatrix(request.Matrix);

                // Создание объекта ответа
                var response = new MatrixProcessor.Models.TransposeResponse // Полный путь к вашей модели TransposeResponse
                {
                    TransposedMatrix = result
                };

                // Возвращение ответа в формате JSON
                return new JsonResult(response);
            }
        }

        // Метод для обработки матрицы (предполагается, что у вас есть подобный метод)
        private int[][] ProcessMatrix(int[][] matrix)
        {
            // Пример обработки матрицы: транспонирование
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            // Создаем новую матрицу для результата транспонирования
            int[][] transposedMatrix = new int[cols][];

            for (int i = 0; i < cols; i++)
            {
                transposedMatrix[i] = new int[rows];
                for (int j = 0; j < rows; j++)
                {
                    transposedMatrix[i][j] = matrix[j][i];
                }
            }

            return transposedMatrix;
        }
    }
}
