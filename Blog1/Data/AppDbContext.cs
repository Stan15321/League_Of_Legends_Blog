using Blog1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog1.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions  options)
            :base(options)
        { 

        }
        public DbSet<Post> Posts { get; set; }
    }
}
