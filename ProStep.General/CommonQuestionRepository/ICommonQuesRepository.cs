using ProStep.DataTransferObject.General.CommonQues;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.CommonQuestionRepository
{
    public interface ICommonQuesRepository
    {
        Task<OperationResult<IEnumerable<GetCommonQuesDto>>> GetAll();
        Task<OperationResult<GetCommonQuesDto>> GetById(Guid id);
        Task<OperationResult<GetCommonQuesDto>> Add(AddCommonQuesDto quesDto);
        Task<OperationResult<GetCommonQuesDto>> Update(UpdateCommonQuesDto quesDto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);
    }
}
