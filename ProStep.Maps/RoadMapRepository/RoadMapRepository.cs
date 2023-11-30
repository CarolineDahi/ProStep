using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.Maps.RoadMap;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProStep.Model.Maps;
using ProStep.DataTransferObject.General.Category;

namespace ProStep.Maps.RoadMapRepository
{
    public class RoadMapRepository : ProStepRepository, IRoadMapRepository
    {
        public RoadMapRepository(ProStepDBContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<GetRoadMapDto>>> GetAll(Guid? categoryId)
        {
            var maps = await context.RoadMaps
                                    .Where(r => categoryId.HasValue ? r.CategoryId == categoryId
                                                                    : true)
                                    .Select(r => new GetRoadMapDto
                                    {
                                        Id = r.Id,
                                        Title = r.Title,
                                        CategoryId = r.CategoryId,
                                        CategoryName = r.Category.Name
                                    }).ToListAsync();

            return OperationR.SetSuccess(maps.AsEnumerable());
        }

        public async Task<OperationResult<DetailsRoadMapDto>> GetById(Guid id)
        {
            var map = await context.RoadMaps
                                   .Where(r => r.Id.Equals(id))
                                   .Select(r => new DetailsRoadMapDto
                                   {
                                       Id = r.Id,
                                       Title = r.Title,
                                       CategoryId = r.CategoryId,
                                       CategoryName = r.Category.Name,
                                       Categories = r.RoadMapCategories.Where(rc => rc.RoadMapCategoryParentId == null).Select(rc => new GetSubCategoryDto
                                       { 
                                           Id = rc.Id,
                                           Name = rc.Category.Name,
                                           SubCategories = rc.RoadMapCategories
                                                             .Select(s => new GetCategoryDto
                                                             {
                                                                 Id = s.CategoryId,
                                                                 Name = s.Category.Name,
                                                             }).ToList()
                                       }).ToList()
                                   }).SingleOrDefaultAsync();

            return OperationR.SetSuccess(map);
        }

        public async Task<OperationResult<DetailsRoadMapDto>> Add(AddRoadMapDto dto)
        {
            var map = new RoadMap
            {
                Title = dto.Title,
                CategoryId = dto.CategoryId,
                
            };
            context.Add(map);
            map.RoadMapCategories = dto.SubCategories.Select(rc => new RoadMapCategory
            {
                RoadMapId = map.Id,
                CategoryId = rc.CategoryId,
                RoadMapCategories = rc.SubCategories.Select(s => new RoadMapCategory
                {
                    CategoryId = s,
                    RoadMapCategoryParentId = rc.CategoryId,
                    RoadMapId = map.Id
                }).ToList()
            }).ToList();

            await context.SaveChangesAsync();
            return (GetById(map.Id).Result);
        }

        public Task<OperationResult<DetailsRoadMapDto>> Update()
        {
            return null;
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var maps = await context.RoadMaps
                                    .Where(r => ids.Contains(r.Id))
                                    .ToListAsync();
            maps.ForEach(map =>
            {
                context.Remove(map);
            });
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

    }
}
