using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.General.City;
using ProStep.DataTransferObject.General.Country;
using ProStep.General.CountryRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCountryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => await countryRepository.GetAll().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCountryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await countryRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCountryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddCountryDto countryDto)
            => await countryRepository.Add(countryDto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCountryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateCountryDto countryDto)
            => await countryRepository.Update(countryDto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await countryRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await countryRepository.Delete(ids).ToJsonResultAsync();
    }
}
