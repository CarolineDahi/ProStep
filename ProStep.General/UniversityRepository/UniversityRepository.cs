using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.Faculty;
using ProStep.DataTransferObject.General.University;
using ProStep.General.FacultyRepository;
using ProStep.Model.General;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.UniversityRepository
{
    public class UniversityRepository : ProStepRepository, IUniversityRepository
    {
        private readonly IFacultyRepository facultyRepository;

        public UniversityRepository(ProStepDBContext context, IFacultyRepository facultyRepository) : base(context)
        {
            this.facultyRepository = facultyRepository;
        }

        public async Task<OperationResult<IEnumerable<GetUniversityDto>>> GetAll()
        {
            var universities = await context.Universities
                                            .Select(u => new GetUniversityDto
                                            {
                                                Id = u.Id,
                                                Name = u.Name,
                                                FacultyDtos = u.Faculties
                                                               .Select(f => new GetBaseFacultyDto
                                                               {
                                                                   Id = f.Id,
                                                                   Name = f.Name,
                                                               }).ToList()
                                            }).ToListAsync();

            return OperationR.SetSuccess(universities.AsEnumerable());
        }

        public async Task<OperationResult<GetUniversityDto>> GetById(Guid id)
        {
            var university = await context.Universities.Where(u => u.Id.Equals(id))
                                                       .Select(u => new GetUniversityDto
                                                       {
                                                           Id = u.Id,
                                                           Name = u.Name,
                                                       }).SingleOrDefaultAsync();

            return OperationR.SetSuccess(university);
        }

        public async Task<OperationResult<GetUniversityDto>> Add(AddUniversityDto universityDto)
        {
            var university = new University
            {
                Name = universityDto.Name,
            };
            context.Add(university);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(university.Id)).Result);
        }

        public async Task<OperationResult<GetUniversityDto>> Update(UpdateUniversityDto universityDto)
        {
            var university = await context.Universities.Where(u => u.Id.Equals(universityDto.Id)).SingleOrDefaultAsync();

            university.Name = universityDto.Name;

            context.Update(university);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(universityDto.Id)).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var facultyIds = await context.Faculties
                                            .Where(f => ids.Contains(f.UniversityId))
                                            .Select(f => f.Id)
                                            .ToListAsync();
            await facultyRepository.Delete(facultyIds);

            var universities = await context.Universities.Where(u => ids.Contains(u.Id)).ToListAsync(); 
            context.RemoveRange(universities);

            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

    }
}
