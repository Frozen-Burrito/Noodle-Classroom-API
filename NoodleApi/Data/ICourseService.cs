using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using NoodleApi.Models;

namespace NoodleApi.Data
{
    public interface ICourseService
    {
        Task<List<CourseDTO>> GetCoursesAsync();
        Task<CourseDTO> GetCourseAsync(Guid id);
        Task<List<CourseSimplifiedDTO>> GetSimplifiedCoursesAsync();
        Task<List<PublicationDTO>> GetCoursePublicationsAsync(Guid id);
        Task CreateCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task ArchiveCourseAsync(Guid id);
        Task DeleteCourseAsync(Guid id);
    }
}