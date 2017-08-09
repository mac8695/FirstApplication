namespace FirstApplication.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DataContext : DbContext
    {
        public DataContext()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<GameGenre> GameGenres { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(e => e.Genres)
                .WithRequired(e => e.Game)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Games)
                .WithRequired(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rating>()
                .Property(e => e.Rank)
                .HasPrecision(18, 0);
        }
    }
}
