using ProStep.DataTransferObject.Maps.RoadMap;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Maps.RoadMapRepository
{
    public interface IRoadMapRepository
    {
        Task<OperationResult<IEnumerable<GetRoadMapDto>>> GetAll(Guid? categoryId);
        Task<OperationResult<DetailsRoadMapDto>> GetById(Guid id);
        Task<OperationResult<DetailsRoadMapDto>> Add(AddRoadMapDto dto);
        Task<OperationResult<DetailsRoadMapDto>> Update();
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
    }
}
