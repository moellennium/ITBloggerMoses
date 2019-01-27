using ITBloggerMoses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITBloggerMoses.Helpers
{
    public class Utilities
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        public static string GetAuthorName(string authorId)
        {
            return db.Users.Find(authorId).DisplayName;
        }
    }
}