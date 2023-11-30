using ProStep.DataTransferObject.Courses.Bootcamp;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Courses.BootcampRepository
{
    public interface IBootcampRepository
    {
        Task<OperationResult<IEnumerable<GetBootcampDto>>> GetAll(Guid? categoryId);
        Task<OperationResult<IEnumerable<GetBootcampDto>>> GetDash(Guid? categoryId, string? name, DateTime? startDate, DateTime? endDate);
        Task<OperationResult<IEnumerable<GetBootcampDto>>> GetRecommendrd();
        Task<OperationResult<IEnumerable<GetBootcampDto>>> GetFavorite();
        Task<OperationResult<IEnumerable<GetBootcampDto>>> GetMyLearning();
        Task<OperationResult<GetDetailsBootcampDto>> GetById(Guid id);
        Task<OperationResult<GetDetailsBootcampDto>> Add(AddBootcampDto dto);
        Task<OperationResult<GetDetailsBootcampDto>> Update(UpdateBootcampDto dto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
        Task<OperationResult<bool>> AddToFavourite(Guid id);
        Task<OperationResult<bool>> RemoveFromFavourite(Guid id);
        Task<OperationResult<bool>> AddToLearning(Guid id);
        Task<OperationResult<IEnumerable<GetBootcampDto>>> GetByCoachId(Guid id);

    }
}
