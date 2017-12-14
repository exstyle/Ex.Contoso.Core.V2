namespace ContosoUniversityCore.Features.Student
{
    using AutoMapper;
    using Domain;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, Index.Model>();
            CreateMap<Student, Details.Model>().ForMember(x => x.Notes, opt => opt.Ignore()).ForMember(x => x.chartNote, opt => opt.Ignore()); ;
            CreateMap<Enrollment, Details.Model.Enrollment>();
            CreateMap<Create.Command, Student>(MemberList.Source);
            CreateMap<Student, Edit.Command>().ReverseMap();
            CreateMap<Student, Delete.Command>().ReverseMap();
        }
    }
}