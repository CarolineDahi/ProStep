using ProStep.DataTransferObject.General.University;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.UniversityRepository
{
    public interface IUniversityRepository
    {
        Task<OperationResult<IEnumerable<GetUniversityDto>>> GetAll();
        Task<OperationResult<GetUniversityDto>> GetById(Guid id);
        Task<OperationResult<GetUniversityDto>> Add(AddUniversityDto universityDto);
        Task<OperationResult<GetUniversityDto>> Update(UpdateUniversityDto universityDto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
    }
}
