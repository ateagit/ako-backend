using ako_api.Models;
using ako_api.Models.DTO;
using AutoMapper;

namespace ako_api
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Subject, SubjectDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SubjectId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.SubjectHeirarchyParentSubject));


            CreateMap<SubjectHeirarchy, SubjectDTO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ChildSubject.SubjectId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ChildSubject.Name))
                .ForAllOtherMembers(opt => opt.Ignore());


            CreateMap<InputCourseDTO, Course>()
                .ForMember(dest => dest.CoursePrerequisiteMainCourse, opt => opt.MapFrom(src => src.PrerequisiteCourseId));

            CreateMap<int, CoursePrerequisite>()
                .ForMember(dest => dest.PrerequisiteCourseId, opt => opt.MapFrom(src => src));

            CreateMap<Course, OutputBasicCourseDTO>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name));

            CreateMap<Course, OutputDetailedCourseDTO>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
                .ForMember(dest => dest.SubjectName, opt => opt.MapFrom(src => src.Subject.Name))
                .ForMember(dest => dest.PrerequisiteCourses, opt => opt.MapFrom(src => src.CoursePrerequisiteMainCourse))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comment));

            CreateMap<CoursePrerequisite, OutputPrerequisiteCourseDTO>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.PrerequisiteCourse.CourseId))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.PrerequisiteCourse.Title));

            CreateMap<Comment, OutputCommentDTO>()
                .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

            CreateMap<Subject, SubjectCourseDTO>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course));

            CreateMap<Comment, OutputCommentDTO>();

            CreateMap<InputCommentDTO, Comment>();
        }
    }
}