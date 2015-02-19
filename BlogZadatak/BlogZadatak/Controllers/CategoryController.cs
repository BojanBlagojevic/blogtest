using BlogZadatak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogZadatak.Controllers
{
    public class CategoryController : Controller
    {


        public ActionResult Index()
        {


            MvcBlogEntities4 kategorijecontext = new MvcBlogEntities4();
            List<Category> kategorije = kategorijecontext.Categories.ToList();

            return View(kategorije);
        }

        [HttpGet]
        public ActionResult Create()
        {
            MvcBlogEntities4 kategorijecontext = new MvcBlogEntities4();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            using (var db = new MvcBlogEntities4())
            {


                var novakategorija = db.Categories.Create();

                novakategorija.Name = category.Name;
                novakategorija.Description = category.Description;
                novakategorija.Date = category.Date;


                db.Categories.Add(novakategorija);
                db.SaveChanges();


                return RedirectToAction("Index", "Category");
            }


          
        }
    }
    

}
