using Trainer.Models;
using Microsoft.EntityFrameworkCore;

namespace Trainer.Data
{
    public class TrainingContext : DbContext
    {
        public TrainingContext()
        {
        }
        public TrainingContext(DbContextOptions<TrainingContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<TrainingExercise> TrainingExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Training>().ToTable("Training");
            modelBuilder.Entity<Exercise>().ToTable("Exercise");
            modelBuilder.Entity<TrainingExercise>().ToTable("TrainingExercise");
        }
    }
}
