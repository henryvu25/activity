using System;
using System.Collections.Generic;

namespace belt_project.Models
{
    public class Activity : BaseEntity
    {
        public int ActivityId { get;set; }
        public string Title { get;set; }
        public DateTime Date { get;set; }
        public DateTime Time { get;set; }
        public string Duration { get;set; }
        public string Description { get;set; }
        public int? UserId { get;set; }
        public User User { get;set; }
        public List<Join> Participants { get;set; }
        public Activity()
        {
            Participants = new List<Join>();
        }
    }
}