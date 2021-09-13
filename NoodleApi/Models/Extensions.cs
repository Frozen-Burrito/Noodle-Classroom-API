using System.Linq;

#pragma warning disable 1591

namespace NoodleApi.Models
{
    public static class Extensions
    {
        public static CourseDTO AsDTO(this Course course)
        {
            return new CourseDTO
            {
                Id = course.Id,
                Name = course.Name,
                AreaOfStudy = course.AreaOfStudy,
                Description = course.Description,
                CourseType = course.CourseType,
                Publications = course.Publications.Select(p => p.AsDTO()).ToList(),
                MeetingUrl = course.MeetingUrl,
                IsArchived = course.IsArchived,
                CreatedDate = course.CreatedDate
            };
        }

        public static CourseSimplifiedDTO AsSimplifiedDTO(this Course course)
        {
            return new CourseSimplifiedDTO
            {
                Id = course.Id,
                Name = course.Name,
                AreaOfStudy = course.AreaOfStudy,
                InstructorName = "Sample Instructor",
                IsArchived = course.IsArchived
            };
        }

        public static Course AsModel(this CourseDTO courseDTO)
        {
            return new Course
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                AreaOfStudy = courseDTO.AreaOfStudy,
                Description = courseDTO.Description,
                CourseType = courseDTO.CourseType,
                Publications = courseDTO.Publications.Select(p => p.AsModel()).ToList(),
                MeetingUrl = courseDTO.MeetingUrl,
                IsArchived = courseDTO.IsArchived,
                CreatedDate = courseDTO.CreatedDate
            };
        }

        public static PublicationDTO AsDTO(this Publication publication)
        {
            return new PublicationDTO
            {
                Id = publication.Id,
                Title = publication.Title,
                Body = publication.Body,
                Value = publication.Value,
                CreatedDate = publication.CreatedDate,
                ModifiedDate = publication.ModifiedDate
            };
        }

        public static Publication AsModel(this PublicationDTO publicationDTO)
        {
            return new Publication
            {
                Id = publicationDTO.Id,
                Title = publicationDTO.Title,
                Body = publicationDTO.Body,
                Value = publicationDTO.Value,
                CreatedDate = publicationDTO.CreatedDate,
                ModifiedDate = publicationDTO.ModifiedDate
            };
        }
    }
}