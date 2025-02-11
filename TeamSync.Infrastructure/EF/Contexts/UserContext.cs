﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TeamSync.Domain.Entities.TaskEntities;

namespace TeamSync.Infrastructure.EF.Contexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add additional configurations if needed
            base.OnModelCreating(modelBuilder);
        }
    } 
}
