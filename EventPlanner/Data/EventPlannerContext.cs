using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;

namespace EventPlanner.Models
{
    public class EventPlannerContext : DbContext
    {
        public EventPlannerContext (DbContextOptions<EventPlannerContext> options)
            : base(options)
        {

        }

        public DbSet<EventPlanner.Models.User> User { get; set; }

        public DbSet<EventPlanner.Models.Accommodation> Accommodation { get; set; }

        public DbSet<EventPlanner.Models.Event> Event { get; set; }

        public DbSet<EventPlanner.Models.Location> Location { get; set; }
    }
}
