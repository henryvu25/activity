using System;
using System.Collections.Generic;

namespace belt_project.Models
{
    public abstract class BaseEntity {}
    public class User
    {
        public int UserId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public List<Join> Activities { get;set; }
        public List<Activity> Creations { get;set; }
        public User()
        {
            Activities = new List<Join>();
            Creations = new List<Activity>();
        }
    }
}