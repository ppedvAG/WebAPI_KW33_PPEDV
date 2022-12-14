using FileUpDownloadSample.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FileUpDownloadSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }


        #region Upload
        [HttpPost(nameof(Upload))]
        public IActionResult Upload([Required] List<IFormFile> formFiles, [Required] string subDirectory)
        {
            try
            {
                _fileService.UploadFile(formFiles, subDirectory);

                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion


        #region Download File
        [HttpGet(nameof(Download))]
        public IActionResult Download([Required] string subDirectory)
        {
            try
            {
                var (fileType, archiveData, archiveName) = _fileService.DownloadFiles(subDirectory);
                return File(archiveData, "application/zip", archiveName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
