using ProStep.DataTransferObject.General.Faculty;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.FacultyRepository
{
    public interface IFacultyRepository
    {
        Task<OperationResult<IEnumerable<GetFacultyDto>>> GetAll();
        Task<OperationResult<GetFacultyDto>> GetById(Guid id);
        Task<OperationResult<GetFacultyDto>> Add(AddFacultyDto dto);
        Task<OperationResult<GetFacultyDto>> Update(UpdateFacultyDto dto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> id);
    }
}
