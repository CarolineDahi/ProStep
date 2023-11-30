using Microsoft.EntityFrameworkCore;
using ProStep.DataSourse.Context;
using ProStep.DataSourse.Migrations;
using ProStep.Model.Courses;
using ProStep.Model.General;
using ProStep.Model.Maps;
using ProStep.SharedKernel.Enums.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Task = System.Threading.Tasks.Task;

namespace ProStep.DataSourse.DataSeed
{
    public static class SeedData
    {
        public static async _Task InitializeAsync(IServiceProvider services)
        {
            var context = (ProStepDBContext)services.GetService(typeof(ProStepDBContext));
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await CategorySeed(context);
                    await CountrySeed(context);
                    await CourseSeed(context);
                    //await RoadMapSeed(context);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
        }

        private static async _Task CategorySeed(ProStepDBContext context)
        {
            if (!context.Categories.Any())
            {
                var cat1 = new Category
                {
                    Name = "Web Development"
                };
                context.Add(cat1);

                var cat2 = new Category
                {
                    ParentCategoryId = cat1.Id,
                    Name = "Back-end"
                };
                context.Add(cat2);

                await context.SaveChangesAsync();
            }
        }

        private static async _Task CountrySeed(ProStepDBContext context)
        {
            if (!context.Countries.Any())
            {
                var country = new Country
                {
                    Name = "Syria",
                    Cities = new List<City>
                    {
                        new City { Name = "Aleppo"},
                        new City { Name = "Homs"},
                        new City { Name = "Hama"},
                       new City { Name = "Latakia"},
                    }
                };
                context.Add(country);
                await context.SaveChangesAsync();
            }
        }

        private static async _Task CourseSeed(ProStepDBContext context)
        {
            if(!context.Courses.Any())
            {
                var course = new Course
                {
                    Title = "test caro",
                    Description = "this is description",
                    Requirements = "test",
                    Target = "test",
                    CoachId = context.Users.Where(u => u.UserType == UserType.SuperAdmin).First().Id,
                    CourseCategories = new List<CourseCategory>()
                    {
                        new CourseCategory
                        {
                            CategoryId = context.Categories.First().Id,
                        }
                    },
                };
                context.Add(course);
                await context.SaveChangesAsync();
            }
        }
    }
}
