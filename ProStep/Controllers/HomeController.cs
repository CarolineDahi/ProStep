using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.Courses.BootcampRepository;
using ProStep.Courses.CourseRepository;
using ProStep.DataTransferObject.Courses.Bootcamp;
using ProStep.DataTransferObject.Courses.Course;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;

namespace ProStep.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly IBootcampRepository bootcampRepository;

        public HomeController(ICourseRepository courseRepository, IBootcampRepository bootcampRepository)
        {
            this.courseRepository = courseRepository;
            this.bootcampRepository = bootcampRepository;
        }

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCourseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCorses()
            => await courseRepository.GetRecommendrd().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetBootcampDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBootcamps()
            => await bootcampRepository.GetRecommendrd().ToJsonResultAsync();

        //public async Task<IActionResult> GetHomeFilter(Guid? )
        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCourseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFavoriteCorses()
            => await courseRepository.GetFavorite().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetBootcampDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFavoriteBootcamps()
            => await bootcampRepository.GetFavorite().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCourseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyLearningCorses()
           => await courseRepository.GetMyLearning().ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetBootcampDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyLearningBootcamps()
            => await bootcampRepository.GetMyLearning().ToJsonResultAsync();
    }
}
