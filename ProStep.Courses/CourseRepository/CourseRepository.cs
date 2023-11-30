using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.Courses.Course;
using ProStep.DataTransferObject.Courses.Lecture;
using ProStep.DataTransferObject.Courses.Section;
using ProStep.DataTransferObject.General.Category;
using ProStep.DataTransferObject.Quizzes.Answer;
using ProStep.DataTransferObject.Quizzes.Question;
using ProStep.DataTransferObject.Quizzes.Quiz;
using ProStep.DataTransferObject.Shared.File;
using ProStep.Model.Courses;
using ProStep.Model.Quizzes;
using ProStep.Shared.FileRepository;
using ProStep.SharedKernel.ExtensionMethods;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Courses.CourseRepository
{
    public class CourseRepository : ProStepRepository, ICourseRepository
    {
        private readonly IFileRepository fileRepository;

        public CourseRepository(ProStepDBContext context, IFileRepository fileRepository)  : base(context)
        {
            this.fileRepository = fileRepository;
        }

        public async Task<OperationResult<IEnumerable<GetCourseDto>>> GetAll(Guid? categoryId)
        {
            var courses = await context.Courses
                                       .Where(c => !categoryId.HasValue ? true :
                                                 c.CourseCategories.Select(cc => cc.CategoryId).Contains(categoryId.Value))
                                       .Select(c => new GetCourseDto
                                       {
                                           Id = c.Id,
                                           Title = c.Title,
                                           CoachId = c.CoachId,
                                           CoachName = c.Coach.FirstName + " " + c.Coach.LastName,
                                           ImagePath = c.CourseFiles.Select(f => f.File.Path)
                                                                    .FirstOrDefault(),
                                           NumOfViews = c.CourseUsers!.Count(),
                                           Evaluation = c.CourseUsers.Where(c => c.DateFinished.HasValue)
                                                                     .Sum(c => c.Evaluation.Value)
                                                         / (c.CourseUsers.Count(c => c.DateFinished.HasValue) != 0 ? c.CourseUsers.Count(c => c.DateFinished.HasValue) : 1.0)
                                       }).ToListAsync();

            return OperationR.SetSuccess(courses.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetCourseDto>>> GetDash(Guid? categoryId,Guid? coachId, string? name, DateTime? startDate, DateTime? endDate)
        {
            var courses = await context.Courses
                                       .Where(c => (!categoryId.HasValue ? true :
                                                 c.CourseCategories.Select(cc => cc.CategoryId).Contains(categoryId.Value))
                                                && (coachId.HasValue ? c.CoachId.Equals(coachId) : true)
                                                && (name != null ? c.Title.Contains(name) : true)
                                                && (startDate.HasValue ? c.DateCreated > startDate : true)
                                                && (endDate.HasValue ? c.DateCreated < endDate : true))
                                       .Select(c => new GetCourseDto
                                       {
                                           Id = c.Id,
                                           Title = c.Title,
                                           CoachId = c.CoachId,
                                           CoachName = c.Coach.FirstName + " " + c.Coach.LastName,
                                           ImagePath = c.CourseFiles.Select(f => f.File.Path)
                                                                    .FirstOrDefault(),
                                           NumOfViews = c.CourseUsers!.Count(),
                                           Evaluation = c.CourseUsers.Where(c => c.DateFinished.HasValue)
                                                                     .Sum(c => c.Evaluation.Value)
                                                         / (c.CourseUsers.Count(c => c.DateFinished.HasValue) != 0 ? c.CourseUsers.Count(c => c.DateFinished.HasValue) : 1.0)
                                       }).ToListAsync();

            return OperationR.SetSuccess(courses.AsEnumerable());
        }

        public async Task<OperationResult<DetailsCourseDto>> GetById(Guid id)
        {
            var course = await context.Courses
                                      .Where(c => c.Id.Equals(id))
                                      .Select(c => new DetailsCourseDto
                                      {
                                          Id = c.Id,
                                          Title = c.Title,
                                          Description = c.Description,
                                          IsFavourite = c.CourseUsers!.Where(bu => bu.UserId == context.CurrentUserId()).FirstOrDefault().IsFavourite,

                                          LastUpdated = c.DateUpdated.HasValue ? c.DateUpdated.Value.Date
                                                                               : c.DateCreated.Date,
                                          CoachId = c.CoachId,
                                          CoachName = c.Coach.FirstName + " " + c.Coach.LastName,
                                          ImagePath = c.CourseFiles!.Select(cf => cf.File.Path)
                                                                   .FirstOrDefault(),
                                          Requirements = c.Requirements.MyList(),
                                          Targets = c.Target.MyList(),
                                          NumOfViews = c.CourseUsers!.Count(),
                                          Evaluation = c.CourseUsers!.Where(c => c.DateFinished.HasValue)
                                                                    .Sum(c => c.Evaluation.Value)
                                                     / (c.CourseUsers!.Count(c => c.DateFinished.HasValue) != 0
                                                                      ? c.CourseUsers!.Count(c => c.DateFinished.HasValue)
                                                                      : 1.0),
                                          SubCategories = c.CourseCategories!
                                                        .Select(cc => new GetCategoryDto
                                                        {
                                                            Id = cc.CategoryId,
                                                            Name = cc.Category.Name,
                                                        }).ToList(),
                                          MainCategories = c.CourseCategories!
                                                            .Where(mc => mc.Category.ParentCategory != null)
                                                            .Select(cc => new GetCategoryDto
                                                            {
                                                                Id = cc.Category.ParentCategoryId.Value,
                                                                Name = cc.Category.ParentCategory.Name
                                                            }).ToList(),
                                          Sections = c.Sections!
                                                      .Select(s => new GetSectionDto
                                                      {
                                                          Id = s.Id,
                                                          Title = s.Title,
                                                          Lectures = s.Lectures!
                                                                      .Select(l => new GetLectureDto
                                                                      {
                                                                          Id = l.Id,
                                                                          Title = l.Title,
                                                                          Description = l.Description,
                                                                          Video = l.LectureFiles!
                                                                                   .Select(f => new GetFileDto
                                                                                   {
                                                                                       Id = f.FileId,
                                                                                       Name = f.File.Name,
                                                                                       Path = f.File.Path,
                                                                                       Type = f.File.Type,
                                                                                   }).First()
                                                                      }).ToList(),
                                                          QuizId = s.QuizId,
                                                      }).ToList(),
                                          
                                      }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(course);
        }

        public async Task<OperationResult<DetailsCourseDto>> Add(AddCourseDto dto)
        {
            Course course = new()
            {
                CoachId = context.CurrentUserId(),
                Title = dto.Title,
                Description = dto.Description,
                Requirements = dto.Requirements.MyString(),
                Target = dto.Targets.MyString(),
                CourseCategories = dto.CategoryIds.ToList().Select(id => new CourseCategory()
                {
                    CategoryId = id,
                }).ToList()
            };
            context.Add(course);
            await context.SaveChangesAsync();
            var files = await fileRepository.Add("Courses",new List<IFormFile> { dto.Image });

            files.Result.ForEach(f => context.Add( new CourseFile()
            {
                CourseId = course.Id,
                FileId = f.Id,
            }));

            foreach(var sec in dto.Sections)
            {
                var videos = await fileRepository.Add("Lectures", sec.Lectures.Select(l => l.Video).ToList()).ConfigureAwait(false);
                //(await fileRepository.Add("Lectures", sec.Lectures.Select(l => l.Video).ToList())).Result;
                var lectureFiles = videos.Result.Select(v => new LectureFile()
                {
                    FileId = v.Id,
                }).ToList();

                //var section = ;
                context.Add(new Section()
                {
                    Title = sec.Title,
                    CourseId = course.Id,
                    Lectures = sec.Lectures.Select(l => new Lecture()
                    {
                        Title = l.Title,
                        Description = l.Description,
                        LectureFiles = lectureFiles
                    }).ToList(),
                    Quiz = new Quiz()
                    {
                        Questions = sec.Quiz?.Questions.Select(q => new Question()
                        {
                            Text = q.Text,
                            Answer = q.Answer,
                            Answers = q.Answers?.Select(a => new Answer()
                            {
                                Text = a.Text,
                                IsCorrect = a.IsCorrect,
                            }).ToList(),
                        }).ToList(),
                    }
                });
            }

            await context.SaveChangesAsync();
            return OperationR.SetSuccess((await GetById(course.Id)).Result);
        }

        public async Task<OperationResult<DetailsCourseDto>> Update(UpdateCourseDto courseDto)
        {
            return null;// OperationR.SetSuccess((await GetById(courseDto.Id)).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var courses = await context.Courses
                                       .Where(c => ids.Contains(c.Id))
                                       .Include(c => c.CourseFiles)
                                       .Include(c => c.BootcampCourses)
                                       .Include(c => c.CourseCategories)
                                       .Include(c => c.Sections)
                                       .ThenInclude(s => s.Lectures)
                                       .ThenInclude(l => l.LectureFiles)
                                       .Include(c => c.Sections)
                                       .ThenInclude(s => s.Lectures)
                                       .Include(c => c.Sections)
                                       .ThenInclude(s => s.Quiz)
                                       .Include(c => c.CourseUsers)
                                       .ToListAsync();
            foreach (var course in courses)
            {
                var res = await fileRepository.Delete(course.CourseFiles.Select(f => f.FileId).ToList());
                context.RemoveRange(course.CourseFiles);
                context.RemoveRange(course.BootcampCourses);
                context.RemoveRange(course.CourseCategories);
                context.RemoveRange(course.Sections.SelectMany(s => s.Lectures).ToList());
                context.RemoveRange(course.Sections.Select(s => s.Quiz).ToList());
                context.RemoveRange(course.Sections);
                context.RemoveRange(course.CourseUsers);
            }
            context.RemoveRange(courses);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> AddToFavourite(Guid id)
        {
            var coures = await context.CourseUsers
                                      .Where(c => c.CourseId == id 
                                               && c.UserId.Equals(context.CurrentUserId()))
                                      .SingleOrDefaultAsync();
            if(coures is null)
            {
                context.CourseUsers.Add(new CourseUser
                {
                    CourseId = id,
                    UserId = context.CurrentUserId(),
                    IsFavourite = true
                });
            }
            else
            {
                coures.IsFavourite = true;
                context.Update(coures);
            }
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> RemoveFromFavourite(Guid id)
        {
            var coures = await context.CourseUsers
                                      .Where(c => c.CourseId == id
                                               && c.UserId.Equals(context.CurrentUserId()))
                                      .SingleOrDefaultAsync();
            
            coures.IsFavourite = false;
            context.Update(coures);
            
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> AddToLearning(Guid id)
        {
            context.CourseUsers.Add(new CourseUser
            {
                CourseId = id,
                UserId = context.CurrentUserId(),
            });
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> WatchLecture(Guid id)
        {
            var courseUserId = await context.Lectures.Where(l => l.Id.Equals(id))
                                                .Select(l => l.Section.Course.CourseUsers
                                                                      .Where(c => c.UserId.Equals(context.CurrentUserId()))
                                                                      .Select(c => c.Id)
                                                                      .FirstOrDefault())
                                                .SingleOrDefaultAsync();
            context.Add(new LectureCourseUser
            {
                CourseUserId = courseUserId,
                LectureId = id,
            });
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> EvaluateCourse(AddEvaluateDto dto)
        {
            var course = await context.CourseUsers
                                      .Where(c => c.CourseId.Equals(dto.CourseId)
                                               && c.UserId.Equals(context.CurrentUserId()))
                                      .SingleOrDefaultAsync();
            course.Evaluation = dto.Value;
            context.Update(course);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<IEnumerable<GetCourseDto>>> GetRecommendrd()
        {
            var courses = await context.Courses
                                       .OrderByDescending(c => c.CourseUsers.Where(c => c.DateFinished.HasValue)
                                                                            .Sum(c => c.Evaluation.Value)
                                                            / (c.CourseUsers!.Count(c => c.DateFinished.HasValue) != 0
                                                                             ? c.CourseUsers.Count(c => c.DateFinished.HasValue)
                                                                             : 1.0))
                                       .Take(10)
                                       .Select(c => new GetCourseDto
                                       {
                                           Id = c.Id,
                                           Title = c.Title,
                                           CoachId = c.CoachId,
                                           CoachName = c.Coach.FirstName + " " + c.Coach.LastName,
                                           ImagePath = c.CourseFiles
                                                        .Select(f => f.File.Path)
                                                        .FirstOrDefault(),
                                           NumOfViews = c.CourseUsers.Count(),
                                           Evaluation = c.CourseUsers
                                                         .Where(c => c.DateFinished.HasValue)
                                                         .Sum(c => c.Evaluation.Value)
                                                                / (c.CourseUsers!.Count(c => c.DateFinished.HasValue) != 0
                                                                                 ? c.CourseUsers.Count(c => c.DateFinished.HasValue)
                                                                                 : 1.0)
                                       }).ToListAsync();

            return OperationR.SetSuccess(courses.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetCourseDto>>> GetFavorite()
        {
            var courses = await context.Courses
                                       .Where(b => b.CourseUsers.Any(u => u.UserId == context.CurrentUserId()
                                                                       && u.IsFavourite == true))
                                       //.OrderByDescending(c => c.CourseUsers.Where(c => c.DateFinished.HasValue)
                                       //                                     .Sum(c => c.Evaluation.Value)
                                       //                     / (c.CourseUsers!.Count(c => c.DateFinished.HasValue) != 0
                                       //                                      ? c.CourseUsers.Count(c => c.DateFinished.HasValue)
                                       //                                      : 1.0))
                                       .Select(c => new GetCourseDto
                                       {
                                           Id = c.Id,
                                           Title = c.Title,
                                           CoachId = c.CoachId,
                                           CoachName = c.Coach.FirstName + " " + c.Coach.LastName,
                                           ImagePath = c.CourseFiles
                                                        .Select(f => f.File.Path)
                                                        .FirstOrDefault(),
                                           NumOfViews = c.CourseUsers!.Count(),
                                           Evaluation = c.CourseUsers!
                                                         .Where(c => c.DateFinished.HasValue)
                                                         .Sum(c => c.Evaluation.Value)
                                                                / (c.CourseUsers!.Count(c => c.DateFinished.HasValue) != 0
                                                                                 ? c.CourseUsers.Count(c => c.DateFinished.HasValue)
                                                                                 : 1.0)
                                       }).ToListAsync();

            return OperationR.SetSuccess(courses.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetCourseDto>>> GetMyLearning()
        {
            var courses = await context.Courses
                                       .Where(b => b.CourseUsers.Any(u => u.UserId == context.CurrentUserId()))
                                       //.OrderByDescending(c => c.CourseUsers.Where(c => c.DateFinished.HasValue)
                                       //                                     .Sum(c => c.Evaluation.Value)
                                       //                     / (c.CourseUsers!.Count(c => c.DateFinished.HasValue) != 0
                                       //                                      ? c.CourseUsers.Count(c => c.DateFinished.HasValue)
                                       //                                      : 1.0))
                                       .Select(c => new GetCourseDto
                                       {
                                           Id = c.Id,
                                           Title = c.Title,
                                           CoachId = c.CoachId,
                                           CoachName = c.Coach.FirstName + " " + c.Coach.LastName,
                                           ImagePath = c.CourseFiles
                                                        .Select(f => f.File.Path)
                                                        .FirstOrDefault(),
                                           NumOfViews = c.CourseUsers!.Count(),
                                           Evaluation = c.CourseUsers!
                                                         .Where(c => c.DateFinished.HasValue)
                                                         .Sum(c => c.Evaluation.Value)
                                                                / (c.CourseUsers!.Count(c => c.DateFinished.HasValue) != 0
                                                                                 ? c.CourseUsers.Count(c => c.DateFinished.HasValue)
                                                                                 : 1.0)
                                       }).ToListAsync();

            return OperationR.SetSuccess(courses.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetCourseDto>>> GetByCoachId(Guid userId)
        {
            var courses = await context.Courses
                                       .Where(c => c.CoachId.Equals(userId))
                                       .Select(c => new GetCourseDto
                                       {
                                           Id = c.Id,
                                           Title = c.Title,
                                           CoachId = c.CoachId,
                                           CoachName = c.Coach.FirstName + " " + c.Coach.LastName,
                                           ImagePath = c.CourseFiles.Select(cf => cf.File.Path).FirstOrDefault(),
                                           NumOfViews = c.CourseUsers!.Count(),
                                           Evaluation = c.CourseUsers!
                                                         .Where(c => c.DateFinished.HasValue)
                                                         .Sum(c => c.Evaluation.Value)
                                                                / (c.CourseUsers!.Count(c => c.DateFinished.HasValue) != 0
                                                                                 ? c.CourseUsers.Count(c => c.DateFinished.HasValue)
                                                                                 : 1.0)
                                       }).ToListAsync();
            return OperationR.SetSuccess(courses.AsEnumerable());
        }

        public async Task<OperationResult<GetQuizDto>> GetQuiz(Guid sectionId)
        {
            var quiz = await context.Quizzes.Where(q => q.SectionId.Equals(sectionId))
                                            .Select(q => new GetQuizDto
                                            {
                                                Id = q.SectionId,
                                                Quiz = q.Questions.Select(ques => new GetQuestionDto
                                                {
                                                    Id = ques.Id,
                                                    Text = ques.Text,
                                                    Type = ques.Type,
                                                    Answer = ques.Answer,
                                                    Answers = ques.Answers.Select(ans => new GetAnswerDto
                                                    {
                                                        Id = ans.Id,
                                                        Text = ans.Text
                                                    }).ToList(),
                                                }).ToList()
                                            }).SingleOrDefaultAsync();

            return OperationR.SetSuccess(quiz);
        }

        public async Task<OperationResult<double>> SolveQuiz(QuizDto dto)
        {
            var quiz = await context.Quizzes.Where(q => q.Id == dto.QuizId)
                                            .Include(q => q.Questions)
                                            .ThenInclude(q => q.Answers)
                                            .FirstOrDefaultAsync();
            double mark = 0;
            foreach (var ques in dto.Questions)
            {
                context.Add(new UserQuesAnswer
                {
                    QuestionId = ques.QuestionId,
                    AnswerId = ques.AnswerId,
                    UserId = context.CurrentUserId()
                });

                var curr = quiz.Questions.Where(q => q.Id == ques.QuestionId).SingleOrDefault();
                if(curr.Answers.Where(ans => ans.IsCorrect).Select(ans => ans.Id).FirstOrDefault() == ques.AnswerId)
                {
                    mark++;
                }
            }
            return OperationR.SetSuccess((mark/dto.Questions.Count())*100);

        }
    }
}
