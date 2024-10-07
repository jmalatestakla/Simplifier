namespace Simplifier.Entities
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class SimplifierContext : DbContext
    {
        public string DbPath { get; }

        // Constructor that accepts DbContextOptions<SimplifierContext>
        public SimplifierContext(DbContextOptions<SimplifierContext> options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "simplifier.db");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Template> Templates { get; set; }
    }
}