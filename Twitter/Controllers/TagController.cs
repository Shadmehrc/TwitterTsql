using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.ServiceInterfaces;
using Core.ApiResponse;
using Core.Models;
using Infrastructure.SQL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;


namespace Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagCrudService _iTagCrudService;
        public TagController(ITagCrudService iTagCrudService)
        {
            _iTagCrudService = iTagCrudService;
        }

        [HttpPost("CreateTag")]
        public async Task<IActionResult> Create(string word)
        {
            var result = await _iTagCrudService.Create(word);
            return result ? new ApiResponse().Success("Tag successfully created.")
                : new ApiResponse().FailedToFind("Some error happened.");
        }


        [HttpGet("ShowTag")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _iTagCrudService.Get(id);
            return Ok(result);
        }

        [HttpPut("EditTag")]
        public async Task<IActionResult> Edit([FromQuery]EditTweetDTO model)
        {
            var result = await _iTagCrudService.Edit(model);
            return result ? new ApiResponse().Success("Tag successfully Edited.")
                : new ApiResponse().FailedToFind("Tag not found.");
        }

        [HttpDelete("DeleteTag")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _iTagCrudService.Delete(id);
            return result ? new ApiResponse().Success("Tag successfully deleted.")
                : new ApiResponse().FailedToFind("Tag not found.");
        }
    }
}
