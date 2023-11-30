using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.Courses.Bootcamp;
using ProStep.DataTransferObject.Courses.Course;
using ProStep.DataTransferObject.General.Category;
using ProStep.Model.Courses;
using ProStep.Model.General;
using ProStep.Shared.FileRepository;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Courses.BootcampRepository
{
    public class BootcampRepository : ProStepRepository, IBootcampRepository
    {
        private readonly IFileRepository fileRepository;

        public BootcampRepository(ProStepDBContext context, IFileRepository fileRepository) : base(context)
        {
            this.fileRepository = fileRepository;
        }

        public async Task<OperationResult<IEnumerable<GetBootcampDto>>> GetAll(Guid? categoryId)
        {
            var camps = await context.Bootcamps
                                     .Where(b => !categoryId.HasValue ? true :
                                                 b.BootcampCategories.Select(bc => bc.CategoryId).Contains(categoryId.Value))
                                     .Select(b => new GetBootcampDto
                                     {
                                         Id = b.Id,
                                         Title = b.Title,
                                         ImagePath = b.BootcampFiles
                                                      .Select(b => b.File.Path)
                                                      .FirstOrDefault(),
                                         Evaluation = b.BootcampUsers
                                                       .Where(b => b.DateFinished.HasValue)
                                                       .Sum(b => b.Evaluation.Value)
                                                       / (b.BootcampUsers
                                                          .Count(b => b.DateFinished.HasValue)
                                                          != 0 ? b.BootcampUsers.Count(c => c.DateFinished.HasValue) : 1.0),
                                         NumOfViews = b.BootcampUsers.Count(),
                                     }).ToListAsync();
            return OperationR.SetSuccess(camps.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetBootcampDto>>> GetDash(Guid? categoryId, string? name, DateTime? startDate, DateTime? endDate)
        {
            var camps = await context.Bootcamps
                                     .Where(b => (!categoryId.HasValue ? true :
                                                 b.BootcampCategories.Select(bc => bc.CategoryId).Contains(categoryId.Value))
                                              && (name != null ? b.Title.Contains(name) : true)
                                              && (startDate.HasValue ? b.DateCreated > startDate : true)
                                              && (endDate.HasValue ? b.DateCreated < endDate : true)
                                              )
                                     .Select(b => new GetBootcampDto
                                     {
                                         Id = b.Id,
                                         Title = b.Title,
                                         ImagePath = b.BootcampFiles
                                                      .Select(b => b.File.Path)
                                                      .FirstOrDefault(),
                                         Evaluation = b.BootcampUsers
                                                       .Where(b => b.DateFinished.HasValue)
                                                       .Sum(b => b.Evaluation.Value)
                                                       / (b.BootcampUsers
                                                          .Count(b => b.DateFinished.HasValue)
                                                          != 0 ? b.BootcampUsers.Count(c => c.DateFinished.HasValue) : 1.0),
                                         NumOfViews = b.BootcampUsers.Count(),
                                     }).ToListAsync();
            return OperationR.SetSuccess(camps.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetBootcampDto>>> GetRecommendrd()
        {
            var camps = await context.Bootcamps
                                     .OrderByDescending(b => b.BootcampUsers.Where(b => b.DateFinished.HasValue)
                                                                            .Sum(b => b.Evaluation.Value)
                                                          / (b.BootcampUsers.Count(b => b.DateFinished.HasValue) != 0 ? 
                                                             b.BootcampUsers.Count(c => c.DateFinished.HasValue) : 1.0))
                                     .Take(10)
                                     .Select(b => new GetBootcampDto
                                     {
                                         Id = b.Id,
                                         Title = b.Title,
                                         ImagePath = b.BootcampFiles
                                                      .Select(b => b.File.Path)
                                                      .FirstOrDefault(),
                                         Evaluation = b.BootcampUsers
                                                       .Where(b => b.DateFinished.HasValue)
                                                       .Sum(b => b.Evaluation.Value)
                                                       / (b.BootcampUsers.Count(b => b.DateFinished.HasValue) != 0 ?
                                                        b.BootcampUsers.Count(c => c.DateFinished.HasValue) : 1.0),
                                         NumOfViews = b.BootcampUsers.Count()
                                     }).ToListAsync();
            return OperationR.SetSuccess(camps.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetBootcampDto>>> GetFavorite()
        {
            var camps = await context.Bootcamps
                                     .Where(b => b.BootcampUsers.Any(u => u.UserId == context.CurrentUserId()
                                                                         && u.IsFavourite == true))
                                     //.OrderByDescending(b => b.BootcampUsers.Where(b => b.DateFinished.HasValue)
                                     //                                       .Sum(b => b.Evaluation.Value)
                                     //                     / (b.BootcampUsers.Count(b => b.DateFinished.HasValue) != 0 ?
                                     //                        b.BootcampUsers.Count(c => c.DateFinished.HasValue) : 1.0))
                                     .Select(b => new GetBootcampDto
                                     {
                                         Id = b.Id,
                                         Title = b.Title,
                                         ImagePath = b.BootcampFiles
                                                      .Select(b => b.File.Path)
                                                      .FirstOrDefault(),
                                         Evaluation = b.BootcampUsers!
                                                       .Where(b => b.DateFinished.HasValue)
                                                       .Sum(b => b.Evaluation.Value)
                                                       / (b.BootcampUsers!.Count(b => b.DateFinished.HasValue) != 0 ?
                                                        b.BootcampUsers!.Count(c => c.DateFinished.HasValue) : 1.0),
                                         NumOfViews = b.BootcampUsers!.Count()
                                     }).ToListAsync();
            return OperationR.SetSuccess(camps.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetBootcampDto>>> GetMyLearning()
        {
            var camps = await context.Bootcamps
                                     .Where(b => b.BootcampUsers.Any(u => u.UserId == context.CurrentUserId()))
                                     //.OrderByDescending(b => b.BootcampUsers.Where(b => b.DateFinished.HasValue)
                                     //                                       .Sum(b => b.Evaluation.Value)
                                     //                     / (b.BootcampUsers.Count(b => b.DateFinished.HasValue) != 0 ?
                                     //                        b.BootcampUsers.Count(c => c.DateFinished.HasValue) : 1.0))
                                     .Select(b => new GetBootcampDto
                                     {
                                         Id = b.Id,
                                         Title = b.Title,
                                         ImagePath = b.BootcampFiles
                                                      .Select(b => b.File.Path)
                                                      .FirstOrDefault(),
                                         Evaluation = b.BootcampUsers!
                                                       .Where(b => b.DateFinished.HasValue)
                                                       .Sum(b => b.Evaluation.Value)
                                                       / (b.BootcampUsers!.Count(b => b.DateFinished.HasValue) != 0 ?
                                                        b.BootcampUsers!.Count(c => c.DateFinished.HasValue) : 1.0),
                                         NumOfViews = b.BootcampUsers!.Count()
                                     }).ToListAsync();
            return OperationR.SetSuccess(camps.AsEnumerable());
        }

        public async Task<OperationResult<GetDetailsBootcampDto>> GetById(Guid id)
        {
            var camp = await context.Bootcamps
                                    .Where(b => b.Id.Equals(id))
                                    .Select(b => new GetDetailsBootcampDto
                                    {
                                        Id = b.Id,
                                        Title = b.Title,
                                        Description = b.Description,
                                        IsFavourite = b.BootcampUsers.Where(bu => bu.UserId== context.CurrentUserId()).FirstOrDefault().IsFavourite,
                                        ImagePath = b.BootcampFiles
                                                     .Select(b => b.File.Path)
                                                     .FirstOrDefault(),
                                        Evaluation = b.BootcampUsers
                                                      .Where(b => b.DateFinished.HasValue)
                                                      .Sum(b => b.Evaluation.Value)
                                                      / (b.BootcampUsers
                                                         .Count(b => b.DateFinished.HasValue)
                                                         != 0 ? b.BootcampUsers.Count(c => c.DateFinished.HasValue) : 1.0),
                                        NumOfViews = b.BootcampUsers!.Count(),
                                        CourseDtos = b.BootcampCourses
                                                      .Select(bc => new GetCourseDto
                                                      {
                                                          Id = bc.CourseId,
                                                          Title = bc.Course.Title,
                                                          CoachId = bc.Course.CoachId,
                                                          CoachName = bc.Course.Coach.FirstName + " " + bc.Course.Coach.LastName,
                                                          ImagePath = bc.Course.CourseFiles
                                                                               .Select(c => c.File.Path)
                                                                               .FirstOrDefault(),
                                                          Evaluation = bc.Course.CourseUsers
                                                                                .Where(c => c.DateFinished.HasValue)
                                                                                .Sum(c => c.Evaluation.Value)
                                                                                / (bc.Course.CourseUsers
                                                                                    .Count(b => b.DateFinished.HasValue)!= 0 
                                                                                    ? b.BootcampUsers.Count(c => c.DateFinished.HasValue) 
                                                                                    : 1.0),
                                                      }).ToList(),
                                        CategoryDtos = b.BootcampCategories
                                                        .Select(bc => new GetCategoryDto
                                                        {
                                                            Id= bc.CategoryId,
                                                            Name = bc.Category.Name,
                                                        }).ToList(),
                                    }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(camp);
        }

        public async Task<OperationResult<GetDetailsBootcampDto>> Add(AddBootcampDto dto)
        {
            var bootcamp = new Bootcamp
            {
                Title = dto.Title,
                Description = dto.Description,
                BootcampCourses = dto.CourseIds.ToList().Select(c => new BootcampCourse
                {
                    CourseId = c
                }).ToList(),
                BootcampCategories = dto.CategoryIds.ToList().Select(c => new BootcampCategory
                {
                    CategoryId = c
                }).ToList(),
            };
            if(dto.Image != null)
            {
                var file = (await fileRepository.Add("Bootcamp", new List<IFormFile> { dto.Image })).Result;
                bootcamp.BootcampFiles = new List<BootcampFile>
                {
                    new BootcampFile
                    {
                        FileId = file.First().Id
                    }
                };
            }

            context.Add(bootcamp);
            await context.SaveChangesAsync();
            return await GetById(bootcamp.Id);
        }

        public async Task<OperationResult<GetDetailsBootcampDto>> Update(UpdateBootcampDto dto)
        {
            var bootcamp = await context.Bootcamps
                                        .Where(b => b.Id.Equals(dto.Id))
                                        .Include(b => b.BootcampFiles)
                                        .Include(b => b.BootcampCourses)
                                        .Include(b => b.BootcampCategories)
                                        .SingleOrDefaultAsync();
            bootcamp.Title = dto.Title;
            bootcamp.Description = dto.Description;

            if(dto.Image is not null || dto.IsDeleted)
            {
                await fileRepository.Delete(bootcamp.BootcampFiles.Select(b => b.FileId).ToList());
            }
            if(dto.Image is not null)
            {
                await fileRepository.Add("Bootcamp", new List<IFormFile> { dto.Image });
            }
            foreach (var courseId in dto.CourseIds)
            {
                if(!bootcamp.BootcampCourses.Select(bc => bc.CourseId).Contains(courseId))
                {
                    context.Add(new BootcampCourse
                    {
                        BootcampId = bootcamp.Id,
                        CourseId = courseId,
                    });
                }
            }
            foreach (var course in bootcamp.BootcampCourses)
            {
                if (!dto.CourseIds.Contains(course.CourseId))
                {
                    context.Remove(course);
                }
            }

            foreach (var categoryId in dto.CategoryIds)
            {
                if (!bootcamp.BootcampCategories.Select(bc => bc.CategoryId).Contains(categoryId))
                {
                    context.Add(new BootcampCategory
                    {
                        BootcampId = bootcamp.Id,
                        CategoryId = categoryId,
                    });
                }
            }
            foreach (var category in bootcamp.BootcampCategories)
            {
                if (!dto.CategoryIds.Contains(category.CategoryId))
                {
                    context.Remove(category);
                }
            }
            context.Update(bootcamp);
            await context.SaveChangesAsync();

            return await GetById(bootcamp.Id);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var bootcamps = await context.Bootcamps
                                         .Where(b => ids.Contains(b.Id))
                                         .Include(b => b.BootcampCategories)
                                         .Include(b => b.BootcampFiles)
                                         .Include(b => b.BootcampCourses)
                                         .Include(b => b.BootcampUsers)
                                         .ToListAsync();
            foreach (var boot in bootcamps)
            {
                var res = await fileRepository.Delete(boot.BootcampFiles.Select(bf => bf.FileId).ToList());
                context.RemoveRange(boot.BootcampFiles);
                context.RemoveRange(boot.BootcampCategories);
                context.RemoveRange(boot.BootcampCourses);
                context.RemoveRange(boot.BootcampUsers);
            }
            context.RemoveRange(bootcamps);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> AddToFavourite(Guid id)
        {
            var bootcamp = await context.BootcampUsers.Where(c => c.BootcampId == id
                                                               && c.UserId.Equals(context.CurrentUserId()))
                                                      .SingleOrDefaultAsync();
            if (bootcamp is null)
            {
                context.BootcampUsers.Add(new BootcampUser
                {
                    BootcampId = id,
                    UserId = context.CurrentUserId(),
                    IsFavourite = true
                });
            }
            else
            {
                bootcamp.IsFavourite = true;
                context.Update(bootcamp);
            }
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> RemoveFromFavourite(Guid id)
        {
            var bootcamp = await context.BootcampUsers.Where(c => c.BootcampId == id
                                                               && c.UserId.Equals(context.CurrentUserId()))
                                                      .SingleOrDefaultAsync();
            bootcamp.IsFavourite = false;
            context.Update(bootcamp);

            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> AddToLearning(Guid id)
        {
            context.BootcampUsers.Add(new BootcampUser
            {
                BootcampId = id,
                UserId = context.CurrentUserId(),
                IsFavourite = true
            });

            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<IEnumerable<GetBootcampDto>>> GetByCoachId(Guid id)
        {
            var camps = await context.Bootcamps
                                     .Where(b => b.BootcampCourses.Select(bc => bc.Course.CoachId).Contains(id))                     
                                     .Select(b => new GetBootcampDto
                                     {
                                         Id = b.Id,
                                         Title = b.Title,
                                         ImagePath = b.BootcampFiles
                                                      .Select(b => b.File.Path)
                                                      .FirstOrDefault(),
                                         Evaluation = b.BootcampUsers
                                                       .Where(b => b.DateFinished.HasValue)
                                                       .Sum(b => b.Evaluation.Value)
                                                       / (b.BootcampUsers
                                                          .Count(b => b.DateFinished.HasValue)
                                                          != 0 ? b.BootcampUsers.Count(c => c.DateFinished.HasValue) : 1.0)
                                     }).ToListAsync();
            return OperationR.SetSuccess(camps.AsEnumerable());
        }
    }
}
