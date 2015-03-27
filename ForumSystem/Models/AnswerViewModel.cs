namespace ForumSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AnswerViewModel
    {
        private ICollection<Comment> comments;

        public AnswerViewModel()
        {
            this.comments = new HashSet<Comment>();
        }

        public int AnswerId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime PostDate { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }

        public ICollection<Comment> Comments 
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}