using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.City;
using ProStep.DataTransferObject.General.Country;
using ProStep.General.CityRepository;
using ProStep.Model.General;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.CountryRepository
{
    public class CountryRepository : ProStepRepository, ICountryRepository
    {
        private readonly ICityRepository cityRepository;

        public CountryRepository(ProStepDBContext context, ICityRepository cityRepository) : base(context)
        {
            this.cityRepository = cityRepository;
        }

        public async Task<OperationResult<IEnumerable<GetCountryDto>>> GetAll()
        {
            var countries = await context.Countries
                                         .Select(c => new GetCountryDto
                                         {
                                             Id = c.Id,
                                             Name = c.Name,
                                             CityDtos = c.Cities.Select(ci => new GetBaseCityDto
                                                                {
                                                                    Id = ci.Id,
                                                                    Name = ci.Name,
                                                                }).ToList(),
                                         }).ToListAsync();
            return OperationR.SetSuccess(countries.AsEnumerable());
        }

        public async Task<OperationResult<GetCountryDto>> GetById(Guid id)
        {
            var country = await context.Countries.Where(c => c.Id.Equals(id))
                                                 .Select(c => new GetCountryDto
                                                 {
                                                     Id = c.Id,
                                                     Name = c.Name,
                                                     CityDtos = c.Cities.Select(ci => new GetBaseCityDto
                                                                        {
                                                                            Id = ci.Id,
                                                                            Name = ci.Name,
                                                                        }).ToList(),
                                                 }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(country);
        }

        public async Task<OperationResult<GetCountryDto>> Add(AddCountryDto countryDto)
        {
            var country = new Country
            {
                Name = countryDto.Name,
            };

            context.Add(country);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(country.Id)).Result);
        }

        public async Task<OperationResult<GetCountryDto>> Update(UpdateCountryDto countryDto)
        {
            var country = await context.Countries
                                       .Where(c => c.Id.Equals(countryDto.Id))
                                       .SingleOrDefaultAsync();
            country.Name = countryDto.Name;
            context.Update(country);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(countryDto.Id)).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var cityIds = await context.Cities.Where(c => ids.Contains(c.CountryId))
                                              .Select(c => c.Id)
                                              .ToListAsync();
            await cityRepository.Delete(cityIds);

            var countries = await context.Countries.Where(c => ids.Contains(c.Id)).ToListAsync();
            context.RemoveRange(countries);
            
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

    }
}
