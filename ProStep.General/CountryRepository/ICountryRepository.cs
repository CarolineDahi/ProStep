using ProStep.DataTransferObject.General.City;
using ProStep.DataTransferObject.General.Country;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.CountryRepository
{
    public interface ICountryRepository
    {
        Task<OperationResult<IEnumerable<GetCountryDto>>> GetAll();
        Task<OperationResult<GetCountryDto>> GetById(Guid id);
        Task<OperationResult<GetCountryDto>> Add(AddCountryDto cityDto);
        Task<OperationResult<GetCountryDto>> Update(UpdateCountryDto cityDto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
    }
}
