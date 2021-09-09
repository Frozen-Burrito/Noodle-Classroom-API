using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NoodleApi.Models
{
    public class CourseDTO : IEquatable<CourseDTO>
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


        public override bool Equals(object obj) => this.Equals(obj as CourseDTO);

        public bool Equals(CourseDTO other)
        {
            if (other is null)
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            if (this.GetType() != other.GetType())
            {
                return false;
            }

            return 
            (
                (Id == other.Id) &&
                (Name == other.Name) &&
                (CreatedDate == other.CreatedDate) &&
                (IsArchived == other.IsArchived)
            );
        }

        public override int GetHashCode() => (Id, CreatedDate).GetHashCode();
    }
}