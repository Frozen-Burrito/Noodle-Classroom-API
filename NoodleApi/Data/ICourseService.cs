using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using NoodleApi.Models;

namespace NoodleApi.Data
{
    // Provides methods for operations related to creating, retrieving, modifying
    // and deleting courses and its publications.
    /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService"]/*'/>
    public interface ICourseService
    {
        // Retrieves a list of courses.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.GetCoursesAsync"]/*'/>
        Task<List<CourseDTO>> GetCoursesAsync();

        // Searches the context for a course with the given id.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.GetCourseAsync(Guid id)"]/*'/>
        Task<CourseDTO> GetCourseAsync(Guid id);

        // Retrieves a list of courses, in a reduced format.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.GetSimplifiedCoursesAsync"]/*'/>
        Task<List<CourseSimplifiedDTO>> GetSimplifiedCoursesAsync();

        // Retrieves all publications from a course.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.GetCoursePublicationsAsync(Guid id)"]/*'/>
        Task<List<PublicationDTO>> GetCoursePublicationsAsync(Guid id);

        // Creates a new course and saves it.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.CreateCourseAsync(Course course)"]/*'/>
        Task CreateCourseAsync(Course course);

        // Updates the saved instance of an existing course.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.UpdateCourseAsync(Course course)"]/*'/>
        Task UpdateCourseAsync(Course course);

        // Toggles the archived status of a given course.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.ArchiveCourseAsync(Guid id)"]/*'/>
        Task ArchiveCourseAsync(Guid id);

        // Deletes an existing course.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.DeleteCourseAsync(Course course)"]/*'/>
        Task DeleteCourseAsync(Guid id);

        // Creates a new publication associated with the specified course.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.CreateCoursePublication(Guid courseId, PublicationDTO publication)"]/*'/>
        Task CreateCoursePublication(Guid courseId, PublicationDTO publication);

        // Retrieves a publication with the given id.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.GetCoursePublicationById(Guid id)"]/*'/>
        Task<PublicationDTO> GetCoursePublicationById(Guid id);

        // Deletes an existing course.
        /// <include file='../Docs/data_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Data.ICourseService.DeleteCoursePublication(Guid id)"]/*'/>
        Task DeleteCoursePublication(Guid id);
    }
}