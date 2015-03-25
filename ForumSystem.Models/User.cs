namespace ForumSystem.Models
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;

    public class User : IdentityUser
    {
        private ICollection<Question> questions;
        private ICollection<Answer> answers;
        
        public User()
        {
            this.questions = new HashSet<Question>();
            this.answers = new HashSet<Answer>();
        }

        [Required]
        public override string UserName { get; set; }

        public byte[] Image { get; set; }

        public virtual ICollection<Question> Questions 
        {
            get { return this.questions; }
            set { this.questions = value; } 
        }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; } 
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
