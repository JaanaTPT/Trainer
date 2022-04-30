using System;
using System.Linq;
using Trainer.Models;

namespace Trainer.Data
{
    public class DbInitializer
    {

        public static void Initialize(TrainingContext context)
        {

            if (context.Clients.Any())
            {
                return;  
            }

            var clients = new Client[]
            {
            new Client{FirstName="Denise",LastName="Ellison",DateOfBirth=DateTime.Parse("2000-10-22"),Phone ="5111223",Email = "denise.ellison@gmail.com",Gender="F",StartWeight=67,CurrentWeight=65,Height=167,AdditionalInfo="Wants to lose weight and improve muscle tone."},
            new Client{FirstName="Timothy",LastName="Herring",DateOfBirth=DateTime.Parse("1992-01-05"), Phone ="5118965",Email = "tim.herring@gmail.com",Gender="M",StartWeight=110,CurrentWeight=97,Height=185,AdditionalInfo="Purpose is to lose fat and be in better shape. Pain in knees."},
            new Client{FirstName="Josh",LastName="Goddard",DateOfBirth=DateTime.Parse("1986-12-10"),Phone ="5023874",Email = "jgoddard@gmail.com",Gender="M",StartWeight=89,CurrentWeight=89,Height=189,AdditionalInfo="Wants to build muscle and lose belly fat."},
            new Client{FirstName="Eric",LastName="Whitley",DateOfBirth=DateTime.Parse("1995-06-18"),Phone ="56554770",Email = "eric.whitley@gmail.com",Gender="M",StartWeight=96,CurrentWeight=90,Height=179,AdditionalInfo="Trains for marathon, wants to improve muscle structure."},
            new Client{FirstName="Tallulah",LastName="Tanner",DateOfBirth=DateTime.Parse("1978-04-04"),Phone ="52339650",Email = "taltanner@gmail.com",Gender="F",StartWeight=79,CurrentWeight=71,Height=164,AdditionalInfo="Wants to lose baby weight. Abdomen diastasis."},
            new Client{FirstName="Mae",LastName="Ventura",DateOfBirth=DateTime.Parse("2002-02-27"),Phone ="50221559",Email = "mae.ventura@gmail.com",Gender="F",StartWeight=63,CurrentWeight=59,Height=158,AdditionalInfo="Interested in body fitness, wants to build muscle and lose fat."},
            new Client{FirstName="Rufus",LastName="Burke",DateOfBirth=DateTime.Parse("1982-06-30"),Phone ="55114552",Email = "rufus999@gmail.com",Gender="M",StartWeight=125,CurrentWeight=119,Height=192,AdditionalInfo="Wants to be in better shape, a lot of aerobic training besides muscle building."},
            new Client{FirstName="Stephan",LastName="Nash",DateOfBirth=DateTime.Parse("1989-11-19"),Phone ="50701490",Email = "stephen.nash@gmail.com",Gender="M",StartWeight=75,CurrentWeight=80,Height=187,AdditionalInfo="Needs to build muscle and increase weight. Likes diversity in trainings."},
            new Client{FirstName="Carly",LastName="Sinclair",DateOfBirth=DateTime.Parse("1998-01-02"),Phone ="52993644",Email = "csinclair@gmail.com",Gender="F",StartWeight=60,CurrentWeight=62,Height=176,AdditionalInfo="Wants to have nice muscles and have fun at training."}
            };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            var trainings = new Training[]
             {
            new Training{Date=DateTime.Parse("2022-03-02"),Client = clients[0]},
            new Training{Date=DateTime.Parse("2022-04-10"),Client = clients[1]},
            new Training{Date=DateTime.Parse("2022-03-19"),Client = clients[2]},
            new Training{Date=DateTime.Parse("2022-04-10"),Client = clients[3]},
            new Training{Date=DateTime.Parse("2022-03-14"),Client = clients[4]},
            new Training{Date=DateTime.Parse("2022-03-19"),Client = clients[5]},
            new Training{Date=DateTime.Parse("2022-03-29"),Client = clients[6]},
            new Training{Date=DateTime.Parse("2022-03-19"),Client = clients[7]},
            new Training{Date=DateTime.Parse("2022-03-29"),Client = clients[8]},
            new Training{Date=DateTime.Parse("2022-03-29"),Client = clients[0]},
            new Training{Date=DateTime.Parse("2022-03-02"),Client = clients[1]},
            new Training{Date=DateTime.Parse("2022-03-19"),Client = clients[2]},
            new Training{Date=DateTime.Parse("2022-03-02"),Client = clients[3]},
            new Training{Date=DateTime.Parse("2022-03-14"),Client = clients[4]},
            new Training{Date=DateTime.Parse("2022-03-19"),Client = clients[5]},
            new Training{Date=DateTime.Parse("2022-03-02"),Client = clients[6]},
            new Training{Date=DateTime.Parse("2022-03-14"),Client = clients[7]},
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
            new TrainingExercise{Training = trainings[0], Exercise = exercises[0],Rounds=3,Repetitions=10,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[0], Exercise = exercises[2],Rounds=3,Repetitions=12,MaxWeight=10,Comments="ok"},
            new TrainingExercise{Training = trainings[0], Exercise = exercises[7],Rounds=3,Repetitions=16,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[0], Exercise = exercises[18],Rounds=3,Repetitions=12,MaxWeight=20,Comments="too hard"},
            new TrainingExercise{Training = trainings[0], Exercise = exercises[11],Rounds=3,Repetitions=12,MaxWeight=5,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[1], Exercise = exercises[1],Rounds=3,Repetitions=10,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[1], Exercise = exercises[4],Rounds=3,Repetitions=12,MaxWeight=47,Comments="ok"},
            new TrainingExercise{Training = trainings[1], Exercise = exercises[8],Rounds=3,Repetitions=16,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[1], Exercise = exercises[3],Rounds=3,Repetitions=12,MaxWeight=39,Comments="too hard"},
            new TrainingExercise{Training = trainings[1], Exercise = exercises[15],Rounds=3,Repetitions=12,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[2], Exercise = exercises[0],Rounds=3,Repetitions=12,MaxWeight=10,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[2], Exercise = exercises[1],Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[2], Exercise = exercises[2],Rounds=2,Repetitions=10,MaxWeight=15,Comments="ok"},
            new TrainingExercise{Training = trainings[2], Exercise = exercises[18],Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{Training = trainings[3], Exercise = exercises[4],Rounds=3,Repetitions=10,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[3], Exercise = exercises[11],Rounds=3,Repetitions=10,MaxWeight=6,Comments="ok"},
            new TrainingExercise{Training = trainings[3], Exercise = exercises[12],Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[3], Exercise = exercises[0],Rounds=2,Repetitions=12,MaxWeight=8,Comments="too hard"},
            new TrainingExercise{Training = trainings[3], Exercise = exercises[8],Rounds=3,Repetitions=16,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[4], Exercise = exercises[7],Rounds=3,Repetitions=10,MaxWeight=12,Comments="ok"},
            new TrainingExercise{Training = trainings[4], Exercise = exercises[13],Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[4], Exercise = exercises[14],Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{Training = trainings[4], Exercise = exercises[17],Rounds=3,Repetitions=10,MaxWeight=10,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[5], Exercise = exercises[1],Rounds=3,Repetitions=12,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[5], Exercise = exercises[4],Rounds=2,Repetitions=12,MaxWeight=4,Comments="ok"},
            new TrainingExercise{Training = trainings[5], Exercise = exercises[9],Rounds=3,Repetitions=10,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[5], Exercise = exercises[2],Rounds=3,Repetitions=12,MaxWeight=20,Comments="too hard"},
            new TrainingExercise{Training = trainings[5], Exercise = exercises[15],Rounds=1,Repetitions=10,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[6], Exercise = exercises[5],Rounds=3,Repetitions=12,MaxWeight=10,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[6], Exercise = exercises[8],Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[6], Exercise = exercises[7],Rounds=3,Repetitions=10,MaxWeight=16,Comments="too hard"},
            new TrainingExercise{Training = trainings[6], Exercise = exercises[10],Rounds=3,Repetitions=16,MaxWeight=10,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[7], Exercise = exercises[12],Rounds=3,Repetitions=12,MaxWeight=16,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[7], Exercise = exercises[1],Rounds=2,Repetitions=10,MaxWeight=10,Comments="ok"},
            new TrainingExercise{Training = trainings[7], Exercise = exercises[5],Rounds=3,Repetitions=12,MaxWeight=12,Comments="ok"},
            new TrainingExercise{Training = trainings[7], Exercise = exercises[16],Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{Training = trainings[8], Exercise = exercises[1],Rounds=2,Repetitions=12,MaxWeight=10,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[8], Exercise = exercises[4],Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[8], Exercise = exercises[8],Rounds=3,Repetitions=10,MaxWeight=12,Comments="ok"},
            new TrainingExercise{Training = trainings[8], Exercise = exercises[3],Rounds=2,Repetitions=12,MaxWeight=10,Comments="too hard"},
            new TrainingExercise{Training = trainings[8], Exercise = exercises[15],Rounds=3,Repetitions=12,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[9], Exercise = exercises[6],Rounds=3,Repetitions=12,MaxWeight=8,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[9], Exercise = exercises[3],Rounds=3,Repetitions=16,MaxWeight=10,Comments="ok"},
            new TrainingExercise{Training = trainings[9], Exercise = exercises[7],Rounds=2,Repetitions=6,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[9], Exercise = exercises[18],Rounds=3,Repetitions=12,MaxWeight=10,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[10], Exercise = exercises[10],Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[10], Exercise = exercises[9],Rounds=2,Repetitions=12,MaxWeight=8,Comments="ok"},
            new TrainingExercise{Training = trainings[10], Exercise = exercises[3],Rounds=3,Repetitions=16,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{Training = trainings[10], Exercise = exercises[5],Rounds=2,Repetitions=12,MaxWeight=18,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[11], Exercise = exercises[5],Rounds=2,Repetitions=10,MaxWeight=10,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[11], Exercise = exercises[13],Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[11], Exercise = exercises[14],Rounds=3,Repetitions=16,MaxWeight=20,Comments="ok"},
            new TrainingExercise{Training = trainings[11], Exercise = exercises[15],Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{Training = trainings[11], Exercise = exercises[0],Rounds=3,Repetitions=12,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[12], Exercise = exercises[2],Rounds=3,Repetitions=12,MaxWeight=14,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[12], Exercise = exercises[11],Rounds=2,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[12], Exercise = exercises[17],Rounds=3,Repetitions=10,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{Training = trainings[12], Exercise = exercises[16],Rounds=3,Repetitions=10,MaxWeight=16,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[13], Exercise = exercises[3],Rounds=3,Repetitions=12,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[13], Exercise = exercises[7],Rounds=3,Repetitions=12,MaxWeight=14,Comments="ok"},
            new TrainingExercise{Training = trainings[13], Exercise = exercises[1],Rounds=3,Repetitions=10,MaxWeight=6,Comments="ok"},
            new TrainingExercise{Training = trainings[13], Exercise = exercises[10],Rounds=2,Repetitions=8,MaxWeight=8,Comments="too hard"},
            new TrainingExercise{Training = trainings[13], Exercise = exercises[13],Rounds=3,Repetitions=10,MaxWeight=0,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[14], Exercise = exercises[17],Rounds=3,Repetitions=12,MaxWeight=20,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[14], Exercise = exercises[5],Rounds=3,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[14], Exercise = exercises[4],Rounds=3,Repetitions=10,MaxWeight=10,Comments="too hard"},
            new TrainingExercise{Training = trainings[14], Exercise = exercises[13],Rounds=3,Repetitions=162,MaxWeight=45,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[15], Exercise = exercises[0],Rounds=3,Repetitions=12,MaxWeight=22,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[15], Exercise = exercises[11],Rounds=3,Repetitions=12,MaxWeight=10,Comments="ok"},
            new TrainingExercise{Training = trainings[15], Exercise = exercises[5],Rounds=2,Repetitions=12,MaxWeight=0,Comments="ok"},
            new TrainingExercise{Training = trainings[15], Exercise = exercises[7],Rounds=3,Repetitions=10,MaxWeight=15,Comments="too hard"},
            new TrainingExercise{Training = trainings[15], Exercise = exercises[9],Rounds=3,Repetitions=12,MaxWeight=10,Comments="more repetitions next time"},
            new TrainingExercise{Training = trainings[16], Exercise = exercises[3],Rounds=3,Repetitions=12,MaxWeight=0,Comments="make it harder next time"},
            new TrainingExercise{Training = trainings[16], Exercise = exercises[11],Rounds=2,Repetitions=10,MaxWeight=22,Comments="ok"},
            new TrainingExercise{Training = trainings[16], Exercise = exercises[8],Rounds=3,Repetitions=16,MaxWeight=10,Comments="ok"},
            new TrainingExercise{Training = trainings[16], Exercise = exercises[4],Rounds=3,Repetitions=12,MaxWeight=0,Comments="too hard"},
            new TrainingExercise{Training = trainings[16], Exercise = exercises[17],Rounds=2,Repetitions=10,MaxWeight=25,Comments="too hard"},
            };
            foreach (TrainingExercise te in trainingExercises)
            {
                context.TrainingExercises.Add(te);
            }
            context.SaveChanges();
        }
    }
}

