using System;
using System.IO;
using System.Threading.Tasks;
using AIH.DEMO.VideoStreaming.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace AIH.DEMO.VideoStreaming.Controllers
{
    public static class Constants
    {
        public const string ApiTemplate = "api/[controller]";
    }

    [ApiController]
    [Route(Constants.ApiTemplate)]
    public class FileUploadController : ControllerBase
    {
        private readonly FileService _fileService;
        private readonly string _targetFilePath;
        private readonly string _targetFfmegeExePath;

        public FileUploadController(FileService fileService, IConfiguration config)
        {
            _fileService = fileService;
            _targetFilePath = config.GetValue<string>("StoredFilesPath");
            _targetFfmegeExePath = config.GetValue<string>("FFMPEGExePath");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("okay man");
        }
        [HttpPost]
        public async Task<IActionResult> UploadVideo()
        {
            //var request = HttpContext.Request;
            //// validation of Content-Type
            //// 1. first, it must be a form-data request
            //// 2. a boundary should be found in the Content-Type
            //if (!request.HasFormContentType ||
            //    !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
            //    string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
            //{
            //    return new UnsupportedMediaTypeResult();
            //}

            //var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
            //var section = await reader.ReadNextSectionAsync();


            //// This sample try to get the first file from request and save it
            //// Make changes according to your needs in actual use
            //while (section != null)
            //{
            //    var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition,
            //        out var contentDisposition);

            //    if (hasContentDispositionHeader && contentDisposition.DispositionType.Equals("form-data") &&
            //        !string.IsNullOrEmpty(contentDisposition.FileName.Value))
            //    {
            //        // Don't trust any file name, file extension, and file data from the request unless you trust them completely
            //        // Otherwise, it is very likely to cause problems such as virus uploading, disk filling, etc
            //        // In short, it is necessary to restrict and verify the upload
            //        // Here, we just use the temporary folder and a random file name

            //        // Get the temporary folder, and combine a random file name with it
            //        var fileName = Path.GetRandomFileName();
            //        var saveToPath = Path.Combine(_targetFilePath, $"{fileName}.mp4");

            //        using (var targetStream = System.IO.File.Create(saveToPath))
            //        {
            //            await section.Body.CopyToAsync(targetStream);
            //        }


            //        return Ok();
            //    }
            //    section = await reader.ReadNextSectionAsync();
            //}

            //// If the code runs to this location, it means that no files have been saved
            //return BadRequest("No files data in the request.");

            await _fileService.ProcessVideo();
            return Ok();
        }

        public void ProcessVideo()
        {

        }
    }
}
