using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.Category;
using ProStep.DataTransferObject.Profile.Education;
using ProStep.DataTransferObject.Profile.Portfolio;
using ProStep.DataTransferObject.Profile.WorkExperience;
using ProStep.DataTransferObject.Shared.File;
using ProStep.Model.Profile;
using ProStep.Model.Security;
using ProStep.Shared.FileRepository;
using ProStep.SharedKernel.Enums.Portfolio;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Profile.PortfolioRepository
{
    public class PortfolioRepository : ProStepRepository, IPortfolioRepository
    {
        private readonly IFileRepository fileRepository;

        public PortfolioRepository(ProStepDBContext context, IFileRepository fileRepository) : base(context)
        {
            this.fileRepository = fileRepository;
        }

        public async Task<OperationResult<GetMyPortfolioDto>> GetMyPortfolio()
        {
            var portfolio = await context.Users.Where(u => u.Id.Equals(context.CurrentUserId()))
                                               .Select(u => new GetMyPortfolioDto
                                               {
                                                   Id = u.Id,
                                                   IsCoach = u.UserType == UserType.Coach ? true : false,
                                                   ImagePath = u.ImagePath,
                                                   FirstName = u.FirstName,
                                                   LastName = u.LastName,
                                                   Email = u.Email,
                                                   Bio = u.Portfolio.Bio,
                                                   CityId = u.Portfolio.CityId,
                                                   CityName = u.Portfolio.City.Name,
                                                   CountryId = u.Portfolio.City.CountryId,
                                                   CountryName = u.Portfolio.City.Country.Name,
                                                   CoverPath = u.UserFiles
                                                                .Where(f => f.Type.Equals(UserFileType.Cover))
                                                                .Select(f => f.File.Path)
                                                                .SingleOrDefault(),
                                                   Careers = u.Portfolio
                                                              .PortfolioCategories
                                                              .Where(c => c.Type.Equals(SKillType.Career))
                                                              .Select(c => new GetCategoryDto
                                                              {
                                                                  Id = c.Category.Id,
                                                                  Name = c.Category.Name,
                                                              }).ToList(),
                                                   Skills = u.Portfolio
                                                             .PortfolioCategories
                                                             .Where(c => c.Type.Equals(SKillType.Skill))
                                                             .Select(c => new GetCategoryDto
                                                             {
                                                                 Id = c.Category.Id,
                                                                 Name = c.Category.Name,
                                                             }).ToList(),
                                                   WorkExperiences = u.Portfolio.WorkExperiences
                                                                                .Select(w => new GetWorkDto
                                                                                {
                                                                                    Id = w.Id,
                                                                                    Start = w.Start,
                                                                                    End = w.End,
                                                                                    Position = w.Position,
                                                                                    CompanyName = w.CompanyName,
                                                                                    WorkType = w.WorkType,
                                                                                    CityId = w.CityId,
                                                                                    CityName = w.City.Name,
                                                                                    CountryId = w.City.CountryId,
                                                                                    CountryName = w.City.Country.Name
                                                                                }).ToList(),
                                                   Educations = u.Portfolio.Educations
                                                                           .Select(e => new GetEducationDto
                                                                           {
                                                                               Id = e.Id,
                                                                               Start = e.Start,
                                                                               End = e.End,
                                                                               FacultyId = e.FacultyId,
                                                                               FacultyName = e.Faculty.Name,
                                                                               UniversityId = e.Faculty.UniversityId,
                                                                               UniversityName = e.Faculty.University.Name,
                                                                           }).ToList(),
                                               }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(portfolio);
        }

        public async Task<OperationResult<GetPortfolioDto>> GetPortfolio(Guid id)
        {
            var portfolio = await context.Users.Where(u => u.Id.Equals(id))
                                               .Select(u => new GetPortfolioDto
                                               {
                                                   Id = u.Id,
                                                   IsCoach = u.UserType == UserType.Coach ? true : false,
                                                   ImagePath = u.ImagePath,
                                                   FirstName = u.FirstName,
                                                   LastName = u.LastName,
                                                   Bio = u.Portfolio.Bio,
                                                   CoverPath = u.UserFiles!
                                                                .Where(f => f.Type.Equals(UserFileType.Cover))
                                                                .Select(f => f.File.Path)
                                                                .SingleOrDefault(),
                                                   Careers = u.Portfolio
                                                              .PortfolioCategories
                                                              .Where(c => c.Type.Equals(SKillType.Career))
                                                              .Select(c => new GetCategoryDto
                                                              {
                                                                  Id = c.Category.Id,
                                                                  Name = c.Category.Name,
                                                              }).ToList(),
                                                   Skills = u.Portfolio
                                                             .PortfolioCategories
                                                             .Where(c => c.Type.Equals(SKillType.Skill))
                                                             .Select(c => new GetCategoryDto
                                                             {
                                                                 Id = c.Category.Id,
                                                                 Name = c.Category.Name,
                                                             }).ToList(),
                                                   Educations = u.Portfolio.Educations
                                                                           .Select(e => new GetEducationDto
                                                                           {
                                                                               Id = e.Id,
                                                                               Start = e.Start,
                                                                               End = e.End,
                                                                               FacultyId = e.FacultyId,
                                                                               FacultyName = e.Faculty.Name,
                                                                               UniversityId = e.Faculty.UniversityId,
                                                                               UniversityName = e.Faculty.University.Name,
                                                                           }).ToList(),
                                                   WorkExperiences = u.Portfolio.WorkExperiences
                                                                                .Select(w => new GetWorkDto
                                                                                {
                                                                                    Id = w.Id,
                                                                                    Start = w.Start,
                                                                                    End = w.End,
                                                                                    Position = w.Position,
                                                                                    CompanyName = w.CompanyName,
                                                                                    WorkType = w.WorkType,
                                                                                    CityId = w.CityId,
                                                                                    CityName = w.City.Name,
                                                                                    CountryId = w.City.CountryId,
                                                                                    CountryName = w.City.Country.Name,
                                                                                }).ToList(),
                                               }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(portfolio);
        }

        public async Task<OperationResult<string>> UploadMyImage(IFormFile file)
        {
            var user = await context.Users.Where(u => u.Id.Equals(context.CurrentUserId())).SingleOrDefaultAsync();
            await fileRepository.Remove(user.ImagePath);
            var path = (await fileRepository.Upload("Users", file)).Result;
            user.ImagePath = path;
            context.Update(user);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(path);
        }

        public async Task<OperationResult<string>> UploadMyCover(IFormFile file)
        {
            var curr = context.CurrentUserId();
            var user = await context.Users.Where(u => u.Id.Equals(curr))
                                          .Include(u => u.UserFiles)
                                          .SingleOrDefaultAsync();

            if(user.UserFiles != null)
            {
                var oldCover = user.UserFiles.Where(f => f.Type.Equals(UserFileType.Cover)).SingleOrDefault();
                if (oldCover != null)
                {
                    var delOld = await fileRepository.Delete(new List<Guid>() { oldCover.FileId });
                    context.Remove(oldCover);
                }
            }

            var cover = (await fileRepository.Add("Users", new List<IFormFile>(){ file })).Result.FirstOrDefault();
            var userFile = new UserFile
            {
                FileId = cover.Id,
                UserId = user.Id,
                Type = UserFileType.Cover,
            };
            context.Add(userFile);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(cover.Path);
        }

        public async Task<OperationResult<GetMyPortfolioDto>> UpateMyPortfolio(UpdatePortfolioDto dto)
        {
            var user = await context.Users.Where(u => u.Id.Equals(context.CurrentUserId()))
                                          .Include(u => u.Portfolio)
                                          .ThenInclude(p => p.Educations)
                                          .Include(u => u.Portfolio)
                                          .ThenInclude(p => p.WorkExperiences)
                                          .SingleOrDefaultAsync();

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Portfolio.CityId = dto.CityId;
            user.Portfolio.Bio = dto.Bio;

            if (dto.Educations != null)
            {
                foreach (var e in dto.Educations)
                {
                    if (!e.Id.HasValue)
                    {
                        context.Add(new Education
                        {
                            PortfolioId = user.PortfolioId.Value,
                            FacultyId = e.FacultyId,
                            Start = e.Start,
                            End = e.End,
                        });
                    }
                    else
                    {
                        var education = await context.Educations.FindAsync(e.Id);
                        if (education != null)
                        {
                            education.Start = e.Start;
                            education.End = e.End;
                            education.FacultyId = e.FacultyId;
                            context.Update(education);
                        }
                    }
                }
            }
            if(dto.EducationRemoveIds != null)
            {
                var educationsToDel = user.Portfolio.Educations.Where(e => dto.EducationRemoveIds.Contains(e.Id)).ToList();
                context.RemoveRange(educationsToDel);
            }

            if (dto.WorkExperiences != null)
            {
                foreach (var w in dto.WorkExperiences)
                {
                    if (!w.Id.HasValue)
                    {
                        context.Add(new WorkExperience
                        {
                            PortfolioId = user.PortfolioId.Value,
                            Position = w.Position,
                            CompanyName = w.CompanyName,
                            Start = w.Start,
                            End = w.End,
                            WorkType = w.WorkType,
                            CityId = w.CityId
                        });
                    }
                    else
                    {
                        var work = await context.WorkExperiences.FindAsync(w.Id);
                        if (work != null)
                        {
                            work.Start = w.Start;
                            work.End = w.End;
                            work.CityId = w.CityId;
                            work.WorkType = w.WorkType;
                            work.Position = w.Position;
                            work.CompanyName = w.CompanyName;
                            context.Update(work);
                        }
                    }
                }
            }
            if (dto.WorkRemoveIds != null)
            {
                var workToDel = user.Portfolio.WorkExperiences.Where(w => dto.WorkRemoveIds.Contains(w.Id)).ToList();
                context.RemoveRange(workToDel);
            }

            if(dto.ApproveToCoach)
            {
                user.UserType = UserType.Coach; 
                context.Update(user);
            }

            await context.SaveChangesAsync();

            return await GetMyPortfolio();
        }

        public async Task<OperationResult<bool>> UpdateMyCareers(UpdateDto dto)
        {
            var user = await context.Users.Where(u => u.Id.Equals(context.CurrentUserId()))
                                          .Include(u => u.Portfolio)
                                          .ThenInclude(u => u.PortfolioCategories)
                                          .SingleOrDefaultAsync();

            if (dto.Ids != null)
            {
                dto.Ids.ForEach(c => context.Add(new PortfolioCategory
                {
                    CategoryId = c,
                    PortfolioId = user.PortfolioId.Value,
                    Type = SKillType.Career,
                }));
            }
            if (dto.RemoveIds != null)
            {
                var careerToRemove = user.Portfolio.PortfolioCategories
                                                    .Where(c => dto.RemoveIds.Contains(c.CategoryId))
                                                    .ToList();
                context.RemoveRange(careerToRemove);
            }

            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<bool>> UpdateMySkills(UpdateDto dto)
        {
            var user = await context.Users.Where(u => u.Id.Equals(context.CurrentUserId()))
                                          .Include(u => u.Portfolio)
                                          .ThenInclude(p => p.PortfolioCategories)
                                          .SingleOrDefaultAsync();

            if (dto.Ids != null)
            {
                dto.Ids.ForEach(c => context.Add(new PortfolioCategory
                {
                    CategoryId = c,
                    PortfolioId = user.PortfolioId.Value,
                    Type = SKillType.Skill,
                }));
            }
            if (dto.RemoveIds != null)
            {
                var skillToRemove = user.Portfolio.PortfolioCategories
                                                  .Where(c => dto.RemoveIds.Contains(c.CategoryId))
                                                  .ToList();
                context.RemoveRange(skillToRemove);
            }

            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<IEnumerable<GetFileDto>>> GetCertificates(Guid? id)
        {
            var certificates = await context.UserFiles
                                            .Where(u => u.UserId == (id != null ? id : context.CurrentUserId())
                                                     && u.Type.Equals(UserFileType.Certificate))
                                            .Select(u => new GetFileDto
                                            {
                                                Id = u.FileId,
                                                Name = u.File.Name,
                                                Path = u.File.Path,
                                                Type = u.File.Type,
                                            }).ToListAsync();
            return OperationR.SetSuccess(certificates.AsEnumerable());
        }


        public async Task<OperationResult<bool>> UpdateMyCertificates(UpdateCertificateDto dto)
        {
            if(dto.Files != null)
            {
                var files = await fileRepository.Add("Certificates", dto.Files.ToList());
                files.Result.ForEach(f => context.Add(new UserFile
                {
                    FileId = f.Id,
                    UserId = context.CurrentUserId(),
                    Type = UserFileType.Certificate,
                }));
            }
            if(dto.RemoveIds != null)
            {
                var certificates = await context.UserFiles
                                                .Where(c => dto.RemoveIds.Contains(c.FileId))
                                                .ToListAsync();
                context.RemoveRange(certificates);
            }
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        

    }
}
