using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProStep.Model.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProStep.Model.Courses;
using ProStep.Model.General;
using ProStep.Model.Maps;
using ProStep.Model.Profile;
using ProStep.Model.Quizzes;
using ProStep.Model.TrainingProjects;
using _Task = ProStep.Model.TrainingProjects.Task;
using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProStep.Model.Base;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection.Emit;
using ProStep.Model.Shared;
using ProStep.SharedKernel.ExtensionMethods;

namespace ProStep.DataSourse.Context
{
    public class ProStepDBContext : IdentityDbContext<User, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ProStepDBContext(DbContextOptions<ProStepDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.Database.SetCommandTimeout(180);
        }

        #region - Courses -
        public DbSet<Bootcamp> Bootcamps { get; set; }
        public DbSet<BootcampCategory> BootcampCategories { get; set; }
        public DbSet<BootcampCourse> BootcampCourses { get; set; }
        public DbSet<BootcampFile> BootcampFiles { get; set; }
        public DbSet<BootcampUser> BootcampUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseCategory> CourseCategories { get; set; }
        public DbSet<CourseFile> CourseFiles { get; set; }
        public DbSet<CourseUser> CourseUsers { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<LectureFile> LectureFiles { get; set; }
        #endregion

        #region - General -
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CommonQuestion> CommonQuestions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationUser> NotificationUsers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<University> Universities { get; set; }
        #endregion

        #region - Maps -
        public DbSet<RoadMap> RoadMaps { get; set; }
        #endregion

        #region - Profile -
        public DbSet<Community> Communities { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageFile> MessageFiles { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        #endregion

        #region - Quizzes -
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<UserQuesAnswer> UserQuesAnswers { get; set; }
        #endregion

        #region - Shared -
        public DbSet<_File> Files { get; set; }
        public DbSet<UserFile> UserFiles { get; set; }
        #endregion

        #region - TrainingProjects -
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<_Task> Tasks { get; set; }
        public DbSet<TaskUser> TaskUsers { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<ToDoFile> ToDoFiles { get; set; }
        public DbSet<ToDoUser> ToDoUsers { get; set; }
        public DbSet<TrainingProject> TrainingProjects { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.GlobalFilters<IBaseEntity>(f => !f.DateDeleted.HasValue);

            builder.Entity<Quiz>().HasOne(q => q.Section)
                                  .WithOne(p => p.Quiz)
                                  .HasForeignKey<Quiz>(q => q.SectionId);

            builder.Entity<Portfolio>().HasOne(p => p.User)
                                       .WithOne(u => u.Portfolio)
                                       .HasForeignKey<Portfolio>(p => p.UserId);

            builder.Entity<User>().HasMany(c => c.CommunityUsers)
                                  .WithOne(c => c.User)
                                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(c => c.CommunityConnectors)
                                  .WithOne(c => c.Connector)
                                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(c => c.UserConversations)
                                  .WithOne(c => c.User)
                                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(c => c.ConnectorConversations)
                                  .WithOne(c => c.Connector)
                                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(c => c.UserQuesAnswers)
                                  .WithOne(c => c.User)
                                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Lecture>().HasMany(c => c.Comments)
                                   .WithOne(c => c.Video)
                                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comment>().HasMany(c => c.SubComments)
                                     .WithOne(c => c.MainComment)
                                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Course>().HasMany(c => c.CourseUsers)
                                     .WithOne(c => c.Course)
                                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(c => c.CourseUsers)
                                     .WithOne(c => c.User)
                                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(c => c.TrainingProjects)
                                     .WithOne(c => c.Coach)
                                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RoadMapCategory>().HasMany(c => c.RoadMapCategories)
                                     .WithOne(c => c.RoadMapCategoryParent)
                                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>().HasMany(c => c.RoadMapCategories)
                                     .WithOne(c => c.Category)
                                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RoadMap>().HasMany(c => c.RoadMapCategories)
                                     .WithOne(c => c.RoadMap)
                                     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RoadMapCategory>().HasOne(c => c.RoadMapCategoryParent)
                                     .WithMany(c => c.RoadMapCategories)
                                     .OnDelete(DeleteBehavior.Restrict);
        }



        //public virtual void SoftDelete<TEntity>(IEnumerable<Guid> ids) where TEntity : class, IBaseEntity
        //{
        //    ids.ToList().ForEach(id =>
        //    {
        //        TEntity entity = Find<TEntity>(id);
        //        entity.DateDeleted = DateTime.Now.ToLocalTime();
        //    });
        //}

        protected bool IsLogged()
        {
            if (httpContextAccessor?.HttpContext?.User != null)
                return httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
            return false;
        }

        public Guid CurrentUserId()
        {
            var userId = Guid.Empty;
            if (IsLogged())
            {
                userId = Guid.Parse(httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value);
            }
            return userId;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var actionBy = this.CurrentUserId();
            var Entries = ChangeTracker.Entries();
            foreach (EntityEntry entry in Entries.ToList())
            {
                IBaseEntity entity = entry.Entity as IBaseEntity;
                var actionType = entry.State;

                if (entity is null) continue;

                switch (entry.State)
                {
                    case EntityState.Detached:
                        break;

                    case EntityState.Unchanged:
                        break;

                    case EntityState.Modified:
                        entity.UpdatedId = actionBy;
                        entity.DateUpdated = DateTime.Now.ToLocalTime();
                        break;

                    case EntityState.Added:
                        entity.CreatedId = actionBy;
                        entity.DateCreated = DateTime.Now.ToLocalTime();
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);

        }

    }
}
