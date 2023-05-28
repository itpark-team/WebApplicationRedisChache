using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationRedisChache.Db;

public partial class AlexTestJwtDbContext : DbContext
{
    public AlexTestJwtDbContext()
    {
    }

    public AlexTestJwtDbContext(DbContextOptions<AlexTestJwtDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=194.67.105.79:5432;Database=alex_test_jwt_db;Username=alex_test_jwt_user;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("articles_pk");

            entity.ToTable("articles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(1024)
                .HasColumnName("content");
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Articles)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("articles_authors_id_fk");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authors_pk");

            entity.ToTable("authors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("name");
        });
        
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
