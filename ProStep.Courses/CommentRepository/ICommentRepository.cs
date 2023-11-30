using ProStep.DataTransferObject.Courses.Comment;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Courses.CommentRepository
{
    public interface ICommentRepository
    {
        Task<OperationResult<IEnumerable<GetCommentDto>>> GetByVideoId(Guid videoId);
        Task<OperationResult<bool>> Add(AddCommentDto commentDto);
        Task<OperationResult<bool>> Update(UpdateCommentDto commentDto);
        Task<OperationResult<bool>> Delete(Guid id);
    }
}
