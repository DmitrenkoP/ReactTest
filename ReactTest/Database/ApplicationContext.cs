using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReactTest.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ReactTest.Database
{
	public class ApplicationContext : DbContext

	{
		public DbSet<Node> Nodes { get; set; }
		public DbSet<StoredException> Exceptions { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>(NodesConfigure);
            modelBuilder.Entity<StoredException>(ExceptionsConfigure);
        }

        public void NodesConfigure(EntityTypeBuilder<Node> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId);
        }

        public void ExceptionsConfigure(EntityTypeBuilder<StoredException> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}

