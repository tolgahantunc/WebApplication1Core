using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class DemoDBContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public DemoDBContext(DbContextOptions<DemoDBContext> options)
        : base(options)
        {
        }
    }
}