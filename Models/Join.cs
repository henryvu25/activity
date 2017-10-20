namespace belt_project.Models
{
    public class Join : BaseEntity
    {
        public int JoinId { get;set; }
        public int? UserId { get;set; }
        public User User { get;set; }
        public int ActivityId { get;set; }
        public Activity Activity { get;set; }
        
    }
}