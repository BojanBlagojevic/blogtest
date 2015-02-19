using BlogZadatak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogZadatak.Controllers
{
    public class BlogController : Controller
    {
       

        public ActionResult Index()
        {

            MvcBlogEntities4 kategorijecontext = new MvcBlogEntities4();
            List<Blog> blogovi = kategorijecontext.Blogs.ToList();

            return View(blogovi);
          
        }

        public ActionResult SelectBlog(int id)
        {

           // MvcBlogEntities4 kategorijecontext = new MvcBlogEntities4();
           //List<Blog> blogovi = kategorijecontext.Blogs.Where(p=>p.Categoryid==id).ToList();

           // return View(blogovi);
            return View();

        }
       

        [HttpGet]
        public ActionResult Create()
        {
            MvcBlogEntities4 kategorijecontext = new MvcBlogEntities4();

            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Blog blog)
        {
            using (var db = new MvcBlogEntities4())
            {
                Category cat= new Category();
               

                var noviblog = db.Blogs.Create();

                
                noviblog.Name = blog.Name;
                noviblog.Content = blog.Content;
                db.Categories.FirstOrDefault(x => x.Categoryid == 1);
               

                db.Blogs.Add(noviblog);
                db.SaveChanges();


                return RedirectToAction("Index", "Blog");
            }



        }

    }
}
