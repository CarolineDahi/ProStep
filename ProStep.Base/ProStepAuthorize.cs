using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using ProStep.DataSourse.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ProStep.SharedKernel.Enums.Security;

namespace ProStep.Base
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ProStepAuthorize : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private ProStepDBContext dbContext;
        public ProStepAuthorize() { }
        public ProStepAuthorize(params string[] roles) : base()
            => base.Roles = string.Join(",", roles);
        public ProStepAuthorize(params ProStepRole[] roles) : this(roles.Select(x => x.ToString()).ToArray()) { }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {

            dbContext = (ProStepDBContext)context.HttpContext.RequestServices.GetService(typeof(ProStepDBContext));

            var userId = context?.HttpContext?.User?.Claims?.Where(x => x.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;
            if (userId != null)
            {
                var user = await dbContext.Users.IgnoreQueryFilters().Where(x => x.Id.ToString() == userId).FirstOrDefaultAsync();
                if (user is not null && !user.DateDeleted.HasValue)
                    return;
            }

            context.Result = new UnauthorizedResult();

            return;
        }
    }
}

