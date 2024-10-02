namespace My_Project.DTO
{
    public class SubscriptionDTO
    {
        public string SubscriptionType { get; set; } // "Free", "Monthly", or "Annual"
        public int DurationMonths { get; set; } // Duration for the subscription in months
        public int UserId { get; set; }
    }
}
