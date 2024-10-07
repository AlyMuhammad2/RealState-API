namespace My_Project.DTO
{
    public class SubscriptionResponseDTO
    {
        public int SubId;
        public string SubscriptionType { get; set; } // "Free", "Monthly", or "Annual"
        public int DurationMonths { get; set; }
        public string UserType { get; set; }
        public string Description { get; set; }
        public int? NumOfsubs { get; set; }
        public int? NumOfAvailableAgents { get; set; }
        public int? NumOfAvailableproducts { get; set; }
        public int? NumOfimagesperproducts { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }

    }
}
