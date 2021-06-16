using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos;
using Application.Repos;
using Application.Services;
using Domain.Exceptions;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using PhotoEntity = Domain.Entities.Photo;

namespace Application.Functions.Photo.Commands
{
    public class AddPhotoCommandHandler : IRequestHandler<AddPhotoCommand, Unit>
    {
        private readonly IPhotoRepo _photoRepo;
        private readonly ICloudinaryService _cloudinaryService;


        public AddPhotoCommandHandler(IPhotoRepo photoRepo, ICloudinaryService cloudinaryService)
        {
            _photoRepo = photoRepo ?? throw new ArgumentNullException(nameof(photoRepo));
            _cloudinaryService = cloudinaryService ?? throw new ArgumentNullException(nameof(cloudinaryService));
        }

        public async Task<Unit> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
        {

            var uploadResult = await _cloudinaryService.AddPhotoWithRandomGuidAsync(request.Photo.File, cancellationToken);

            var photoToAdd = CreatePhotoEntity(uploadResult, request);

            var result = await AddPhotoToDatabaseAsync(photoToAdd);

            return result;
        
        }

        private PhotoEntity CreatePhotoEntity((string, string) uploadResultTuple, AddPhotoCommand request)
        {
            var photoEntity = new PhotoEntity();

            photoEntity.Url = uploadResultTuple.Item1;
            photoEntity.Id = Guid.Parse(uploadResultTuple.Item2);
            photoEntity.Description = request.Photo.Description;
            photoEntity.UserId = request.UserId;

            return photoEntity;
        }

        private async Task<Unit> AddPhotoToDatabaseAsync(PhotoEntity photoToAdd)
        {
            await _photoRepo.AddAsync(photoToAdd);

            if (await _photoRepo.SaveAllAsync())
            {
                return await Task.FromResult(Unit.Value);   
            }

            throw new DatabaseException("Error occured while updating database.");
        }

    }
}