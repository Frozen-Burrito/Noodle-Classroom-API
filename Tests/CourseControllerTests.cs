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
    /// <summary>
    ///    The <c>CourseControllerTests</c> class contains all tests for the 
    ///    <c>CourseController</c> API class.
    /// </summary>
    /// <remarks>
    /// <para>
    /// There is at least one test method for each controller method.
    /// </para>
    /// <para>
    /// A few private utility methods are used for convenience.
    /// </para>
    /// </remarks>
    public class CourseControllerTests
    {
        // Creates a random CourseDTO object.
        /// <summary>
        ///    Creates a random <c>CourseDTO</c> object.
        /// </summary>
        /// <returns>
        ///     The <c>CourseDTO</c> instance.
        /// </returns>
        /// <example>
        /// <code> 
        /// CourseDTO course = CreateRandomCourse();
        /// </code>
        /// </example>
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
        /// <summary>
        ///    Creates a random <c>CourseSimplifiedDTO</c> object.
        /// </summary>
        /// <returns>
        ///     The <c>CourseSimplifiedDTO</c> instance.
        /// </returns>
        /// <example>
        /// <code> 
        /// CourseSimplifiedDTO course = CreateRandomSimplifiedCourse();
        /// </code>
        /// </example>
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
        /// <summary>
        ///    Adds various random <c>CourseDTO</c> instances to a List
        /// </summary>
        /// <returns>
        ///     The <c>List of CourseDTO</c> instance.
        /// </returns>
        /// <example>
        /// <code> 
        /// int n = 5;
        /// var courses = GetTestCourses(n);
        /// </code>
        /// </example>
        /// <param name="count">The desired size of the returned list.</param>
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
        /// <summary>
        ///    Adds various random <c>CourseSimplifiedDTO</c> instances to a List
        /// </summary>
        /// <returns>
        ///     The <c>List of CourseSimplifiedDTO</c> instance.
        /// </returns>
        /// <example>
        /// <code> 
        /// var courses = GetTestSimplifiedCourses(3);
        /// </code>
        /// </example>
        /// <param name="count">The desired size of the returned list.</param>
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
        /// <summary>
        ///    Test for the <c>GetCourses</c> method, asserts a matching total count.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Creates a mock <c>ICourseService</c>, <c>ILogger</c> and 
        ///     <c>CourseController</c>
        /// </para>
        /// <para>
        ///     Asserts that the size of the collection returned by 
        ///     <c>CourseController.GetCourses()</c> is equal to the expected
        ///     element count.
        /// </para>
        /// </remarks>
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
        /// <summary>
        ///    Test for the <c>GetCourses</c> method, asserts equivalent collections.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Creates a mock <c>ICourseService</c>, <c>ILogger</c> and 
        ///     <c>CourseController</c>, populating the repository with a random 
        ///     collection of <c>Course</c> instances.
        /// </para>
        /// <para>
        ///     Asserts that the collection returned by 
        ///     <c>CourseController.GetCourses()</c> is equal to the expected
        ///     collection.
        /// </para>
        /// </remarks>
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
        /// <summary>
        ///     Test for the <c>GetSimpleCourses</c> method, asserts a matching total count.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Creates a mock <c>ICourseService</c>, <c>ILogger</c> and 
        ///     <c>CourseController</c>.
        /// </para>
        /// <para>
        ///     Asserts that the size of the collection of <c>CourseSimplifiedDTO</c> instances
        ///     returned by <c>CourseController.GetSimpleCourses()</c> is equal to the expected
        ///     element count.
        /// </para>
        /// </remarks>
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
        /// <summary>
        ///    Test for the <c>GetCourse</c> method with a random unexisting Course ID, asserts the
        ///     type of the result to be <c>NotFoundResult</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Creates a mock <c>ICourseService</c>, <c>ILogger</c> and 
        ///     <c>CourseController</c>.
        /// </para>
        /// <para>
        ///     Asserts that the type of the result returned by
        ///     <c>CourseController.GetCourse(Guid id)</c> is <c>NotFoundResult</c>
        /// </para>
        /// </remarks>
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
        /// <summary>
        ///    Test for the <c>GetCourse</c> method with a random Course ID, asserts the
        ///    equivalence between the result and the expected <c>CourseDTO</c> objects.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Creates a mock <c>ICourseService</c>, <c>ILogger</c> and 
        ///     <c>CourseController</c>.
        /// </para>
        /// <para>
        ///     Populates the <c>ICourseService</c> with a random number of randomized courses,
        ///     then asserts that the <c>CourseDTO</c> object retrieved by a call to 
        ///     <c>CourseController.GetCourse(Guid id)</c> is equivalent to the original 
        ///     <c>CourseDTO</c> object inserted in the repository.
        /// </para>
        /// </remarks>
        [Fact]
        public async Task GetCourse_ReturnsCorrectCourse()
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
        /// <summary>
        ///     Test for the <c>CreateCourse</c> method with a valid <c>CourseDTO</c> payload,
        ///     assert that the resulting object is equivalent to the original.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Creates a mock <c>ICourseService</c>, <c>ILogger</c> and 
        ///     <c>CourseController</c>.
        /// </para>
        /// <para>
        ///     Asserts that the <c>CourseDTO</c> object returned by the
        ///     <c>CourseController.CreateCourse(CourseDTO newCourse)</c> method is equivalent
        ///     to the object originally sent as payload.
        /// </para>
        /// </remarks>
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
        /// <summary>
        ///     Test for the <c>DeleteCourse</c> method with a valid course <c>id</c>,
        ///     assert that the type of the action result is <c>NoContentResult</c>.
        /// </summary>
        /// <remarks>
        /// <para>
        ///     Creates a mock <c>ICourseService</c>, <c>ILogger</c> and 
        ///     <c>CourseController</c>.
        /// </para>
        /// <para>
        ///     Asserts that the result produced by the call to
        ///     <c>CourseController.DeleteCourse(Guid id)</c> method with a valid <c>id</c>
        ///     is of type <c>NoContentResult</c>.
        /// </para>
        /// </remarks>
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
