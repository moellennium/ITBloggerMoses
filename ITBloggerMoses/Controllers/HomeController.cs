using ITBloggerMoses.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;


namespace ITBloggerMoses.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();
        // GET: BlogPosts
        public ActionResult Index(int? page, string searchStr)
        {
            ViewBag.Search = searchStr;
            var blogList = IndexSearch(searchStr);
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(blogList.ToPagedList(pageNumber, pageSize));
        }

        public IQueryable<BlogPost> IndexSearch(string searchStr)
        {
            IQueryable<BlogPost> result = null;
            result = _db.BlogPosts.AsQueryable();

            if (searchStr != null)
            {
                result = result.Where(p => p.Title.Contains(searchStr) ||
                                           p.Body.Contains(searchStr) ||
                                           p.Comments.Any(c => c.Body.Contains(searchStr) ||
                                                              c.Author.FirstName.Contains(searchStr) ||
                                                              c.Author.LastName.Contains(searchStr) ||
                                                              c.Author.DisplayName.Contains(searchStr) ||
                                                              c.Author.Email.Contains(searchStr)));
            }
            return result.OrderByDescending(p => p.Created);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var body = "<p>Email from: <bold>{0}</bold> | Phone: <bold>{1}</bold>({2})</p><p>Message:</p><p>{3}</p>";
                    var from = "ITBlogger<moellennium@gmail.com>";
                    model.Body = "This is a message from your ITBlogger site . The name and the email of the contacting person above." + model.Body;

                    var email = new MailMessage(from, ConfigurationManager.AppSettings["emailto"])
                    {
                        Subject = "Portfolio Contact Email",
                        Body = string.Format(body, model.FromName, model.Phone, model.FromEmail, model.Body),
                        IsBodyHtml = true
                    };

                    var svc = new PersonalEmail();
                    await svc.SendAsync(email);

                    return RedirectToAction("Index");
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await Task.FromResult(0);
                }
            }
            return View(model);
        }


        //public ActionResult Index()
        //{
        //    var blogPosts = _db.BlogPosts.ToList();
        //    return View(blogPosts);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SamplePost()
        {
            ViewBag.Message = "Your sample post.";

            return View();    

        }
    }
}