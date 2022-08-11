using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Blog1.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(ContextOptions<AppDbContext> options)
            : base(options)
        {

        }
       public DbSet<Posts> MyProperty { get; set }
        public object Posts { get; internal set; }
    }

    public class ContextOptions<T>
    {
    }
}
