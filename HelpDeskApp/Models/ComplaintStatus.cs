using System;

namespace HelpDeskApp.Models
{
    public class ComplaintStatus : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
