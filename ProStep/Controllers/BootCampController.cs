using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.Courses.BootcampRepository;
using ProStep.Courses.CourseRepository;
using ProStep.DataTransferObject.Courses.Bootcamp;
using ProStep.DataTransferObject.General.Category;
using ProStep.General.CategoryRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class BootCampController : Controller
    {
        private readonly IBootcampRepository bootcampRepository;

        public BootCampController(IBootcampRepository bootcampRepository)
        {
            this.bootcampRepository = bootcampRepository;
        }

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetBootcampDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(Guid? categoryId)
            => await bootcampRepository.GetAll(categoryId).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetBootcampDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDash(Guid? categoryId, string? name, DateTime? startDate, DateTime? endDate)
            => await bootcampRepository.GetDash(categoryId, name, startDate, endDate).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(GetDetailsBootcampDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await bootcampRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(GetDetailsBootcampDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromForm] AddBootcampDto dto)
            => await bootcampRepository.Add(dto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(GetDetailsBootcampDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromForm] UpdateBootcampDto dto)
            => await bootcampRepository.Update(dto).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToFavourite([Required] Guid id)
            => await bootcampRepository.AddToFavourite(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromFavourite([Required] Guid id)
            => await bootcampRepository.RemoveFromFavourite(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToLearning([Required] Guid id)
            => await bootcampRepository.AddToLearning(id).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await bootcampRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await bootcampRepository.Delete(ids).ToJsonResultAsync();
    }
}
