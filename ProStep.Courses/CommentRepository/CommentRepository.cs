using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.Courses.Comment;
using ProStep.Model.Courses;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Courses.CommentRepository
{
    public class CommentRepository : ProStepRepository, ICommentRepository
    {
        public CommentRepository(ProStepDBContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<GetCommentDto>>> GetByVideoId(Guid videoId)
        {
            var comments = await context.Comments.Where(c => c.VideoId.Equals(videoId)
                                                          && !c.MainCommentId.HasValue)
                                                 .Select(c => new GetCommentDto
                                                 {
                                                     Id = c.Id,
                                                     Text = c.Text,
                                                     UserId = c.UserId,
                                                     UserName = c.User.FirstName + " " + c.User.LastName,
                                                     ImagePath = c.User.ImagePath,
                                                     NumOfReply = c.SubComments.Count(),
                                                     IsUpdated = c.DateUpdated.HasValue
                                                 }).ToListAsync();
            return OperationR.SetSuccess(comments.AsEnumerable());
        }

        public async Task<OperationResult<bool>> Add(AddCommentDto commentDto)
        {
            var comment = new Comment
            {
                Text = commentDto.Text,
                VideoId = commentDto.VideoId,
                MainCommentId = commentDto.MainCommentId,
                UserId = context.CurrentUserId()
            };
            context.Add(comment);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> Update(UpdateCommentDto commentDto)
        {
            var comment = await context.Comments
                                       .Where(c => c.Id.Equals(commentDto.Id))
                                       .SingleOrDefaultAsync();
            if(comment.UserId != context.CurrentUserId())
            {
                return OperationR.SetFailed<bool>("You can not delete this comment", OperationResultType.Unauthorized);
            }
            comment.Text = commentDto.Text;
            context.Update(comment);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> Delete(Guid id)
        {
            var comment = await context.Comments
                                       .Where(c => c.Id.Equals(id))
                                       .Include(c => c.SubComments)
                                       .SingleOrDefaultAsync();
            if(comment.UserId != context.CurrentUserId())
            {
                return OperationR.SetFailed<bool>("You can not delete this comment", OperationResultType.Unauthorized);
            }
            context.RemoveRange(comment.SubComments);
            context.Remove(comment);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }
    }
}
