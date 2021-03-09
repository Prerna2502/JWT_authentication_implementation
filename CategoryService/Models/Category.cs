using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CategoryService.Models
{
    public class Category
    {
        [BsonId]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
