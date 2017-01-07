using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity; 

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }


        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            //base.Dispose(disposing);
        }


        public ActionResult Index()
        {
            var movies = getMovie();
            return View(movies);
        }



        // GET: Movies
        public ActionResult Random()
        {
            Movie movie = new Movie();
            movie.Id = 001;
            movie.Name = "007 James Bond";


            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };

            RandomMovieViewModel viewModel = new RandomMovieViewModel();
            viewModel.movie = movie;
            viewModel.Customers = customers;


            ViewResult result = new ViewResult();

            result.ViewData.Model = viewModel;
                
            return result;                   
            
                                 
        }


      


        [Route("movies/released/{year}/{month}")]
        
        public ActionResult ByReleaseYear(int year, int month)
        {
            return Content(year.ToString()+month.ToString());
        }



        [Route("movies/details/{id}")]
        public ActionResult Details(int id)
        {
            

            var movie = _context.Movies.Include(m => m.MovieGenre).SingleOrDefault(m => m.Id == id); ;

            if (movie!=null)
            {
                ViewResult detailResult = View();
                detailResult.ViewData.Model = movie;
                return detailResult;
            }
            else
            {
                return new HttpNotFoundResult();

            }



        }


        public ActionResult New()
        {
            var viewmodel = new MovieFormViewModel() {
                MovieGenres = _context.MovieGenres.ToList()

            };         

            return View("MovieForm", viewmodel);
        }


        public ActionResult Edit(int id)
        {


            var viewModel = new MovieFormViewModel()
            {
                Movie = _context.Movies.Single(m => m.Id == id),
                MovieGenres = _context.MovieGenres.ToList()

            };


            return View("MovieForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDB = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDB.Name = movie.Name;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.MovieGenreId = movie.MovieGenreId;
                movieInDB.NumbersInStock = movie.NumbersInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");

          

        }




        private List<Movie> getMovie()
        {
            return _context.Movies.Include(m => m.MovieGenre).ToList(); ;

        }




    }
}