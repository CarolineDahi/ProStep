using Microsoft.AspNetCore.Identity;
using ProStep.Model.Courses;
using ProStep.Model.General;
using ProStep.Model.Profile;
using ProStep.Model.Quizzes;
using ProStep.Model.TrainingProjects;
using ProStep.SharedKernel.Enums.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProStep.Model.Security
{
    public class User : IdentityUser<Guid>
    {
        public User() 
        { 
            NotificationUsers = new HashSet<NotificationUser>();
            UserQuesAnswers = new HashSet<UserQuesAnswer>();
            UserConversations = new HashSet<Conversation>();
            ConnectorConversations = new HashSet<Conversation>();
        }
        public string? GenerationStamp { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? BDay { get; set; }
        public UserType UserType { get; set; }
        public string? ImagePath { get; set; }
        public string? DeviceToken { get; set; }

        public Guid? PortfolioId { get; set; }
        [ForeignKey(nameof(PortfolioId))]
        public Portfolio Portfolio { get; set; }
        
        public ICollection<NotificationUser> NotificationUsers { get; set; }
        public ICollection<UserQuesAnswer> UserQuesAnswers { get; set; }

        [InverseProperty(nameof(Conversation.User))]
        public ICollection<Conversation> UserConversations { get; set; }

        [InverseProperty(nameof(Conversation.Connector))]
        public ICollection<Conversation> ConnectorConversations { get; set; }

        [InverseProperty(nameof(Community.User))]
        public ICollection<Community> CommunityUsers { get; set; }

        [InverseProperty(nameof(Community.Connector))]
        public ICollection<Community> CommunityConnectors { get; set; }

        [InverseProperty(nameof(Course.Coach))]
        public ICollection<Course> Courses { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<ToDoUser> ToDoUsers { get; set; }
        public ICollection<TaskUser> TaskUsers { get; set; }
        public ICollection<BootcampUser> BootcampUsers { get; set; }
        public ICollection<CourseUser> CourseUsers { get; set; }
        public ICollection<ProjectUser> ProjectUsers { get; set; }
        public ICollection<TrainingProject> TrainingProjects { get; set; }
        public ICollection<UserFile> UserFiles { get; set; }

        #region - Base -
        public Guid CreaterId { get; set; }
        public Guid? ModifierId { get; set; }
        public Guid? DeleterId { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }
        #endregion
    }
}
