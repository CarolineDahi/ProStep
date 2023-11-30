using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ProStep.SharedKernel.Enums.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.SharedKernel.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static void GlobalFilters<TInterface>(this ModelBuilder builder, Expression<Func<TInterface, bool>> expression)
        {
            var entities = builder.Model.GetEntityTypes()
                                        .Where(e => e.ClrType.GetInterface(typeof(TInterface).Name) != null)
                                        .Select(e => e.ClrType);
            foreach (var entity in entities)
            {
                var newParam = Expression.Parameter(entity);
                var newbody = ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                builder.Entity(entity).HasQueryFilter(Expression.Lambda(newbody, newParam));
            }
        }

        public static FileType TypeOfFile(IFormFile file)
        {
            var type = new FileType();
            if (file.ContentType == "image/gif")
            {
                type = FileType.Image;
            }
            else if (file.ContentType == "application/pdf")
            {
                type = FileType.Document;
            }
            else
            {
                type = FileType.File;
            }
            return type;
        }

        public static string MyString(this List<string> list) 
            => String.Join(",.", list).ToString();

        public static List<string> MyList(this string myString)
            => myString.Split(",.").ToList();

        
    }
}
