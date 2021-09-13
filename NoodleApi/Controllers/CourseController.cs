using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NoodleApi.Data;
using NoodleApi.Models;
using Microsoft.AspNetCore.Http;

namespace NoodleApi.Controllers
{
    /*
    *   The CourseController class performs actions on the Course and Publication
    *   models by presenting api endpoints.  
    */
    /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="T:NoodleApi.Controllers.CourseController"]/*'/>
    [Route("api/courses")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseRepository;
        private readonly ILogger<CourseController> logger;

        // Creates a new instance of the CourseController class and injects a
        // ICourseService repository and an ILogger<CourseController> logger.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.CourseController"]/*'/>
        public CourseController(ICourseService courseRepository, ILogger<CourseController> logger)
        {
            if (courseRepository is null)
            {
                throw new ArgumentNullException(nameof(courseRepository));
            }

            this._courseRepository = courseRepository;
            this.logger = logger;
        }

        // Returns all courses in the repository
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.GetCourses"]/*'/>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseDTO>))]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] GET - /api/courses";
            logger.LogInformation(logMsg);

            var courses = await _courseRepository.GetCoursesAsync();

            return courses;
        }

        // Returns all courses in the repository in a reduced format.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.GetSimplifiedCourses"]/*'/>
        [HttpGet("/api/courses/simple")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CourseSimplifiedDTO>))]
        public async Task<ActionResult<IEnumerable<CourseSimplifiedDTO>>> GetSimplifiedCourses()
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] GET - /api/courses/simplified";
            logger.LogInformation(logMsg);

            var courses = await _courseRepository.GetSimplifiedCoursesAsync();

            return courses;
        }

        // Searches the repository for a certain course.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.GetCourse(System.Guid)"]/*'/>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CourseDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseDTO>> GetCourse(Guid id)
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] GET - /api/courses/{id}";
            logger.LogInformation(logMsg);

            var course = await _courseRepository.GetCourseAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            var publications = await _courseRepository.GetCoursePublicationsAsync(id);
            course.Publications = publications;

            return course;
        }

        // Creates a new course and saves it to the repository.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.CreateCourse(CourseDTO course)"]/*'/>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CourseDTO))]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<CourseDTO>> CreateCourse(CourseDTO courseDTO)
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] POST - /api/courses";
            logger.LogInformation(logMsg);

            Course newCourse = courseDTO.AsModel();
            logger.LogInformation($"CREATED: {nameof(GetCourse)}, {newCourse.Id}");

            await _courseRepository.CreateCourseAsync(newCourse);


            return CreatedAtAction(nameof(GetCourse), new { id = newCourse.Id }, newCourse.AsDTO());
        }

        // Removes an existing course from the collection.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.DeleteCourse(System.Guid id)"]/*'/>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCourse(Guid id)
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] DELETE - /api/courses/{id}";
            logger.LogInformation(logMsg);

            var course = await _courseRepository.GetCourseAsync(id);

            if (course is null)
            {
                return NotFound();
            }

            await _courseRepository.DeleteCourseAsync(course.Id);

            return NoContent();
        }

        #region Publications
        // Retrieves all the publications assigned with a specific course.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.GetCoursePublications(System.Guid courseId)"]/*'/>
        [HttpGet("{courseId:guid}/publications")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PublicationDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<PublicationDTO>>> GetCoursePublications(Guid courseId)
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] GET - /api/courses/{courseId}/publications";
            logger.LogInformation(logMsg);

            var course = await _courseRepository.GetCourseAsync(courseId);

            if (course is null)
            {
                return NotFound();
            }

            var publications = await _courseRepository.GetCoursePublicationsAsync(courseId);

            return publications;
        }

        // Creates a new publication and saves it to the repository.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.CreateCoursePublication(System.Guid courseId, PublicationDTO publication)"]/*'/>
        [HttpPost("{courseId:guid}/publications")]
        public async Task<ActionResult<PublicationDTO>> CreateCoursePublication(Guid courseId, PublicationDTO publication)
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] POST - /api/courses/{courseId}/publications";
            logger.LogInformation(logMsg);

            var course = await _courseRepository.GetCourseAsync(courseId);

            if (course == null)
            {
                return NotFound();
            }

            await _courseRepository.CreateCoursePublication(courseId, publication);

            logger.LogInformation($"CREATED: {nameof(GetCoursePublicationById)}, {publication.Id}");

            return CreatedAtAction(nameof(GetCoursePublicationById), new { id = publication.Id }, publication);
        }

        // Searches the repository for a certain publication.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.GetCoursePublicationById(System.Guid courseId, System.Guid id)"]/*'/>
        [HttpGet("{courseId:guid}/publications/{id:guid}")]
        public async Task<ActionResult<PublicationDTO>> GetCoursePublicationById(Guid courseId, Guid id)
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] GET - /api/courses/{courseId}/publications/{id}";
            logger.LogInformation(logMsg);

            var publication = await _courseRepository.GetCoursePublicationById(id);

            if (publication is null)
            {
                return NotFound();
            }

            return publication;
        }

        // Removes an existing publications from a course.
        /// <include file='../Docs/controller_docs.xml' path='/doc/members/member[@name="M:NoodleApi.Controllers.CourseController.DeleteCoursePublication(System.Guid courseId, System.Guid id)"]/*'/>
        [HttpDelete("{courseId}/publications/{id}")]
        public async Task<ActionResult> DeleteCoursePublication(Guid courseId, Guid id)
        {
            string logMsg = $"[{DateTime.UtcNow.ToLongTimeString()}] DELETE - /api/courses/{courseId}/publications/{id}";
            logger.LogInformation(logMsg);
            
            var publication = await _courseRepository.GetCoursePublicationById(id);

            if (publication is null)
            {
                return NotFound();
            }

            await _courseRepository.DeleteCoursePublication(publication.Id);

            return NoContent();
        }
        #endregion
    }
}