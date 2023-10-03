using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Numerics;
using Vidyalaya.Models;

namespace Vidyalaya.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<School> school { get; set; }
        public DbSet<Class> cclass { get; set; }
        public DbSet<Teachers> teachers { get; set; }
        public DbSet<Activities> activities { get; set; }
    }
}
