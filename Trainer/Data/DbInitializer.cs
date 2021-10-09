using Trainer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trainer.Data
{
    public class DbInitializer
    {

        public static void Initialize(TrainingContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }

            var clients = new Client[]
            {
            new Client{FirstName="Denise",LastName="Ellison",DateOfBirth=DateTime.Parse("2000-10-22"),Gender="F",StartWeight=67,CurrentWeight=65,Height=167,AdditionalInfo="Wants to lose weight and improve muscle tone."},
            new Client{FirstName="Timothy",LastName="Herring",DateOfBirth=DateTime.Parse("1992-01-05"),Gender="M",StartWeight=110,CurrentWeight=97,Height=185,AdditionalInfo="Purpose is to lose fat and be in better shape. Pain in knees."},
            new Client{FirstName="Josh",LastName="Goddard",DateOfBirth=DateTime.Parse("1986-12-10"),Gender="M",StartWeight=89,CurrentWeight=89,Height=189,AdditionalInfo="Wants to build muscle and lose belly fat."},
            new Client{FirstName="Eric",LastName="Whitley",DateOfBirth=DateTime.Parse("1995-06-18"),Gender="M",StartWeight=96,CurrentWeight=90,Height=179,AdditionalInfo="Trains for marathon, wants to improve muscle structure."},
            new Client{FirstName="Tallulah",LastName="Tanner",DateOfBirth=DateTime.Parse("1978-04-04"),Gender="F",StartWeight=79,CurrentWeight=71,Height=164,AdditionalInfo="Wants to lose baby weight. Abdomen diastasis."},
            new Client{FirstName="Mae",LastName="Ventura",DateOfBirth=DateTime.Parse("2002-02-27"),Gender="F",StartWeight=63,CurrentWeight=59,Height=158,AdditionalInfo="Interested in body fitness, wants to build muscle and lose fat."},
            new Client{FirstName="Rufus",LastName="Burke",DateOfBirth=DateTime.Parse("1982-06-30"),Gender="M",StartWeight=125,CurrentWeight=119,Height=192,AdditionalInfo="Wants to be in better shape, a lot of aerobic training besides muscle building."},
            new Client{FirstName="Stephan",LastName="Nash",DateOfBirth=DateTime.Parse("1989-11-19"),Gender="M",StartWeight=75,CurrentWeight=80,Height=187,AdditionalInfo="Needs to build muscle and increase weight. Likes diversity in trainings."},
            new Client{FirstName="Carly",LastName="Sinclair",DateOfBirth=DateTime.Parse("1998-01-02"),Gender="F",StartWeight=60,CurrentWeight=62,Height=176,AdditionalInfo="Wants to have nice muscles and have fun at training."}
            };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            var trainings = new Training[]
             {
            new Training{Date=DateTime.Parse("2021-08-02"),ClientID=1},
            new Training{Date=DateTime.Parse("2021-09-10"),ClientID=1},
            new Training{Date=DateTime.Parse("2021-08-19"),ClientID=2},
            new Training{Date=DateTime.Parse("2021-09-10"),ClientID=2},
            new Training{Date=DateTime.Parse("2021-08-14"),ClientID=3},
            new Training{Date=DateTime.Parse("2021-08-19"),ClientID=3},
            new Training{Date=DateTime.Parse("2021-08-29"),ClientID=3},
            new Training{Date=DateTime.Parse("2021-08-19"),ClientID=4},
            new Training{Date=DateTime.Parse("2021-08-29"),ClientID=4},
            new Training{Date=DateTime.Parse("2021-08-29"),ClientID=5},
            new Training{Date=DateTime.Parse("2021-08-02"),ClientID=6},
            new Training{Date=DateTime.Parse("2021-08-19"),ClientID=6},
            new Training{Date=DateTime.Parse("2021-08-02"),ClientID=7},
            new Training{Date=DateTime.Parse("2021-08-14"),ClientID=7},
            new Training{Date=DateTime.Parse("2021-08-19"),ClientID=7},
            new Training{Date=DateTime.Parse("2021-08-02"),ClientID=8},
            new Training{Date=DateTime.Parse("2021-08-14"),ClientID=9},
            };
            foreach (Training t in trainings)
            {
                context.Trainings.Add(t);
            }
            context.SaveChanges();

            var exercises = new Exercise[]
            {
            new Exercise{Title = "Wide pushaps", MuscleGroup=MuscleGroup.Chest},
            new Exercise{Title = "Narrow pushaps", MuscleGroup=MuscleGroup.Arms},
            new Exercise{Title = "Squats", MuscleGroup=MuscleGroup.Legs},
            new Exercise{Title = "Deadlifts", MuscleGroup=MuscleGroup.Back},
            new Exercise{Title = "Pull-ups on machine", MuscleGroup=MuscleGroup.Back},
            new Exercise{Title = "Overhead press", MuscleGroup=MuscleGroup.Arms},
            new Exercise{Title = "Reverse grip/close grip bench press", MuscleGroup=MuscleGroup.Arms},
            new Exercise{Title = "Close grip pull-up", MuscleGroup=MuscleGroup.Arms},
            new Exercise{Title = "Sit-ups", MuscleGroup=MuscleGroup.Abdominals},
            new Exercise{Title = "Cable chest press", MuscleGroup=MuscleGroup.Chest},
            new Exercise{Title = "Flat machine bench press", MuscleGroup=MuscleGroup.Chest},
            new Exercise{Title = "Bent over rows", MuscleGroup=MuscleGroup.Back},
            new Exercise{Title = "Leg raises",MuscleGroup=MuscleGroup.Legs},
            new Exercise{Title = "Plank hold", MuscleGroup=MuscleGroup.Abdominals},
            new Exercise{Title = "Romanian deadlifts", MuscleGroup=MuscleGroup.Legs},
            new Exercise{Title = "Leg press", MuscleGroup=MuscleGroup.Legs},
            new Exercise{Title = "Push press", MuscleGroup=MuscleGroup.Shoulders},
            new Exercise{Title = "Arnold press", MuscleGroup=MuscleGroup.Shoulders},
            new Exercise{Title = "Dumbbell front raises", MuscleGroup=MuscleGroup.Shoulders},
            };
            foreach (Exercise e in exercises)
            {
                context.Exercises.Add(e);
            }
            context.SaveChanges();

            var trainingExercises = new TrainingExercise[]
            {
            new TrainingExercise{TrainingID=1,ExerciseID=1,Rounds=3,Repetitions=10,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=1,ExerciseID=3,Rounds=3,Repetitions=12,MaxWeight=10,Comments="ok"},
            new TrainingExercise{TrainingID=1,ExerciseID=8,Rounds=3,Repetitions=16,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=1,ExerciseID=19,Rounds=3,Repetitions=12,MaxWeight=20,Comments="too hard"},
            new TrainingExercise{TrainingID=1,ExerciseID=12,Rounds=3,Repetitions=12,MaxWeight=5,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=2,ExerciseID=2,Rounds=3,Repetitions=10,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=2,ExerciseID=5,Rounds=3,Repetitions=12,MaxWeight=47,Comments="ok"},
            new TrainingExercise{TrainingID=2,ExerciseID=9,Rounds=3,Repetitions=16,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=2,ExerciseID=4,Rounds=3,Repetitions=12,MaxWeight=39,Comments="too hard"},
            new TrainingExercise{TrainingID=2,ExerciseID=16,Rounds=3,Repetitions=12,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=3,ExerciseID=1,Rounds=3,Repetitions=12,MaxWeight=10,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=3,ExerciseID=2,Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=3,ExerciseID=3,Rounds=2,Repetitions=10,MaxWeight=15,Comments="ok"},
            new TrainingExercise{TrainingID=3,ExerciseID=19,Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{TrainingID=4,ExerciseID=5,Rounds=3,Repetitions=10,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=4,ExerciseID=12,Rounds=3,Repetitions=10,MaxWeight=6,Comments="ok"},
            new TrainingExercise{TrainingID=4,ExerciseID=13,Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=4,ExerciseID=1,Rounds=2,Repetitions=12,MaxWeight=8,Comments="too hard"},
            new TrainingExercise{TrainingID=4,ExerciseID=9,Rounds=3,Repetitions=16,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=5,ExerciseID=7,Rounds=3,Repetitions=12,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=5,ExerciseID=8,Rounds=3,Repetitions=10,MaxWeight=12,Comments="ok"},
            new TrainingExercise{TrainingID=5,ExerciseID=14,Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=5,ExerciseID=15,Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{TrainingID=5,ExerciseID=18,Rounds=3,Repetitions=10,MaxWeight=10,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=6,ExerciseID=2,Rounds=3,Repetitions=12,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=6,ExerciseID=5,Rounds=2,Repetitions=12,MaxWeight=4,Comments="ok"},
            new TrainingExercise{TrainingID=6,ExerciseID=10,Rounds=3,Repetitions=10,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=6,ExerciseID=3,Rounds=3,Repetitions=12,MaxWeight=20,Comments="too hard"},
            new TrainingExercise{TrainingID=6,ExerciseID=16,Rounds=1,Repetitions=10,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=7,ExerciseID=6,Rounds=3,Repetitions=12,MaxWeight=10,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=7,ExerciseID=9,Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=7,ExerciseID=8,Rounds=3,Repetitions=10,MaxWeight=16,Comments="too hard"},
            new TrainingExercise{TrainingID=7,ExerciseID=11,Rounds=3,Repetitions=16,MaxWeight=10,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=8,ExerciseID=13,Rounds=3,Repetitions=12,MaxWeight=16,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=8,ExerciseID=2,Rounds=2,Repetitions=10,MaxWeight=10,Comments="ok"},
            new TrainingExercise{TrainingID=8,ExerciseID=6,Rounds=3,Repetitions=12,MaxWeight=12,Comments="ok"},
            new TrainingExercise{TrainingID=8,ExerciseID=17,Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{TrainingID=9,ExerciseID=2,Rounds=2,Repetitions=12,MaxWeight=10,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=9,ExerciseID=5,Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=9,ExerciseID=9,Rounds=3,Repetitions=10,MaxWeight=12,Comments="ok"},
            new TrainingExercise{TrainingID=9,ExerciseID=4,Rounds=2,Repetitions=12,MaxWeight=10,Comments="too hard"},
            new TrainingExercise{TrainingID=9,ExerciseID=16,Rounds=3,Repetitions=12,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=10,ExerciseID=7,Rounds=3,Repetitions=12,MaxWeight=8,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=10,ExerciseID=4,Rounds=3,Repetitions=16,MaxWeight=10,Comments="ok"},
            new TrainingExercise{TrainingID=10,ExerciseID=8,Rounds=2,Repetitions=6,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=10,ExerciseID=19,Rounds=3,Repetitions=12,MaxWeight=10,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=11,ExerciseID=11,Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=11,ExerciseID=10,Rounds=2,Repetitions=12,MaxWeight=8,Comments="ok"},
            new TrainingExercise{TrainingID=11,ExerciseID=4,Rounds=3,Repetitions=16,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{TrainingID=11,ExerciseID=6,Rounds=2,Repetitions=12,MaxWeight=18,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=12,ExerciseID=3,Rounds=2,Repetitions=10,MaxWeight=10,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=12,ExerciseID=14,Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=12,ExerciseID=15,Rounds=3,Repetitions=16,MaxWeight=20,Comments="ok"},
            new TrainingExercise{TrainingID=12,ExerciseID=16,Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{TrainingID=12,ExerciseID=1,Rounds=3,Repetitions=12,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=13,ExerciseID=3,Rounds=3,Repetitions=12,MaxWeight=14,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=13,ExerciseID=12,Rounds=2,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=13,ExerciseID=18,Rounds=3,Repetitions=10,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{TrainingID=13,ExerciseID=17,Rounds=3,Repetitions=10,MaxWeight=16,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=14,ExerciseID=4,Rounds=3,Repetitions=12,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=14,ExerciseID=8,Rounds=3,Repetitions=12,MaxWeight=14,Comments="ok"},
            new TrainingExercise{TrainingID=14,ExerciseID=2,Rounds=3,Repetitions=10,MaxWeight=6,Comments="ok"},
            new TrainingExercise{TrainingID=14,ExerciseID=11,Rounds=2,Repetitions=8,MaxWeight=8,Comments="too hard"},
            new TrainingExercise{TrainingID=14,ExerciseID=14,Rounds=3,Repetitions=10,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=15,ExerciseID=18,Rounds=3,Repetitions=12,MaxWeight=20,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=15,ExerciseID=6,Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=15,ExerciseID=5,Rounds=3,Repetitions=10,MaxWeight=10,Comments="too hard"},
            new TrainingExercise{TrainingID=15,ExerciseID=14,Rounds=3,Repetitions=162,MaxWeight=45,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=16,ExerciseID=1,Rounds=3,Repetitions=12,MaxWeight=22,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=16,ExerciseID=13,Rounds=3,Repetitions=12,MaxWeight=10,Comments="ok"},
            new TrainingExercise{TrainingID=16,ExerciseID=6,Rounds=2,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{TrainingID=16,ExerciseID=8,Rounds=3,Repetitions=10,MaxWeight=15,Comments="too hard"},
            new TrainingExercise{TrainingID=16,ExerciseID=10,Rounds=3,Repetitions=12,MaxWeight=10,Comments="more repetitions next time"},
            new TrainingExercise{TrainingID=17,ExerciseID=4,Rounds=3,Repetitions=12,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{TrainingID=17,ExerciseID=12,Rounds=2,Repetitions=10,MaxWeight=22,Comments="ok"},
            new TrainingExercise{TrainingID=17,ExerciseID=9,Rounds=3,Repetitions=16,MaxWeight=10,Comments="ok"},
            new TrainingExercise{TrainingID=17,ExerciseID=5,Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{TrainingID=17,ExerciseID=18,Rounds=2,Repetitions=10,MaxWeight=25,Comments="too hard"},
            };
            foreach (TrainingExercise te in trainingExercises)
            {
                context.TrainingExercises.Add(te);
            }
            context.SaveChanges();
        }
    }
}

