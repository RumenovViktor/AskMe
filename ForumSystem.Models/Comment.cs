namespace ForumSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public string CommentContent { get; set; }

        public int AnswerId { get; set; }

        public virtual Answer Answer { get; set; }

        public DateTime PostDate { get; set; }
    }
}
