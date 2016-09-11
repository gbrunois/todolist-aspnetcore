using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Web.MoviesApi.Models
{
    public class Movie
    {
        public Movie() {

        }    

        [BsonId(IdGenerator = typeof(MongoDB.Bson.Serialization.IdGenerators.GuidGenerator))]
        public Guid Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("category")]
        public string Category { get; set; } 

        [BsonElement("releaseYear")]  
        public int ReleaseYear { get; set; }

        [BsonElement("poster")]
        public string Poster { get; set; }

        [BsonElement("directors")]
        public string Directors { get; set; }

        [BsonElement("actors")]
        public string Actors { get; set; }

        [BsonElement("synopsis")]
        public string Synopsis { get; set; }

        [BsonElement("rate")]
        public int Rate { get; set; }

        [BsonElement("lastViewDate")]
        public DateTime LastViewDate { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}