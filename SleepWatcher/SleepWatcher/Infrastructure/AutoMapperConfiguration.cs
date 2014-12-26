using AutoMapper;
using SleepWatcher.Entites;
using SleepWatcher.Model;

namespace SleepWatcher.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Patient, PatientModel>();
            Mapper.CreateMap<Note, NoteModel>().ForMember(model => model.StepId,expression => expression.MapFrom(note => note.StepId));
            Mapper.CreateMap<Step, StepModel>();
            Mapper.CreateMap<StepModel, Step>();
            Mapper.CreateMap<NoteModel, Note>();
            Mapper.CreateMap<PatientModel, Patient>();
            Mapper.CreateMap<CurrentStep, StepModel>();
            Mapper.CreateMap<StepModel, CurrentStep>();
            Mapper.CreateMap<Step, CurrentStep>();


        }
    }
}
