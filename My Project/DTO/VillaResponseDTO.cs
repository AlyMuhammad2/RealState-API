namespace My_Project.DTO
{
    public class VillaResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAvailable { get; set; }
        public string PrimaryImg { get; set; }

        public List<string> images { get; set; }
        public int NumberOfFloors { get; set; } 

        public bool HasSwimmingPool { get; set; } 

        public bool HasGarden { get; set; }

        // Agency details
        public string AgencyName { get; set; }
        public string AgencyEmail { get; set; }
        public string AgencyPhoneNumber { get; set; }

        // Agent details
        public string AgentName { get; set; }
        public string AgentPhoneNumber { get; set; }
        public string AgentEmail { get; set; }
    }
}
