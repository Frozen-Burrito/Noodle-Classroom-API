using System;

namespace NoodleApi.Models
{
    public class PublicationDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
        public int Value { get; set; }
        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}