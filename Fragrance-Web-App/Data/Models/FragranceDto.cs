﻿namespace Fragrance_Web_App.Data.Models
{
    public class FragranceDto
    {
        public string Id { get; init; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public IEnumerable<Note> Notes { get; set; }
    }
}