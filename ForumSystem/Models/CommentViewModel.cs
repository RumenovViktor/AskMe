namespace ForumSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CommentViewModel
    {
        public int CommentId { get; set; }

        [Required]
        public string CommentContent { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public DateTime PostDate { get; set; }
    }
}