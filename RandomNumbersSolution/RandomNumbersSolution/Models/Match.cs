using RandomNumbersSolution.Models;
using System;
using System.Collections.Generic;

namespace RandomNumbersSolution.Models
{
    public class Match
    {
        public Match()
        {
            Items = new List<MatchItem>();
        }
        public int Id { get; set; }
        public DateTime Expired { get; set; }
        public string WinUserName { get; set; }
        public List<MatchItem> Items { get; set; }
    }
}