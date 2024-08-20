using Application.Feature.Command;
using Application.Feature.Query;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertiessController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        public PropertiessController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddNewProperty([FromBody] NewProperty newPropertyRequest)
        {
            bool isSuccessful = await _mediatrSender.Send(new CreatePropertyRequest(newPropertyRequest));
            if (isSuccessful)
            {
                return Ok("Property is created succesfully");
            }
            return BadRequest("Failed to create property");
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProperty([FromBody] UpdateProperty updateProperty)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdatePropertyRequest(updateProperty));
            if (isSuccessful)
            {
                return Ok("Property is updated succesfully");
            }
            return NotFound("Property does not exists");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            PropertyDto propertyDto = await _mediatrSender.Send(new GetPropertyByIdRequest(id));
            if (propertyDto != null)
            {
                return Ok(propertyDto);
            }
            return NotFound("Property does not exists");
        }
    }
}
