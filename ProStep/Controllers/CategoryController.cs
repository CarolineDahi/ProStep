using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.DataTransferObject.General.Category;
using ProStep.DataTransferObject.Security.Account;
using ProStep.General.CategoryRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(IEnumerable<GetCategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
            => await categoryRepository.GetAll().ToJsonResultAsync();


        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await categoryRepository.GetById(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add(AddCategoryDto categoryDto)
            => await categoryRepository.Add(categoryDto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(GetCategoryDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdateCategoryDto categoryDto)
            => await categoryRepository.Update(categoryDto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await categoryRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await categoryRepository.Delete(ids).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<GetCategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMainCategories()
            => await categoryRepository.GetMain().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<GetCategoryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubCategories()
            => await categoryRepository.GetSub().ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChooseMyCategories(IEnumerable<Guid> categoryIds)
            => await categoryRepository.ChooseMyCategories(categoryIds).ToJsonResultAsync();
    }
}
