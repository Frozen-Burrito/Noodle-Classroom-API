using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using NoodleApi.Models;

namespace NoodleApi.Data
{
    public interface ICourseService
    {
        Task<Course> GetCourseAsync(Guid id);
        Task<IEnumerable<Course>> GetCoursesAsync();
        Task CreateCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task ArchiveCourseAsync(Guid id);
        Task DeleteCourseAsync(Guid id);
    }
}