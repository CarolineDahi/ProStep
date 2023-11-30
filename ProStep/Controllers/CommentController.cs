using Microsoft.AspNetCore.Mvc;
using ProStep.Base;
using ProStep.Courses.CommentRepository;
using ProStep.DataTransferObject.Courses.Comment;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System.ComponentModel.DataAnnotations;

namespace ProStep.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        [HttpGet(GlobalValue.RouteBoth)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        public async Task<IActionResult> GetByVideoId([Required] Guid videoId)
            => await commentRepository.GetByVideoId(videoId).ToJsonResultAsync();

        [HttpPost(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        public async Task<IActionResult> Add(AddCommentDto commentDto)
            => await commentRepository.Add(commentDto).ToJsonResultAsync();

        [HttpPut(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.User)]
        public async Task<IActionResult> Update(UpdateCommentDto commentDto)
            => await commentRepository.Update(commentDto).ToJsonResultAsync();

        [HttpDelete(GlobalValue.RouteApp)]
        [ProStepAuthorize(ProStepRole.DashUser, ProStepRole.User)]
        public async Task<IActionResult> Delete([Required] Guid id)
           => await commentRepository.Delete(id).ToJsonResultAsync();
    }
}
