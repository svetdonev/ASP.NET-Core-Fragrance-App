﻿using Fragrance_Web_App.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fragrance_Web_App.Data
{
    public class FragranceAppDbContext : IdentityDbContext<User>
    {
        public FragranceAppDbContext(DbContextOptions<FragranceAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Fragrance>()
                .HasKey(f => f.Id);

            builder.Entity<Review>()
                .HasOne(f => f.Author)
                .WithMany(f => f.Reviews)
                .HasForeignKey(f => f.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(f => f.Fragrance)
                .WithMany(f => f.Reviews)
                .HasForeignKey(f => f.FragranceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserReview>()
                .HasKey(ur => new { ur.UserId, ur.ReviewId });

            builder.Entity<UserReview>()
                .HasOne(ur => ur.User)
                .WithMany(ur => ur.UserReviews)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserReview>()
                .HasOne(ur => ur.Review)
                .WithMany(ur => ur.UserReviews)
                .HasForeignKey(ur => ur.ReviewId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasKey(c => c.Id);

            builder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Note>()
                .HasKey(n => n.Id);

            builder.Entity<Note>()
                .Property(n => n.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Fragrance>()
                .HasOne(f => f.Category)
                .WithMany(f => f.Fragrances)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<FragranceNote>()
                .HasKey(fn => new { fn.NoteId, fn.FragranceId });

            builder.Entity<FragranceNote>()
                .HasOne(fn => fn.Fragrance)
                .WithMany(f => f.FragranceNotes)
                .HasForeignKey(fn => fn.FragranceId);

            builder.Entity<FragranceNote>()
                .HasOne(fn => fn.Note)
                .WithMany(n => n.FragranceNotes)
                .HasForeignKey(fn => fn.NoteId);

            base.OnModelCreating(builder);
        }
        public DbSet<Fragrance> Fragrances { get; init; }
        public DbSet<Category> Categories { get; init; }
        public DbSet<Review> Reviews { get; init; }
        public DbSet<UserReview> UserReviews { get; init; }
        public DbSet<Note> Notes { get; init; }
        public DbSet<FragranceNote> FragranceNotes { get; init; }
    }
}
