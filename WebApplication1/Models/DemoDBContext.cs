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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(
        //        @"Server=TOLGAHAN-PC;Database=DemoDB;Integrated Security=True");
        //}

        public DemoDBContext(DbContextOptions<DemoDBContext> options)
        : base(options)
        {
        }
    }
}