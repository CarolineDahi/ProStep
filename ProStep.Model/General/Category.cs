using ProStep.Model.Base;
using ProStep.Model.Courses;
using ProStep.Model.Maps;
using ProStep.Model.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProStep.Model.General
{
    public class Category : BaseEntity
    {
        public Category() 
        {
            ChildCategories = new HashSet<Category>();
            PortfolioCategories = new HashSet<PortfolioCategory>();
            CourseCategories = new HashSet<CourseCategory>();
            BootcampCategories= new HashSet<BootcampCategory>();
        }

        public string Name { get; set; }

        public Guid? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set;}


        public ICollection<Category> ChildCategories { get; set;}
        public ICollection<PortfolioCategory> PortfolioCategories { get; set; }
        public ICollection<CourseCategory> CourseCategories { get; set; }
        public ICollection<BootcampCategory> BootcampCategories { get; set; }
        public ICollection<RoadMap> RoadMap { get; set;}
        public ICollection<RoadMapCategory> RoadMapCategories { get; set; }
    }
}
