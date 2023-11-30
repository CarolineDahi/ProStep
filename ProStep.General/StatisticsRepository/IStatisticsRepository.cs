using ProStep.DataTransferObject.General.Statistics;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.StatisticsRepository
{
    public interface IStatisticsRepository
    {
        Task<OperationResult<GetCountDto>> GetCounts();
        Task<OperationResult<IEnumerable<GetCategoryWithCountDto>>> GetCategories();
        Task<OperationResult<IEnumerable<GetCategoryWithCountDto>>> GetRoadMaps();
        Task<OperationResult<IEnumerable<GetMonthsDto>>> GetUsers();
    }
}
