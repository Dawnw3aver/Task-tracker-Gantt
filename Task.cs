namespace tasks
{
    public class Task
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string Resource { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PercentComplete { get; set; }
    }
}
