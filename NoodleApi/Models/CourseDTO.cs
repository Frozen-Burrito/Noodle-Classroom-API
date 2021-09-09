using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoodleApi.Models
{
    public class CourseDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string AreaOfStudy { get; set; }
        public string Description { get; set; }

        public CourseType CourseType { get; set; }

        public List<PublicationDTO> Publications { get; set; }

        public string MeetingUrl { get; set; }  

        public bool IsArchived { get; set; }
        public DateTime CreatedDate { get; set; }   
    }
}