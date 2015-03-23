namespace ForumSystem.Models
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Collections.Generic;

    public class QuestionViewModel
    {
        public string Title { get; set; }

        public string QuestionContent { get; set; }

        public DateTime TimeOfCreation { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}