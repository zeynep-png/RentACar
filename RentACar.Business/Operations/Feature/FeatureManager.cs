﻿using RentACar.Business.Operations.Feature.Dtos;
using RentACar.Business.Types;
using RentACar.Data.Entities;
using RentACar.Data.Repositories;
using RentACar.Data.UnifOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Operations.Feature
{
    public class FeatureManager : IFeatureService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<FeatureEntity> _repository;

       
        public FeatureManager(IUnitOfWork unitOfWork, IRepository<FeatureEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        // Adds a new feature if it does not already exist in the repository
        public async Task<ServiceMessage> AddFeature(AddFeatureDto feature)
        {
            // Checks if a feature with the same title (case insensitive) already exists
            var hasFeature = _repository.GetAll(x => x.Title.ToLower() == feature.Title.ToLower()).Any();

            if (hasFeature)
            {
                // Returns a failure message if the feature already exists
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Feature already exist."
                };
            }

            // Creates a new FeatureEntity object from the provided DTO
            var featureEntity = new FeatureEntity
            {
                Title = feature.Title
            };

            // Adds the new feature entity to the repository
            _repository.Add(featureEntity);

            try
            {
                // Saves changes to the database asynchronously
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception )
            {
                // Throws a general exception if an error occurs during saving
                throw new Exception("Error due adding feature");
            }

            // Returns a success message if the feature was added successfully
            return new ServiceMessage
            {
                IsSucceed = true
            };
        }
    }
}
