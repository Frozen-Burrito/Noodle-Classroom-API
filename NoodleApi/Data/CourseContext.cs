using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoodleApi.Models;

namespace NoodleApi.Data
{
    public class CourseContext : ICourseService
    {
        Task ICourseService.ArchiveCourseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task ICourseService.CreateCourseAsync(Course course)
        {
            throw new NotImplementedException();
        }

        Task ICourseService.DeleteCourseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Course> ICourseService.GetCourseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Course>> ICourseService.GetCoursesAsync()
        {
            throw new NotImplementedException();
        }

        Task ICourseService.UpdateCourseAsync(Course course)
        {
            throw new NotImplementedException();
        }
    }
}