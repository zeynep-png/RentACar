using RentACar.Business.Operations.Feature.Dtos;
using RentACar.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Business.Operations.Feature
{
    public interface IFeatureService
    {
        Task<ServiceMessage> AddFeature(AddFeatureDto feature);
    }
}
