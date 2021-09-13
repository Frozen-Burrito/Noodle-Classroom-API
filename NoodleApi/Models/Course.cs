using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoodleApi.Models
{
    // Represents the possible types of courses.
    public enum CourseType 
    {
        Class,
        Workshop
    }

    // Holds information for a specific course, references a collection
    // of publications and another one of users.
    public class Course
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        public string AreaOfStudy { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }

        public CourseType CourseType { get; set; }

        public List<Publication> Publications { get; set; } = new List<Publication>();

        [MaxLength(200)]
        public string MeetingUrl { get; set; }  

        public bool IsArchived { get; set; }

        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
    }
}