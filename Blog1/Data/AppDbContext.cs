﻿using Blog1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog1.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions  options)
            :base(options)
        { 

        }
        public DbSet<Post> Posts { get; set; }
    }
}
