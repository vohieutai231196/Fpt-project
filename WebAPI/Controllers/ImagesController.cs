using Application.Feature.Properties.Command;
using Application.Features.Images.Commands;
using Application.Features.Images.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public ImagesController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> AddNewImage([FromBody] NewImage newImage)
        {
            bool isSuccessful = await _mediatrSender.Send(new CreateImageRequest(newImage));
            if (isSuccessful)
            {
                var responseSuccess = ApiResponse<bool>.Success(isSuccessful, true, "Image created successfully.");
                return Ok(responseSuccess);
            }
            else
            {
                var responseFailure = ApiResponse<bool>.Error("Failed to create image.");
                return BadRequest(responseFailure);
            }
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(ApiResponse<UpdateProperty?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> UpdateImage(UpdateImage updateImage)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdateImageRequest(updateImage));
            if (isSuccessful)
            {
                var responseSuccess = ApiResponse<bool>.Success(isSuccessful, true, "Image updated successfully.");
                return Ok(responseSuccess);
            }
            else
            {
                var responseFailure = ApiResponse<bool>.Error("Image does not exists.");
                return BadRequest(responseFailure);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            bool isSuccessful = await _mediatrSender.Send(new DeleteImageRequest(id));
            if (isSuccessful)
            {
                var responseSuccess = ApiResponse<bool>.Success(isSuccessful, true, "Image deleted successfully.");
                return Ok(responseSuccess);
            }
            else
            {
                var responseFailure = ApiResponse<bool>.NotFound("Image does not exists.");
                return NotFound(responseFailure);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<PropertyDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> GetImage(int id)
        {
            ImageDto image = await _mediatrSender.Send(new GetImageByIdRequest(id));
            if (image != null)
            {
                var responseSuccess = ApiResponse<ImageDto?>.Success(image, true, "Image is found successfully.");
                return Ok(responseSuccess);
            }
            else
            {

                var responseFailure = ApiResponse<ImageDto?>.NotFound("Image does not exists.");
                return NotFound(responseFailure);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(typeof(ApiResponse<PropertyDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> GetImages()
        {
            List<ImageDto> images = await _mediatrSender.Send(new GetImagesRequest());
            if (images != null)
            {
                var responseSuccess = ApiResponse<List<ImageDto>>.Success(images, true, "Images is found successfully.");
                return Ok(responseSuccess);
            }
            else
            {

                var responseFailure = ApiResponse<PropertyDto?>.NotFound("No Images were found.");
                return NotFound(responseFailure);
            }
        }
    }
}
