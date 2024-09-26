namespace My_Project.DTO
{
    public class TaskRequestDTO
    {
        public int Id { get; set; }

        public int AgentId { get; set; }
        public int AgencyId { get; set; }

        public string TaskName { get; set; }

        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
