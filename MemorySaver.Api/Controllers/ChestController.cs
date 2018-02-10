using System;
using MemorySaver.Domain.ServiceContracts.DTOs.Request;
using MemorySaver.Domain.ServiceContracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MemorySaver.Api.Controllers
{
    [Route("api/Chest")]
    public class ChestController : Controller
    {
        private readonly IChestService chestService;
        private readonly IFileService fileService;

        public ChestController(IChestService chestService, IFileService fileService)
        {
            this.chestService = chestService;
            this.fileService = fileService;
        }
        
        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var response = chestService.GetChest(id);

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody]CreateChestRequestDTO chestModel)
        {
            if (chestService.CreateChest(chestModel))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("{id:guid}/add-file")]
        public IActionResult AddFile(Guid id, IFormFile file, string description, string facebookId, string vimeoId)
        {
            var memoryStream = new MemoryStream();
            if (file == null)
            {
                return BadRequest();
            }

            file.CopyTo(memoryStream);
            var fileInByte = memoryStream.ToArray();

            if(fileService.UploadFile(fileInByte, file.FileName, description, facebookId, vimeoId, id))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("{id:guid}")]
        public IActionResult EditChest(Guid id, EditChestRequestDTO chestDetails)
        {
            chestDetails.Id = id;
            if (chestService.EditChest(chestDetails))
            {
                return Accepted();
            }

            return BadRequest();
        }

        [HttpDelete("id:guid")]
        public IActionResult DeleteChest(Guid id)
        {
            if (chestService.DeleteChest(id))
            {
                return Accepted();
            }

            return BadRequest();
        }
    }
}
