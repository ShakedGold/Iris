﻿using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashboardBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpdatesConsumer;

namespace DashboardBackend.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public DbSet<Update> Updates { get; set; }
        
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Update>(
                builder =>
                {
                    builder.HasKey(u => u.Idd);
                    builder.Ignore(u => u.Media);
                });
        }
    }
}