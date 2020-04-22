using System;
using System.Collections.Generic;

namespace RandomNumbersSolution.Models
{

    public class Match
    {
        public int Id { get; set; }
        public DateTime Expiration { get; set; }
        public List<MatchItem> Items { get; set; }
    }
}