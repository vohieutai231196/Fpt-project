using Application.Feature.Properties.Command;
using Application.Feature.Properties.Query;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PropertiessController : ControllerBase
    {
        private readonly ISender _mediatrSender;
        public PropertiessController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }
        [HttpPost("add")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> AddNewProperty([FromBody] NewProperty newPropertyRequest)
        {
            bool isSuccessful = await _mediatrSender.Send(new CreatePropertyRequest(newPropertyRequest));
            if (isSuccessful)
            {
                var responseSuccess = ApiResponse<bool>.Success(isSuccessful, true, "Property is created succesfully.");
                return Ok(responseSuccess);
            }
            else
            {
                var responseFailure = ApiResponse<bool>.Error("Failed to create property.");
                return BadRequest(responseFailure);
            }
        }
        [HttpPut("update")]
        [ProducesResponseType(typeof(ApiResponse<UpdateProperty?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> UpdateProperty([FromBody] UpdateProperty updateProperty)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdatePropertyRequest(updateProperty));
            if (isSuccessful)
            {
                var responseSuccess = ApiResponse<bool>.Success(isSuccessful, true, "Property is updateed succesfully.");
                return Ok(responseSuccess);
            }
            else
            {
                var responseFailure = ApiResponse<bool>.Error("Failed to update property.");
                return BadRequest(responseFailure);
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<PropertyDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> GetPropertyById(int id)
        {
            PropertyDto propertyDto = await _mediatrSender.Send(new GetPropertyByIdRequest(id));
            if (propertyDto != null)
            {
                var responseSuccess = ApiResponse<PropertyDto?>.Success(propertyDto, true, "Property is found successfully.");
                return Ok(responseSuccess);
            }
            else
            {

                var responseFailure = ApiResponse<PropertyDto?>.NotFound("Property does not exists.");
                return NotFound(responseFailure);
            }
        }
        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResponse<PropertyDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProperty()
        {
            List<PropertyDto> propertyDtos = await _mediatrSender.Send( new GetPropertyRequest());
            if (propertyDtos != null)
            {
                var responseSuccess = ApiResponse<List<PropertyDto>>.Success(propertyDtos, true, "Property is found successfully.");
                return Ok(responseSuccess);
            }
            else
            {

                var responseFailure = ApiResponse<PropertyDto?>.NotFound("No Properties were found.");
                return NotFound(responseFailure);
            }
        }
        //step 3
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            bool isSuccessful = await _mediatrSender.Send(new DeletePropertyRequest(id));
            if (isSuccessful)
            {
                var responseSuccess = ApiResponse<bool>.Success(isSuccessful, true, "Property deleted successfully.");
                return Ok(responseSuccess);
            }
            else
            {
                var responseFailure = ApiResponse<bool>.NotFound("Property does not exists.");
                return NotFound(responseFailure);
            }
        }
        
    }
}
