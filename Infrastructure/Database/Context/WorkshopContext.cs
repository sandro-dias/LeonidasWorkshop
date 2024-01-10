﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Context
{
    public class WorkshopContext : DbContext
    {
        public DbSet<Workshop> Workshop { get; set; }

        public WorkshopContext(DbContextOptions<WorkshopContext> options)
            : base(options)
        {

        }
        public WorkshopContext()
        {

        }
    }
}
