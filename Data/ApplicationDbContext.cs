﻿using BlogEngineWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogEngineWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
