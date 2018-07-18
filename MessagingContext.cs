using System;
using Messaging.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace Messaging
{
  public class MessagingContext : DbContext
  {
    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      // TODO: Get config from CF config
      optionsBuilder.UseMySql("server=us-cdbr-iron-east-04.cleardb.net;database=ad_491555170060d96;user=b0f8cd67e89dcf;password=a09906d8");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Message>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.FromId).IsRequired();
        entity.Property(e => e.FromUserType).IsRequired();
        entity.Property(e => e.ToId).IsRequired();
        entity.Property(e => e.ToUserType).IsRequired();
        entity.Property(e => e.Title).IsRequired();
        entity.Property(e => e.CreatedDate).HasDefaultValue(DateTime.UtcNow);
      });
    }
  }
}