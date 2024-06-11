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
        public async Task<IActionResult> Transpose([FromBody] TransposeRequest request)
        {
            try
            {
                var transposedMatrix = await _matrixService.TransposeAsync(request.Matrix);
                return Ok(transposedMatrix);
            }
            catch (MatrixException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while transposing matrix.");
            }
        }

        [HttpPost("determinant")]
        public async Task<IActionResult> CalculateDeterminant([FromBody] DeterminantRequest request)
        {
            try
            {
                var determinant = await _matrixService.CalculateDeterminantAsync(request.Matrix);
                return Ok(determinant);
            }
            catch (MatrixException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while calculating determinant.");
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
            catch (MatrixException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error while multiplying matrices.");
            }
        }
    }
}
