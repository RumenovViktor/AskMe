namespace ForumSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        private ICollection<Question> questions;

        public Category()
        {
            this.questions = new HashSet<Question>();
        }

        [Key]
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Question> Questions 
        {
            get { return this.questions; }
            set { this.questions = value; }  
        }
    }
}
