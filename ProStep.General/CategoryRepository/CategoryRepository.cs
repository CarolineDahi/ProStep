using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.Category;
using ProStep.Model.Courses;
using ProStep.Model.General;
using ProStep.Model.Profile;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.CategoryRepository
{
    public class CategoryRepository : ProStepRepository, ICategoryRepository
    {
        public CategoryRepository(ProStepDBContext context) : base(context)
        {
        }

        public async Task<OperationResult<IEnumerable<GetCategoryDashDto>>> GetAll()
        {
            var categories = await context.Categories
                                          .Where(c => !c.ParentCategoryId.HasValue)
                                          .Select(c => new GetCategoryDashDto
                                          {
                                              Id = c.Id,
                                              Name = c.Name,
                                              SubCategories = c.ChildCategories
                                                               .Select(ch => new GetCategoryDto
                                                               {
                                                                   Id = ch.Id,
                                                                   Name = ch.Name
                                                               }).ToList(),
                                          }).ToListAsync();
            return OperationR.SetSuccess(categories.AsEnumerable());
        }


        public async Task<OperationResult<DetailsCategoryDto>> GetById(Guid id)
        {
            var category = await context.Categories.Where(c => c.Id.Equals(id))
                                                   .Select(c => new DetailsCategoryDto
                                                   {
                                                       Id = c.Id,
                                                       Name = c.Name,
                                                       Childs = c.ChildCategories
                                                                 .Select(child => new GetCategoryDto
                                                                 {
                                                                     Id = child.Id,
                                                                     Name = child.Name,
                                                                 }).ToList()
                                                   }).SingleOrDefaultAsync();
            return OperationR.SetSuccess(category);
        }

        public async Task<OperationResult<DetailsCategoryDto>> Add(AddCategoryDto categoryDto)
        {
            var category = new Category
            {
                ParentCategoryId = categoryDto.ParentCategoryId,
                Name = categoryDto.Name
            };
            context.Add(category);
            await context.SaveChangesAsync();

            return OperationR.SetSuccess((await GetById(category.Id)).Result);
        }

        public async Task<OperationResult<DetailsCategoryDto>> Update(UpdateCategoryDto categoryDto)
        {
            var category = await context.Categories.Where(c => c.Id.Equals(categoryDto.Id))
                                                   .SingleOrDefaultAsync();

            category.ParentCategoryId = categoryDto.ParentCategoryId;
            category.Name = categoryDto.Name;
            context.Update(category);
            await context.SaveChangesAsync();
            return OperationR.SetSuccess((await GetById(category.Id)).Result);
        }

        public async Task<OperationResult<bool>> Delete(IEnumerable<Guid> ids)
        {
            var categories = await context.Categories
                                          .Where(c => ids.Contains(c.Id))
                                          .Include(c => c.ChildCategories)
                                          .Include(c => c.PortfolioCategories)
                                          .Include(c => c.BootcampCategories)
                                          .Include(c => c.CourseCategories)
                                          .ToListAsync();

            //await Delete(categories.SelectMany(c => c.ChildCategories.Select(ch => ch.Id).ToList()).ToList());

            categories.ForEach(async c =>
            {
                Delete(c.ChildCategories.Select(ch => ch.Id).ToList());
                context.RemoveRange(c.PortfolioCategories);
                context.RemoveRange(c.BootcampCategories);
                context.RemoveRange(c.CourseCategories);
            });
            context.RemoveRange(categories);

            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

        public async Task<OperationResult<IEnumerable<GetCategoryDto>>> GetMain()
        {
            var categories = await context.Categories
                                          .Where(c => c.ParentCategoryId == null)
                                          .Select(c => new GetCategoryDto
                                          {
                                              Id = c.Id,
                                              Name = c.Name
                                          }).ToListAsync();
            return OperationR.SetSuccess(categories.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetCategoryDto>>> GetSub()
        {
            var categories = await context.Categories
                                          .Where(c => c.ParentCategoryId != null)
                                          .Select(c => new GetCategoryDto
                                          {
                                              Id = c.Id,
                                              Name = c.Name
                                          }).ToListAsync();
            return OperationR.SetSuccess(categories.AsEnumerable());
        }

        public async Task<OperationResult<bool>> ChooseMyCategories(IEnumerable<Guid> ids)
        {
            var portfolio = await context.Portfolios.Where(p => p.UserId.Equals(context.CurrentUserId())).SingleOrDefaultAsync();
            context.AddRange(
                ids.ToList().Select(id => new PortfolioCategory
                {
                    CategoryId = id,
                    PortfolioId = portfolio.Id
                }));
            await context.SaveChangesAsync();
            return OperationR.SetSuccess(true);
        }

    }
}
