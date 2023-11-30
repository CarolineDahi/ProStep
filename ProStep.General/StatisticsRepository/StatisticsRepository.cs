using Microsoft.EntityFrameworkCore;
using ProStep.Base;
using ProStep.DataSourse.Context;
using ProStep.DataTransferObject.General.Statistics;
using ProStep.SharedKernel.Enums.Security;
using ProStep.SharedKernel.OperationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.General.StatisticsRepository
{
    public class StatisticsRepository : ProStepRepository, IStatisticsRepository
    {
        public StatisticsRepository(ProStepDBContext context) : base(context)
        {
        }

        public async Task<OperationResult<GetCountDto>> GetCounts()
        {
            var course = context.Courses!.Count();
            var bootcamps = context.Bootcamps!.Count();
            var roads = context.RoadMaps!.Count();
            var coachs = context.Users.Where(u => u.UserType.Equals(UserType.Coach)).Count();
            var students = context.Users.Where(u => u.UserType.Equals(UserType.User)).Count();

            return OperationR.SetSuccess(new GetCountDto
            {
                CountCourse = course,
                CountBootcamp = bootcamps,
                CountRoadMap = roads,
                CountCoach = coachs,
                CountStudent = students
            });

        }

        public async Task<OperationResult<IEnumerable<GetCategoryWithCountDto>>> GetCategories()
        {
            var categories = await context.Categories.Select(c => new GetCategoryWithCountDto
            {
                Id = c.Name,
                Label = c.Name,
                Value = c.CourseCategories!.Count()
            }).OrderByDescending(c => c.Value).Take(5).ToListAsync();

            return OperationR.SetSuccess(categories.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetCategoryWithCountDto>>> GetRoadMaps()
        {
            var categories = await context.Categories.Select(c => new GetCategoryWithCountDto
            {
                Id = c.Name,
                Label = c.Name,
                Value = c.RoadMap!.Count()
            }).OrderByDescending(c => c.Value).Take(5).ToListAsync();

            return OperationR.SetSuccess(categories.AsEnumerable());
        }

        public async Task<OperationResult<IEnumerable<GetMonthsDto>>> GetUsers()
        {
            var users = await context.CourseUsers
                .GroupBy(cu => cu.DateCreated.Month)
                           .Select(group => new GetMonthsDto
                           {
                               Month = group.Key.ToString(),
                               NumOfUsers = group.Count()
                           }).ToListAsync();
            var allMonths = Enumerable.Range(1, 12); // Represents all months of the year

            // Find the missing months and add them with NumOfUsers set to 0
            var missingMonths = allMonths.Except(users.Select(u => int.Parse(u.Month)));
            users.AddRange(missingMonths.Select(m => new GetMonthsDto
            {
                Month = m.ToString(),
                NumOfUsers = 0
            }));


            return OperationR.SetSuccess(users.OrderBy(u => int.Parse(u.Month)).AsEnumerable());
        }
    }
}
