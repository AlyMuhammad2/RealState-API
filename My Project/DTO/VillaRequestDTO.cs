namespace My_Project.DTO
{
    public class VillaRequestDto
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


        public int AgencyId { get; set; }

        public int AgentId { get; set; }
    }
}
