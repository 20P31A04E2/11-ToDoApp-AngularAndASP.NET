using AutoMapper;

namespace Services
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Concerns.User, Repository.DataConcerns.User>().ReverseMap();
            CreateMap<Concerns.TodoTask, Repository.DataConcerns.TodoTask>();
            CreateMap<Repository.DataConcerns.TodoTask, Concerns.TodoTask>()
                            .ForMember(dest => dest.TaskStatus, opt => opt.MapFrom(src => src.Status.Status1));

        }
    }
}
