﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Trainer.Data;

namespace Trainer.Migrations
{
    [DbContext(typeof(TrainingContext))]
    [Migration("20211009151044_MaxLengthOnNames")]
    partial class MaxLengthOnNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Trainer.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CurrentWeight")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StartWeight")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Trainer.Models.Exercise", b =>
                {
                    b.Property<int>("ExerciseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MuscleGroup")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExerciseID");

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("Trainer.Models.Training", b =>
                {
                    b.Property<int>("TrainingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.HasKey("TrainingID");

                    b.HasIndex("ClientID");

                    b.ToTable("Training");
                });

            modelBuilder.Entity("Trainer.Models.TrainingExercise", b =>
                {
                    b.Property<int>("TrainingExerciseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExerciseID")
                        .HasColumnType("int");

                    b.Property<int>("MaxWeight")
                        .HasColumnType("int");

                    b.Property<int>("Repetitions")
                        .HasColumnType("int");

                    b.Property<int>("Rounds")
                        .HasColumnType("int");

                    b.Property<int>("TrainingID")
                        .HasColumnType("int");

                    b.HasKey("TrainingExerciseID");

                    b.HasIndex("ExerciseID");

                    b.HasIndex("TrainingID");

                    b.ToTable("TrainingExercise");
                });

            modelBuilder.Entity("Trainer.Models.Training", b =>
                {
                    b.HasOne("Trainer.Models.Client", "Client")
                        .WithMany("Trainings")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Trainer.Models.TrainingExercise", b =>
                {
                    b.HasOne("Trainer.Models.Exercise", "Exercise")
                        .WithMany("TrainingExercises")
                        .HasForeignKey("ExerciseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Trainer.Models.Training", "Training")
                        .WithMany("TrainingExercises")
                        .HasForeignKey("TrainingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("Trainer.Models.Client", b =>
                {
                    b.Navigation("Trainings");
                });

            modelBuilder.Entity("Trainer.Models.Exercise", b =>
                {
                    b.Navigation("TrainingExercises");
                });

            modelBuilder.Entity("Trainer.Models.Training", b =>
                {
                    b.Navigation("TrainingExercises");
                });
#pragma warning restore 612, 618
        }
    }
}
