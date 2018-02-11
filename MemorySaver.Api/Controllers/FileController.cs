using System;
using System.IO;
using MemorySaver.Domain.ServiceContracts.DTOs.Response;
using MemorySaver.Domain.ServiceContracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MemorySaver.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/File")]
    public class FileController : Controller
    {
        private readonly IFileService fileService;

        public FileController(IFileService fileService)
        {
            this.fileService = fileService;
        }

        [HttpGet("{id:guid}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            GetFileResponseDTO fileResponseDto = fileService.GetFile(id);

            if (fileResponseDto != null)
            {
                return Ok(fileResponseDto);
            }

            return BadRequest();
        }

        [HttpPut("{id:guid}")]
        public IActionResult Put(Guid id, [FromBody]string value)
        {
            return BadRequest();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            if (fileService.DeleteFile(id))
            {
                return Accepted();
            }

            return BadRequest();
        }

        [HttpGet("{id:guid}/download")]
        public IActionResult DownloadFeedbackForTeamMember(Guid id)
        {
            var result = fileService.GetFileForDownload(id);
            var stream = new MemoryStream(result);

            return File(stream, "application/octet-stream");
        }
    }
}
