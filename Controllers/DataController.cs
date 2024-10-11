using Microsoft.AspNetCore.Mvc;
using ShellCodingTask.Services;

namespace ShellCodingTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly BlobService _blobService;
        private readonly DataProcessingService _dataProcessingService;

        public DataController(BlobService blobService, DataProcessingService dataProcessingService)
        {
            _blobService = blobService;
            _dataProcessingService = dataProcessingService;
        }
        // Endpoint to upload a file to the blobcontainer
        [HttpPost("UploadFileToBlobStorage")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var fileName = file.FileName;
            var blobUrl = await _blobService.UploadFileAsync(stream, fileName);

            return Ok(new { BlobUrl = blobUrl });
        }

        // Endpoint to list all blobs in the container
        [HttpGet("GetAllBlobFiles")]
        public async Task<IActionResult> ListBlobs()
        {
            try
            {
                var blobs = await _blobService.ListBlobsAsync();
                return Ok(blobs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpGet("GetAggregatedResult/")]
        public async Task<IActionResult> GetStats(string fileName, [FromQuery] string date, [FromQuery] string meter, [FromQuery] string dataType)
        {
            // Validate input parameters
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(date) || string.IsNullOrEmpty(meter) || string.IsNullOrEmpty(dataType))
            {
                return BadRequest("File name, date, meter, and dataType must be provided.");
            }
            // Download the file from blob storage
            using var stream = await _blobService.DownloadFileAsync(fileName);
            // Process the data with the parameters
            var (min, max, median) = await _dataProcessingService.ProcessData(stream, date, meter, dataType);
            // Return the aggregated values
            return Ok(new { Min = min, Max = max, Median = median });
        }

    }
}
