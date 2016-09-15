using System;
using System.Threading.Tasks;
using Web.MoviesApi.Models;

namespace Web.MoviesApi.Repositories.Interface
{
    public interface IMoviesRepository
    {
        Task<Movie[]> GetMovies();
        Task<Movie> GetMovie(Guid id);

        Task InsertMovie(Movie movie);

        Task DeleteMovie(Guid id);

        Task UpdateMovie(Movie movie);
    }
}