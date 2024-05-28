using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using MatrixProcessor.Models;

namespace MatrixProcessor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatrixController : ControllerBase
    {
        private readonly ILogger<MatrixController> _logger;

        public MatrixController(ILogger<MatrixController> logger)
        {
            _logger = logger;
        }

        [HttpPost("transpose")]
        public async Task<IActionResult> Transpose([FromBody] int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return BadRequest("Matrix cannot be null or empty.");
            }
            try
            {
                var matrixModel = new MatrixModel(matrix);
                var transposed = await matrixModel.TransposeAsync();
                return Ok(transposed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error transposing matrix");
                return StatusCode(500, "Internal server error while transposing matrix.");
            }
        }

        [HttpPost("determinant")]
        public async Task<IActionResult> CalculateDeterminant([FromBody] int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return BadRequest("Matrix cannot be null or empty.");
            }

            try
            {
                var matrixModel = new MatrixModel(matrix);
                var determinant = await matrixModel.CalculateDeterminantAsync();
                return Ok(determinant);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating determinant");
                return StatusCode(500, "Internal server error while calculating determinant.");
            }
        }

        [HttpPost("multiply")]
        public async Task<IActionResult> Multiply([FromBody] MatrixMultiplicationRequest request)
        {
            if (request.MatrixA == null || request.MatrixB == null ||
                request.MatrixA.GetLength(0) == 0 || request.MatrixA.GetLength(1) == 0 ||
                request.MatrixB.GetLength(0) == 0 || request.MatrixB.GetLength(1) == 0)
            {
                return BadRequest("Matrices cannot be null or empty.");
            }

            try
            {
                var matrixModel = new MatrixModel(request.MatrixA);
                var result = await matrixModel.MultiplyAsync(request.MatrixB);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error multiplying matrices");
                return StatusCode(500, "Internal server error while multiplying matrices.");
            }
        }
    }

    public class MatrixMultiplicationRequest
    {
        public int[,]? MatrixA { get; set; }  // Сделаем свойства nullable
        public int[,]? MatrixB { get; set; }  // Сделаем свойства nullable
    }
}