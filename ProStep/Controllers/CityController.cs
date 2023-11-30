using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.General.Category;
using ProStep.DataTransferObject.General.City;
using ProStep.General.CityRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityRepository cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            this.cityRepository = cityRepository;
        }

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => await cityRepository.GetAll().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await cityRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddCityDto cityDto)
            => await cityRepository.Add(cityDto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCityDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateCityDto cityDto) 
            => await cityRepository.Update(cityDto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await cityRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await cityRepository.Delete(ids).ToJsonResultAsync();
    }
}
