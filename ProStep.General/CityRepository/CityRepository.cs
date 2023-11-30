using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.City;
using ProStep.Model.General;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.CityRepository
{
    public class CityRepository : ProStepRepository, ICityRepository
    {
        public CityRepository(ProStepDBContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<GetCityDto>>> GetAll()
        {
            var cities = await context.Cities
                                      .Select(c => new GetCityDto
                                      {
                                          Id = c.Id,
                                          Name = c.Name,
                                          CountryId = c.CountryId,
                                          CountryName = c.Country.Name,
                                      }).ToListAsync();
            return OperationR.SetSuccess(cities.AsEnumerable());
        }

        public async Task<OperationResult<GetCityDto>> GetById(Guid id)
        {
            var city = await context.Cities.Where(c => c.Id.Equals(id))
                                           .Select(c => new GetCityDto
                                           {
                                               Id = c.Id,
                                               Name = c.Name,
                                               CountryId = c.CountryId,
                                               CountryName = c.Country.Name,
                                           }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(city);
        }

        public async Task<OperationResult<GetCityDto>> Add(AddCityDto cityDto)
        {
            var city = new City
            {
                Name = cityDto.Name,
                CountryId = cityDto.CountryId,
            };
            context.Add(city);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(city.Id)).Result);
        }

        public async Task<OperationResult<GetCityDto>> Update(UpdateCityDto cityDto)
        {
            var city = await context.Cities.Where(c => c.Id.Equals(cityDto.Id)).SingleOrDefaultAsync();
            city.Name = cityDto.Name;
            city.CountryId = cityDto.CountryId;
            context.Update(city);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(city.Id)).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var users = await context.Users
                                     .Where(u => ids.Contains(u.Portfolio.CityId.Value))
                                     .Include(u => u.Portfolio)
                                     .ToListAsync();
            users.ForEach(u => u.Portfolio.CityId = null);

            var cities = await context.Cities.Where(c => ids.Contains(c.Id)).ToListAsync();
            context.RemoveRange(cities);

            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }
    }
}
