using System.Linq;

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
    }
}