namespace ForumSystem.Models
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class QuestionViewModel
    {
        private ICollection<Answer> answers;

        public QuestionViewModel()
        {
            this.answers = new HashSet<Answer>();
        }

        public int QuestionId { get; set; }

        [Required]
        //[Range(1, 100, ErrorMessage = "The question field should not be empty.")]
        public string Title { get; set; }

        [Required]
        //[Range(1, 500, ErrorMessage = "The description field should not be empty.")]
        public string QuestionContent { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public DateTime TimeOfCreation { get; set; }

        public virtual ICollection<Answer> Answers 
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}