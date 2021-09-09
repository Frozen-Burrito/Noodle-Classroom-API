using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using NoodleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace NoodleApi.Data
{
    public class CourseRepository : ICourseService
    {
        private readonly CourseContext _dbContext;

        public CourseRepository(CourseContext context)
        {
            this._dbContext = context;
        }

        public Task<List<CourseDTO>> GetCoursesAsync()
        {
            return _dbContext.Courses
                .Select(course => course.AsDTO())
                .ToListAsync();
        }

        public Task<List<CourseSimplifiedDTO>> GetSimplifiedCoursesAsync()
        {
            return _dbContext.Courses
                .Select(course => course.AsSimplifiedDTO())
                .ToListAsync();
        }

        public Task<List<PublicationDTO>> GetCoursePublicationsAsync(Guid id)
        {
            return _dbContext.Publications
                .Where(p => p.CourseId == id)
                .Select(p => p.AsDTO())
                .ToListAsync();
        }

        public Task<CourseDTO> GetCourseAsync(Guid id)
        {
            return _dbContext.Courses
                .Select(c => c.AsDTO())
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        Task ICourseService.ArchiveCourseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task CreateCourseAsync(Course course)
        {
            _dbContext.Courses.Add(course);
            return _dbContext.SaveChangesAsync();
        }

        Task ICourseService.UpdateCourseAsync(Course course)
        {
            _dbContext.Entry(course).State = EntityState.Modified;
            return _dbContext.SaveChangesAsync();
        }

        Task ICourseService.DeleteCourseAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}