﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class CalendarDbContext : DbContext
    {
        public DbSet<CalendarEvent> Events { get; set; }

        public CalendarDbContext(DbContextOptions<CalendarDbContext> options) : base(options) { }
    }

    public class CalendarEvent
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string? Text { get; set; }
        public string? Color { get; set; }
    }
}
