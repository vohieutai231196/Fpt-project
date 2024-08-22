using Application.Models;
using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.Images.Commands
{
    public class DeleteImageRequest : IRequest<bool>
    {
        public int ImageId { get; set; }
        public List<string> CacheKeys { get; set; }

        public DeleteImageRequest(int imageId)
        {
            ImageId = imageId;
            CacheKeys = new() { $"GetImageByIdRequest:{ImageId}", "GetImagesRequest" };
        }
    }

    public class DeleteImageRequestHandler : IRequestHandler<DeleteImageRequest, bool>
    {
        private readonly IImageRepo _imageRepo;

        public DeleteImageRequestHandler(IImageRepo imageRepo)
        {
            _imageRepo = imageRepo;
        }

        public async Task<bool> Handle(DeleteImageRequest request, CancellationToken cancellationToken)
        {
            Image imageInDb = await _imageRepo.GetByIdAsync(request.ImageId);
            if (imageInDb != null)
            {
                await _imageRepo.DeleteAsync(imageInDb);
                return true;
            }
            return false;
        }
    }
}
