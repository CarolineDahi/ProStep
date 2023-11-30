using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.General.CommonQues;
using ProStep.DataTransferObject.General.Country;
using ProStep.General.CommonQuestionRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommonQuestionController : ControllerBase
    {
        private readonly ICommonQuesRepository commonQuesRepository;

        public CommonQuestionController(ICommonQuesRepository commonQuesRepository)
        {
            this.commonQuesRepository = commonQuesRepository;
        }

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCommonQuesDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => await commonQuesRepository.GetAll().ToJsonResultAsync();

        //[HttpGet(GlobalValue.RouteDash)]
        ////[ProStepAuthorize(ProStepRole.DashUser)]
        //[ProducesResponseType(typeof(GetCommonQuesDto), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetById([Required] Guid id)
        //    => await commonQuesRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCommonQuesDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddCommonQuesDto quesDto)
            => await commonQuesRepository.Add(quesDto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCommonQuesDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateCommonQuesDto quesDto)
            => await commonQuesRepository.Update(quesDto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await commonQuesRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await commonQuesRepository.Delete(ids).ToJsonResultAsync();

    }
}
