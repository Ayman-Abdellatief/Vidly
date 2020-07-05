using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;


        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        //Get/api/Movies
        public IHttpActionResult GetMovies()
        {
            var MovieDtos = _context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(MovieDtos);
        }

        //Get/api/Movie/1

        public IHttpActionResult GetMovie(int Id)
        {

            var Movie = _context.Movies.SingleOrDefault(m => m.Id == Id);
            if (Movie == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(Movie));
        }

        //post/api/Movie
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var Movie = Mapper.Map<MovieDto, Movie>(MovieDto);
            _context.Movies.Add(Movie);
            _context.SaveChanges();
            MovieDto.Id = Movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + Movie.Id), MovieDto);

        }

        //Put/api/Movie
        [HttpPut]
        public IHttpActionResult UpdateMovie(int Id, MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var MovieInDb = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (MovieInDb == null)
                return NotFound();

            Mapper.Map(MovieDto, MovieInDb);


            _context.SaveChanges();
            return Ok();
        }

        //Delete/api/Movie
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int Id)
        {
            var MovieInDb = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (MovieInDb == null)
                return NotFound();

            _context.Movies.Remove(MovieInDb);

            _context.SaveChanges();

            return Ok();
        }
    }
}
