using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.Maps.RoadMap;
using ProStep.Maps.RoadMapRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using System.ComponentModel.DataAnnotations;
using ProStep.SharedKernel.OperationResult;
using ProStep.General.FacultyRepository;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class RoadMapController : Controller
    {
        private readonly IRoadMapRepository roadMapRepository;

        public RoadMapController(IRoadMapRepository roadMapRepository)
        {
            this.roadMapRepository = roadMapRepository;
        }

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetRoadMapDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(Guid? categoryId)
            => await roadMapRepository.GetAll(categoryId).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(DetailsRoadMapDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await roadMapRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(DetailsRoadMapDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddRoadMapDto roadMapDto)
           => await roadMapRepository.Add(roadMapDto).ToJsonResultAsync();

        //[HttpDelete(GlobalValue.RouteDash)]
        //[ProStepAuthorize(ProStepRole.DashUser)]
        //[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        //public async Task<IActionResult> Delete([Required] Guid id)
        //    => await roadMapRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        //[HttpDelete(GlobalValue.RouteDash)]
        //[ProStepAuthorize(ProStepRole.DashUser)]
        //[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        //public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
        //    => await roadMapRepository.Delete(ids).ToJsonResultAsync();
    }
}
