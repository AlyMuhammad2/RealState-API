namespace My_Project.DTO
{
    public class ApartmentResponseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAvailable { get; set; }
        public int FloorNumber { get; set; } // رقم الطابق
        public bool HasElevatorAccess { get; set; } // هل يوجد مصعد؟

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
