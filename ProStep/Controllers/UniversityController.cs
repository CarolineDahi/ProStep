using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.General.University;
using ProStep.General.UniversityRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository universityRepository;

        public UniversityController(IUniversityRepository universityRepository)
        {
            this.universityRepository = universityRepository;
        }

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetUniversityDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll() 
            => await universityRepository.GetAll().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetUniversityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await universityRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetUniversityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddUniversityDto universityDto)
            => await universityRepository.Add(universityDto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetUniversityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateUniversityDto universityDto)
            => await universityRepository.Update(universityDto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await universityRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await universityRepository.Delete(ids).ToJsonResultAsync();
    }
}
