using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.MoviesApi.Models;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Web.MoviesApi.Data
{
    public static class MoviesDAO
    {
        private static void InitMongo()
        {
            // To directly connect to a single MongoDB server
            // (this will not auto-discover the primary even if it's a member of a replica set)
            var client = new MongoClient();

            // or use a connection string
            var client1 = new MongoClient("mongodb://localhost:27017");

            // or, to connect to a replica set, with auto-discovery of the primary, supply a seed list of members
            var client2 = new MongoClient("mongodb://localhost:27017,localhost:27018,localhost:27019");
        }

        private static Dictionary<int, Movie> _Movies = LoadMovies();
        public class MoviesList
        {
            public Movie[] Movies { get; set; }
        }

        public static Movie[] GetMovies()
        {
            return _Movies.Values.ToArray();
        }

        private static Dictionary<int, Movie> LoadMovies()
        {
            var strings = File.ReadAllText("Data/movies.json");
            var jsonList = JObject.Parse(strings);
            var movies = JsonConvert.DeserializeObject<MoviesList>(jsonList.ToString());
            return movies.Movies.ToDictionary(movie => movie.Id, movie => movie);
        }

        public static Movie GetMovie(int id)
        {
            return GetMovies().Where(movie => movie.Id == id).FirstOrDefault();
        }

        public static void InsertMovie(Movie movie)
        {
            var id = _Movies.Count + 1;
            _Movies.Add(id, movie);
        }

        internal static void DeleteMovie(int id)
        {
            _Movies.Remove(id);
        }

        public static void UpdateMovie(Movie movie)
        {
            _Movies[movie.Id] = movie;
        }
    }
}