using Application.Commands.Employee;
using Application.Commands.Project;
using Application.Commands.Team;
using Application.Commands.Technology;
using Application.Commands.TechnologyCategory;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateTechnology, Technology>();
            CreateMap<UpdateTechnology, Technology>();
            CreateMap<CreateTechnologyCategory, TechnologyCategory>();
            CreateMap<UpdateTechnologyCategory, TechnologyCategory>();
            CreateMap<CreateEmployee, Employee>();
            CreateMap<UpdateEmployee, Employee>();
            CreateMap<CreateTeam, Team>();
            CreateMap<UpdateTeam, Team>();
            CreateMap<CreateProject, Project>();
            CreateMap<UpdateProject, Project>();
        }
    }
}