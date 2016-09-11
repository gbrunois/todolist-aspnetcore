using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.MoviesApi.Models;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using System;

namespace Web.MoviesApi.Data
{
    public static class MoviesDAO
    {
        private static IMongoDatabase db = InitMongo();

        private static IMongoDatabase InitMongo()
        {
            // or use a connection string
            var settings = new MongoClientSettings() {
                Credentials = new [] { MongoCredential.CreateCredential("angularmovies", "angularUser", "password") },
                Server = new MongoServerAddress("192.168.99.100",27017)
            };
            var client = new MongoClient(settings);
            return client.GetDatabase("angularmovies");
        }

         public class MoviesList
        {
            public Movie[] Movies { get; set; }
        }

        public static Movie[] GetMovies()
        {
            var table = db.GetCollection<Movie>("movies");
            var movies =  table.AsQueryable().Select(x => x);
            return movies.ToArray();
        }

        public static Movie GetMovie(Guid id)
        {
            return GetMovies().Where(movie => movie.Id == id).FirstOrDefault();
        }

        public static void InsertMovie(Movie movie)
        {
            db.GetCollection<Movie>("movies").InsertOne(movie);
        }

        internal static void DeleteMovie(Guid id)
        {
            
        }

        public static void UpdateMovie(Movie movie)
        {
            var filter = new BsonDocument("_id", movie.Id);
            db.GetCollection<Movie>("movies").ReplaceOne(filter, movie);
        }
    }
}