using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.CommonQues;
using ProStep.Model.General;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.CommonQuestionRepository
{
    public class CommonQuesRepository : ProStepRepository, ICommonQuesRepository
    {
        public CommonQuesRepository(ProStepDBContext context) : base(context)
        {
        }
        public async Task<OperationResult<IEnumerable<GetCommonQuesDto>>> GetAll()
        {
            var Questions = await context.CommonQuestions.Select(c => new GetCommonQuesDto
            {
                Id = c.Id,
                Answer = c.Answer,
                Question = c.Question,
            }).ToListAsync();

            return OperationR.SetSuccess(Questions.AsEnumerable());
        }

        public async Task<OperationResult<GetCommonQuesDto>> GetById(Guid id)
        {
            var ques = await context.CommonQuestions.Where(c => c.Id.Equals(id))
                                                    .Select(c => new GetCommonQuesDto
                                                    {
                                                        Id = c.Id,
                                                        Question = c.Question,
                                                        Answer = c.Answer,
                                                    }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(ques);
        }

        public async Task<OperationResult<GetCommonQuesDto>> Add(AddCommonQuesDto quesDto)
        {
            var ques = new CommonQuestion
            {
                Question = quesDto.Question, 
                Answer = quesDto.Answer,
            };
            context.Add(ques);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(ques.Id)).Result);
        }

        public async Task<OperationResult<GetCommonQuesDto>> Update(UpdateCommonQuesDto quesDto)
        {
            var ques = await context.CommonQuestions.Where(c => c.Id.Equals(quesDto.Id)).SingleOrDefaultAsync();
            ques.Question = quesDto.Question;
            ques.Answer = quesDto.Answer;

            context.Update(ques);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(ques.Id)).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var ques = await context.CommonQuestions.Where(c => ids.Contains(c.Id)).ToListAsync();
            context.RemoveRange(ques);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }
    }
}
