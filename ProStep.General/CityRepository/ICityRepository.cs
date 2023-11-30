using ProStep.DataTransferObject.General.Category;
using ProStep.DataTransferObject.General.City;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.CityRepository
{
    public interface ICityRepository
    {
        Task<OperationResult<IEnumerable<GetCityDto>>> GetAll();
        Task<OperationResult<GetCityDto>> GetById(Guid id);
        Task<OperationResult<GetCityDto>> Add(AddCityDto cityDto);
        Task<OperationResult<GetCityDto>> Update(UpdateCityDto cityDto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
    }
}
