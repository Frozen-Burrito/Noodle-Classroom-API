using System;
using System.ComponentModel.DataAnnotations;

namespace NoodleApi.Models
{
    public class CourseSimplifiedDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string InstructorName { get; set; }  
        public string AreaOfStudy { get; set; } 

        public bool IsArchived { get; set; }
    }
}