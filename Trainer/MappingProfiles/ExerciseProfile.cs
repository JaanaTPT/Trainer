﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trainer.Models;
using Trainer.Models.ViewModels;

namespace Trainer.MappingProfiles
{
    public class ExerciseProfile : Profile
    {
        public ExerciseProfile()
        {
            CreateMap<List<Exercise>, List<ExerciseModel>>();
            CreateMap<Exercise, ExerciseModel>();
            CreateMap<Exercise, ExerciseEditModel>();

            CreateMap<ExerciseEditModel, Exercise>()
              .ForMember(m => m.ID, m => m.Ignore())
              .ForMember(m => m.TrainingExercises, m => m.Ignore());
        }
    }
}
