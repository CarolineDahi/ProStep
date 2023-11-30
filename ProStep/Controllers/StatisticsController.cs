using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.Courses.BootcampRepository;
using ProStep.DataTransferObject.Courses.Bootcamp;
using ProStep.DataTransferObject.General.Statistics;
using ProStep.General.StatisticsRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;

namespace ProStep.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsRepository statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            this.statisticsRepository = statisticsRepository;
        }

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCountDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCounts()
            => await statisticsRepository.GetCounts().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCategoryWithCountDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCategories()
            => await statisticsRepository.GetCategories().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCategoryWithCountDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoadMaps()
            => await statisticsRepository.GetRoadMaps().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetMonthsDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsers()
            => await statisticsRepository.GetUsers().ToJsonResultAsync();
    }
}
