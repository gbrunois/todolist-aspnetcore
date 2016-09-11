using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Web.MoviesApi.Models;

namespace Web.MoviesApi.Controllers
{
    [Route("server/api/[controller]")]
    public class MoviesController : Controller
    {
        // GET server/api/movies
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return Data.MoviesDAO.GetMovies();
        }

        // GET server/api/movies/:id
        [HttpGet("{id}")]
        public Movie Get(string id)
        {
            return Data.MoviesDAO.GetMovie(Guid.Parse(id));
        }

        // POST server/api/movies
        [HttpPost]
        public void Post([FromBody]Movie value)
        {
            Data.MoviesDAO.InsertMovie(value);
        }

        // PUT server/api/movies
        [HttpPut]
        public void Put([FromBody]Movie value)
        {
            Movie movie = Data.MoviesDAO.GetMovie(value.Id);
            if (movie != null)
            {
                Data.MoviesDAO.UpdateMovie(value);
            }
        }

        // DELETE server/api/movies/:id
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Data.MoviesDAO.DeleteMovie(Guid.Parse(id));
        }
    }

}