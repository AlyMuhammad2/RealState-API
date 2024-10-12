namespace My_Project.DTO
{
    public class AgentRequestDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public string AgencyName { get; set; }
 public int AgencyId { get; set; }
    }
}
