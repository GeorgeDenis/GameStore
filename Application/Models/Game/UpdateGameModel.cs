using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Application.Models.Game
{
    public class UpdateGameModel
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public decimal Price { get; set; }  
        public IFormFile? Image { get; set; }  
    }
}
