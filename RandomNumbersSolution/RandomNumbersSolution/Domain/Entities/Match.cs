﻿using RandomNumbersSolution.Models;
using System;
using System.Collections.Generic;

namespace RandomNumbersSolution.Domain.Entities
{

    public class Match
    {
        public int Id { get; set; }
        public DateTime Expiration { get; set; }
        public string WinUserName { get; set; }
        public List<MatchItem> Items { get; set; }
    }
}