using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITBloggerMoses.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int BlogPostId { get; set; }

        public string AuthorId { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public string UpdateReason { get; set; }

        //Navugational Properties - Parents of the Comment record 

        //Which Blop Post do I belong to....
        public virtual BlogPost BlogPost { get; set; }

        //Who wrote me...some registered user found in the Users table
        public virtual ApplicationUser Author { get; set; }
    }
}