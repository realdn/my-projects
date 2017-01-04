using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult Random()
        {
            Movie movie = new Movie();
            movie.Id = 001;
            movie.Name = "007 James Bond";                     
            return View(movie);
                                 
        }


        public ActionResult Edit(int id)
        {
            return Content("Id = " + id);           
        }
        




    }
}