using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.General.Country;
using ProStep.DataTransferObject.General.Faculty;
using ProStep.General.FacultyRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyRepository facultyRepository;

        public FacultyController(IFacultyRepository facultyRepository)
        {
            this.facultyRepository = facultyRepository;
        }

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetFacultyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => await facultyRepository.GetAll().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetFacultyDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await facultyRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetFacultyDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddFacultyDto facultyDto)
            => await facultyRepository.Add(facultyDto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetFacultyDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateFacultyDto facultyDto)
            => await facultyRepository.Update(facultyDto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await facultyRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await facultyRepository.Delete(ids).ToJsonResultAsync();
    }
}
