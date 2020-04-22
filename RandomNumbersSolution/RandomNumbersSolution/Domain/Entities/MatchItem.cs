﻿using RandomNumbersSolution.Models;

namespace RandomNumbersSolution.Domain.Entities
{
    public class MatchItem
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string UserName { get; set; }
        public int Number { get; set; }
    }
}