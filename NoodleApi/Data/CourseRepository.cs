using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using NoodleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace NoodleApi.Data
{
    // Retrieves and inserts course and publication data from a relational
    // database. 
    /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.RelationalCourseRepository"]/*'/>
    public class CourseRepository : ICourseService
    {
        private readonly CourseContext _dbContext;

        // Construct an instance with a database context
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Data.RelationalCourseRepository.Constructor"]/*'/>
        public CourseRepository(CourseContext context)
        {
            this._dbContext = context;
        }

        Task<List<CourseDTO>> ICourseService.GetCoursesAsync()
        {
            return _dbContext.Courses
                .Select(course => course.AsDTO())
                .ToListAsync();
        }

        Task<List<CourseSimplifiedDTO>> ICourseService.GetSimplifiedCoursesAsync()
        {
            return _dbContext.Courses
                .Select(course => course.AsSimplifiedDTO())
                .ToListAsync();
        }

        Task<List<PublicationDTO>> ICourseService.GetCoursePublicationsAsync(Guid id)
        {
            return _dbContext.Publications
                .Where(p => p.CourseId == id)
                .Select(p => p.AsDTO())
                .ToListAsync();
        }

        Task<CourseDTO> ICourseService.GetCourseAsync(Guid id)
        {
            return _dbContext.Courses
                .Where(c => c.Id == id)
                .Select(c => c.AsDTO())
                .FirstOrDefaultAsync();
        }

        Task ICourseService.ArchiveCourseAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task ICourseService.CreateCourseAsync(Course course)
        {
            course.Id = new Guid();
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
            var course = _dbContext.Courses.Find(id);
            _dbContext.Courses.Remove(course);
            return _dbContext.SaveChangesAsync();
        }

        Task ICourseService.CreateCoursePublication(Guid courseId, PublicationDTO publication)
        {
            publication.Id = new Guid();
            _dbContext.Publications.Add(publication.AsModel());
            return _dbContext.SaveChangesAsync();
        }

        Task<PublicationDTO> ICourseService.GetCoursePublicationById(Guid id)
        {
            return _dbContext.Publications
                .Where(p => p.Id == id)
                .Select(p => p.AsDTO())
                .FirstOrDefaultAsync();
        }

        Task ICourseService.DeleteCoursePublication(Guid id)
        {
            var publication = _dbContext.Publications.Find(id);
            _dbContext.Publications.Remove(publication);
            return _dbContext.SaveChangesAsync();
        }
    }
}