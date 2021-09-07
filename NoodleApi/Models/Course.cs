using System;
using System.ComponentModel.DataAnnotations;

namespace NoodleApi.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}