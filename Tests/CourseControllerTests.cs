using System;
using Xunit;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

using NoodleApi.Controllers;
using NoodleApi.Models;
using NoodleApi.Data;
using NoodleApi.Tests.Util;

namespace NoodleApi.Tests
{
    /* 
        The CourseControllerTests class contains all tests for the 
        CourseController API class.
    */
    /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="T:NoodleApi.Tests.CourseControllerTests"]/*'/>
    public class CourseControllerTests
    {
        // Creates a random CourseDTO object.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.CreateRandomCourse"]/*'/>
        private CourseDTO CreateRandomCourse()
        {
            return new CourseDTO()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                AreaOfStudy = "Comp. Sci.",
                Description = "A short description",
                Publications = new List<PublicationDTO>(),
                IsArchived = new Random().Next(2) == 0,
                CourseType = (CourseType) new Random().Next(2),
                MeetingUrl = null,
                CreatedDate = DateTime.UtcNow
            };
        }

        // Creates a random CourseSimplifiedDTO object.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.CreateRandomSimplifiedCourse"]/*'/>
        private CourseSimplifiedDTO CreateRandomSimplifiedCourse()
        {
            return new CourseSimplifiedDTO()
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                InstructorName = Guid.NewGuid().ToString(),
                AreaOfStudy = Guid.NewGuid().ToString(),
                IsArchived = new Random().Next(2) == 0
            };
        }

        // Adds various random CourseDTO instances to a List<CourseDTO>
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.GetTestCourses(System.Int32)"]/*'/>
        private List<CourseDTO> GetTestCourses(int count = 1)
        {
            var courses = new List<CourseDTO>();

            for (int i = 0; i < count; i++)
            {
                courses.Add(CreateRandomCourse());
            }

            return courses;
        }

        // Adds various random CourseSimplifiedDTO instances to a List<CourseSimplifiedDTO>
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.GetTestSimplifiedCourses(System.Int32)"]/*'/>
        private List<CourseSimplifiedDTO> GetTestSimplifiedCourses(int count = 1)
        {
            var simplifiedCourses = new List<CourseSimplifiedDTO>();

            for (int i = 0; i < count; i++)
            {
                simplifiedCourses.Add(CreateRandomSimplifiedCourse());
            }

            return simplifiedCourses;
        }

        #region GetCourses
        // Test for the GetCourses method, asserts a matching total count.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.GetCourses_NoParams_ReturnsMatchingTotalCount"]/*'/>
        [Fact]
        public async Task GetCourses_NoParams_ReturnsMatchingTotalCount()
        {
            // Arrange
            int expectedCount = new Random().Next(5);
            var repositoryStub = new Mock<ICourseService>();
            repositoryStub.Setup(repo => repo.GetCoursesAsync())
                .ReturnsAsync(GetTestCourses(expectedCount));

            var loggerStub = new Mock<ILogger<CourseController>>();
            var controller = new CourseController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetCourses();

            // Assert
            Assert.Equal(expectedCount, result.Value.Count());
        }

        // Test for the GetCourses method, asserts equivalent collections.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.GetCourses_ExistingCourses_ReturnsAllCourses"]/*'/>
        [Fact]
        public async Task GetCourses_ExistingCourses_ReturnsAllCourses()
        {
            // Arrange
            int courseCount = new Random().Next(1, 5);
            List<CourseDTO> courses = GetTestCourses(courseCount);

            var repositoryStub = new Mock<ICourseService>();
            repositoryStub.Setup(repo => repo.GetCoursesAsync())
                .ReturnsAsync(courses);

            var loggerStub = new Mock<ILogger<CourseController>>();
            var controller = new CourseController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetCourses();

            // Assert
            Assert.Equal(courses, result.Value, new CollectionEquivalenceComparer<CourseDTO>());
        }

        #endregion

        // Test for the GetSimpleCourses method, asserts a matching total count.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.GetSimpleCourses_NoParams_ReturnsMatchingTotalCount"]/*'/>
        [Fact]
        public async Task GetSimpleCourses_NoParams_ReturnsMatchingTotalCount()
        {
            // Arrange
            int expectedCount = new Random().Next(5);
            var repositoryStub = new Mock<ICourseService>();
            repositoryStub.Setup(repo => repo.GetSimplifiedCoursesAsync())
                .ReturnsAsync(GetTestSimplifiedCourses(expectedCount));

            var loggerStub = new Mock<ILogger<CourseController>>();
            var controller = new CourseController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetSimplifiedCourses();

            // Assert
            Assert.Equal(expectedCount, result.Value.Count());
        }

        #region GetCourse
        // Test for the GetCourse method with a random unexisting Course ID, asserts the
        // type of the result to be NotFoundResult.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.GetCourse_UnexistingCourseId_ReturnsNotFoundResult"]/*'/>
        [Fact]
        public async Task GetCourse_UnexistingCourseId_ReturnsNotFoundResult()
        {
            // Arrange
            var courses = GetTestCourses(3);
            var repositoryStub = new Mock<ICourseService>();
            repositoryStub.Setup(repo => repo.GetCoursesAsync())
                .ReturnsAsync(courses);

            var loggerStub = new Mock<ILogger<CourseController>>();
            var controller = new CourseController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetCourse(new Guid());

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        // Test for the GetCourse method with a random Course ID, asserts the 
        // equivalence between the result and the expected CourseDTO objects.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.GetCourse_ValidCourseId_ReturnsCorrectCourse"]/*'/>
        [Fact]
        public async Task GetCourse_ValidCourseId_ReturnsCorrectCourse()
        {
            // Arrange
            int courseIndex = new Random().Next(3);
            var courses = GetTestCourses(3);
            var expectedCourse = courses[courseIndex];

            var repositoryStub = new Mock<ICourseService>();
            repositoryStub.Setup(repo => repo.GetCourseAsync(expectedCourse.Id))
                .ReturnsAsync(expectedCourse);

            var loggerStub = new Mock<ILogger<CourseController>>();
            var controller = new CourseController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.GetCourse(expectedCourse.Id) as ActionResult<CourseDTO>;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Value.Equals(expectedCourse));
        }
        #endregion
   
        // Test for the CreateCourse method with a valid CourseDTO payload,
        // assert that the resulting object is equivalent to the original.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.CreateCourseAsync_ValidCourse_ReturnsCreatedCourse"]/*'/>
        [Fact]
        public async Task CreateCourseAsync_ValidCourse_ReturnsCreatedCourse()
        {
            // Arrange
            CourseDTO newCourse = CreateRandomCourse();
            
            var repositoryStub = new Mock<ICourseService>();
            var loggerStub = new Mock<ILogger<CourseController>>();

            var controller = new CourseController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.CreateCourse(newCourse);
            var createdCourse = (result.Result as CreatedAtActionResult).Value as CourseDTO;

            // Assert
            Assert.True(createdCourse.Equals(newCourse));
        }

        // Test for the DeleteCourse method with a valid course Id,
        // assert that the type of the action result is NoContentResult.
        /// <include file='docs/tests_doc.xml' path='/doc/members/member[@name="M:NoodleApi.Tests.CourseControllerTests.DeleteCourseAsync_ExistingCourse_ReturnsNoContentResult"]/*'/>
        [Fact]
        public async Task DeleteCourseAsync_ExistingCourse_ReturnsNoContentResult()
        {
            // Arrange
            CourseDTO course = CreateRandomCourse();
            
            var repositoryStub = new Mock<ICourseService>();
            repositoryStub.Setup(repo => repo.GetCourseAsync(It.IsAny<Guid>()))
                .ReturnsAsync(course);
            var loggerStub = new Mock<ILogger<CourseController>>();

            var controller = new CourseController(repositoryStub.Object, loggerStub.Object);

            // Act
            var result = await controller.DeleteCourse(course.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
