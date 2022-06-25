using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace BorisKnowsAllApi.Data
{
    public class BorisKnowsAllApiContext : DbContext
    {
        public BorisKnowsAllApiContext (DbContextOptions<BorisKnowsAllApiContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.User>? User { get; set; }

        public DbSet<Domain.Contact> Contact { get; set; }

        public DbSet<Domain.OurMessage> OurMessage { get; set; }
    }
}
