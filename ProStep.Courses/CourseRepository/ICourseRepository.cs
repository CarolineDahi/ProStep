using ProStep.DataTransferObject.Courses.Bootcamp;
using ProStep.DataTransferObject.Courses.Course;
using ProStep.DataTransferObject.Quizzes.Quiz;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Courses.CourseRepository
{
    public interface ICourseRepository
    {
        Task<OperationResult<IEnumerable<GetCourseDto>>> GetAll(Guid? categoryId);
        Task<OperationResult<DetailsCourseDto>> GetById(Guid id);
        Task<OperationResult<IEnumerable<GetCourseDto>>> GetDash(Guid? categoryId, Guid? coachId,string? name, DateTime? startDate, DateTime? endDate);

        Task<OperationResult<DetailsCourseDto>> Add(AddCourseDto courseDto);
        Task<OperationResult<DetailsCourseDto>> Update(UpdateCourseDto courseDto);
        Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids);

        Task<OperationResult<bool>> AddToFavourite(Guid id);
        Task<OperationResult<bool>> RemoveFromFavourite(Guid id);
        Task<OperationResult<bool>> AddToLearning(Guid id);
        Task<OperationResult<bool>> WatchLecture(Guid id);
        Task<OperationResult<bool>> EvaluateCourse(AddEvaluateDto dto);

        Task<OperationResult<IEnumerable<GetCourseDto>>> GetRecommendrd();
        Task<OperationResult<IEnumerable<GetCourseDto>>> GetFavorite();
        Task<OperationResult<IEnumerable<GetCourseDto>>> GetMyLearning();

        Task<OperationResult<IEnumerable<GetCourseDto>>> GetByCoachId(Guid userId);

        Task<OperationResult<GetQuizDto>> GetQuiz(Guid sectionId);
        Task<OperationResult<double>> SolveQuiz(QuizDto dto); 
    }
}
