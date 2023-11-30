using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.Courses.BootcampRepository;
using ProStep.Courses.CourseRepository;
using ProStep.DataTransferObject.Courses.Course;
using ProStep.DataTransferObject.Quizzes.Quiz;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCourseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(Guid? categoryId)
            => await courseRepository.GetAll(categoryId).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(DetailsCourseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([Required] Guid id)
            => await courseRepository.GetById(id).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        [ProducesResponseType(typeof(IEnumerable<GetCourseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDAsh(Guid? categoryId,Guid? coachId, string? name, DateTime? startDate, DateTime? endDate)
            => await courseRepository.GetDash(categoryId,coachId, name, startDate, endDate).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.ActiveCoach, ProStepRole.User)]
        [ProducesResponseType(typeof(GetCourseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromForm] AddCourseDto courseDto)
            => await courseRepository.Add(courseDto).ToJsonResultAsync();


        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToFavourite([Required] Guid id)
            => await courseRepository.AddToFavourite(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveFromFavourite([Required] Guid id)
           => await courseRepository.RemoveFromFavourite(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddToLearning([Required] Guid id)
            => await courseRepository.AddToLearning(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> WatchLecture([Required] Guid id)
            => await courseRepository.WatchLecture(id).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> EvaluateCourse(AddEvaluateDto dto)
            => await courseRepository.EvaluateCourse(dto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([Required] Guid id)
            => await courseRepository.Delete(new List<Guid> { id }).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteDash)]
        [ProStepAuthorize(ProStepRole.DashUser)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteRange(IEnumerable<Guid> ids)
            => await courseRepository.Delete(ids).ToJsonResultAsync();

        [HttpGet(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        [ProducesResponseType(typeof(GetQuizDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetQuiz(Guid sectionId)
    => await courseRepository.GetQuiz(sectionId).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        public async Task<IActionResult> SolveQuiz(QuizDto dto)
            => await courseRepository.SolveQuiz(dto).ToJsonResultAsync();
    }
}
