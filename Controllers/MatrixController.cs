using Microsoft.AspNetCore.Mvc;
using MatrixProcessor.Services;
using MatrixProcessor.Models;
using System;
using System.Threading.Tasks;

namespace MatrixProcessor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatrixController : ControllerBase
    {
        private readonly IMatrixService _matrixService;

        public MatrixController(IMatrixService matrixService)
        {
            _matrixService = matrixService;
        }

        [HttpPost("transpose")]
        public async Task<IActionResult> TransposeMatrix([FromBody] int[][] matrix)
        {
            try
            {
                var transposedMatrix = await _matrixService.TransposeAsync(matrix);
                return Ok(transposedMatrix);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("determinant")]
        public async Task<IActionResult> CalculateDeterminant([FromBody] int[][] matrix)
        {
            try
            {
                var determinant = await _matrixService.CalculateDeterminantAsync(matrix);
                return Ok(determinant);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        [HttpPost("multiply")]
        public async Task<IActionResult> Multiply([FromBody] MultiplicationRequest request)
        {
            try
            {
                var result = await _matrixService.MultiplyAsync(request.MatrixA, request.MatrixB);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            if (ex is MatrixException matrixException)
            {
                return BadRequest(matrixException.Message);
            }

            return StatusCode(500, "Internal server error.");
        }
    }
}