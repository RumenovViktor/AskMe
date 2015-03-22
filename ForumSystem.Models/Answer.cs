namespace ForumSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Answer
    {
        private ICollection<Comment> comments;

        public Answer()
        {
            this.comments = new HashSet<Comment>();
        }

        [Key]
        public int AnswerId { get; set; }

        public string Content { get; set; }

        public DateTime PostDate { get; set; }

        public ICollection<Comment> Comments 
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
