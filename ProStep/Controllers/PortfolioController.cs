using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.Courses.BootcampRepository;
using ProStep.Courses.CourseRepository;
using ProStep.DataTransferObject.Courses.Bootcamp;
using ProStep.DataTransferObject.General.Category;
using ProStep.DataTransferObject.Profile.Portfolio;
using ProStep.DataTransferObject.Shared.File;
using ProStep.Profile.PortfolioRepository;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class PortfolioController : Controller
    {
        private readonly IPortfolioRepository portfolioRepository;
        private readonly IBootcampRepository bootcampRepository;
        private readonly ICourseRepository courseRepository;

        public PortfolioController(IPortfolioRepository portfolioRepository, 
                                   IBootcampRepository bootcampRepository,
                                   ICourseRepository courseRepository)
        {
            this.portfolioRepository = portfolioRepository;
            this.bootcampRepository = bootcampRepository;
            this.courseRepository = courseRepository;
        }

        

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.User, ProStepRole.Coach)]
        [ProducesResponseType(typeof(GetPortfolioDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyPortfolio() 
            => await portfolioRepository.GetMyPortfolio().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.User, ProStepRole.Coach)]
        [ProducesResponseType(typeof(GetMyPortfolioDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPortfolio([Required] Guid id)
            => await portfolioRepository.GetPortfolio(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User, ProStepRole.Coach)]
        [ProducesResponseType(typeof(GetMyPortfolioDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadMyImage(IFormFile file)
            => await portfolioRepository.UploadMyImage(file).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User, ProStepRole.Coach)]
        [ProducesResponseType(typeof(GetMyPortfolioDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadMyCover(IFormFile file)
            => await portfolioRepository.UploadMyCover(file).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(GetMyPortfolioDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(UpdatePortfolioDto dto)
            => await portfolioRepository.UpateMyPortfolio(dto).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMyCareers(UpdateDto dto)
            => await portfolioRepository.UpdateMyCareers(dto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMySkills(UpdateDto dto)
            => await portfolioRepository.UpdateMySkills(dto).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetFileDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCertificates(Guid? id)
            => await portfolioRepository.GetCertificates(id).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateMyCertificates([FromForm] UpdateCertificateDto dto)
            => await portfolioRepository.UpdateMyCertificates(dto).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetBootcampDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBootcampByCoachId([Required] Guid id)
            => await bootcampRepository.GetByCoachId(id).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetBootcampDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCourseByCoachId([Required] Guid id)
            => await courseRepository.GetByCoachId(id).ToJsonResultAsync();
    }
}
