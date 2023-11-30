using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.Faculty;
using ProStep.Model.General;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.FacultyRepository
{
    public class FacultyRepository : ProStepRepository, IFacultyRepository
    {
        public FacultyRepository(ProStepDBContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<GetFacultyDto>>> GetAll()
        {
            var faculties = await context.Faculties.Select(f => new GetFacultyDto
            {
                Id = f.Id,
                Name = f.Name,
                UniversityId = f.UniversityId,
                UniversityName = f.University.Name
            }).ToListAsync();

            return OperationR.SetSuccess(faculties.AsEnumerable());
        }

        public async Task<OperationResult<GetFacultyDto>> GetById(Guid id)
        {
            var faculty = await context.Faculties.Where(f => f.Id.Equals(id))
                                                 .Select(f => new GetFacultyDto
                                                 {
                                                     Id = f.Id,
                                                     Name = f.Name,
                                                     UniversityId = f.UniversityId,
                                                     UniversityName = f.University.Name
                                                 }).SingleOrDefaultAsync();

            return OperationR.SetSuccess(faculty);
        }

        public async Task<OperationResult<GetFacultyDto>> Add(AddFacultyDto dto)
        {
            var faculty = new Faculty
            {
                Name = dto.Name,
                UniversityId = dto.UniversittyId
            };
            context.Add(faculty);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(faculty.Id)).Result);
        }

        public async Task<OperationResult<GetFacultyDto>> Update(UpdateFacultyDto dto)
        {
            var faculty = await context.Faculties.Where(f => f.Id.Equals(dto.Id)).SingleOrDefaultAsync();
            faculty.Name = dto.Name;
            faculty.UniversityId = dto.UniversityId;

            context.Update(faculty);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(faculty.Id)).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var educations = await context.Educations.Where(e => ids.Contains(e.FacultyId)).ToListAsync();
            context.RemoveRange(educations);

            var faculties = await context.Faculties.Where(f => ids.Contains(f.Id)).ToListAsync();
            context.RemoveRange(faculties);

            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }
    }
}
