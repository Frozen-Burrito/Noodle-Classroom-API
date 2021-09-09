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
    public class CourseControllerTests
    {
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

        private List<CourseDTO> GetTestCourses(int count = 1)
        {
            var courses = new List<CourseDTO>();

            for (int i = 0; i < count; i++)
            {
                courses.Add(CreateRandomCourse());
            }

            return courses;
        }

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
            Assert.Equal(expectedCourse.CreatedDate, result.Value.CreatedDate);
        }
        #endregion
   
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
