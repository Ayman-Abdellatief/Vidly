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

        private ApplicationDbContext _Context;

        public MoviesController()
        {

            _Context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _Context.Dispose();
        }
        // GET: Movies
        public ActionResult Index()
        {
            var Movies = _Context.Movies.Include(m => m.Genre).ToList();
    
            return View(Movies);
        }

        public ActionResult Details(int? id)
        {
            var Movie = _Context.Movies.Include(m => m.Genre).SingleOrDefault(M => M.Id == id);

            return View(Movie);

        }

        //IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie{Id=1,Name="Sherak"},
        //        new Movie{Id = 2 , Name="The Last Sumari"}
        //    };

        //}

         

        public ActionResult New()
        {
            var Genres = _Context.Genres.ToList();
            var viewmodel = new MovieFormViewModel
            {
            
                Genres = Genres
            };
            return View("MovieForm", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie Movie)
        {

            if (!ModelState.IsValid)
            {
                var viewmodel = new MovieFormViewModel(Movie)
                {
                  
                    Genres = _Context.Genres
                };
                return View("MovieForm", viewmodel);
            }
            if (Movie.Id == 0)
            {
                Movie.DateAdded = DateTime.Now;
                _Context.Movies.Add(Movie);
            }
            else
            {
                var MovieInDb = _Context.Movies.Single(m => m.Id == Movie.Id);

                MovieInDb.Name = Movie.Name;
                MovieInDb.RelaseDate = Movie.RelaseDate;
                MovieInDb.GenreId = Movie.GenreId;
                MovieInDb.NumberInStock = Movie.NumberInStock;
            }
            _Context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }


        public ActionResult Edit(int Id)
        {
            var Movie = _Context.Movies.SingleOrDefault(c => c.Id == Id);

            if (Movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(Movie)
            {
            
                Genres = _Context.Genres
            };
            return View("MovieForm", viewModel);
        }
    }
}