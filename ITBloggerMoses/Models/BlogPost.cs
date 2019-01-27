using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ITBloggerMoses.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }

        [AllowHtml]
        public string Body { get; set; }

        public string MediaUrl { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        //Navigational Properties
        public virtual ICollection<Comment> Comments { get; set; }

        //Constructor method - Used to instantiate the Comments ICollection
        public BlogPost()
        {
            this.Comments = new HashSet<Comment>();
        }

    }
}