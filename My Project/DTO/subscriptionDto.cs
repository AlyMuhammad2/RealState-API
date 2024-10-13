namespace My_Project.DTO
{
    public class subscriptionDto
    {
        public int SubscriptionId { get; set; }
        public string SubscriberType { get; set; } 
        public string SubscriberName { get; set; }
        public string SubscriptionType { get; set; }
        public DateTime StartDate { get; set; }
        public int Durationmonth { get; set; }
        public bool IsActive { get; set; }
        public decimal Amount { get; set; }
        public void CalculateIsActive()
        {
            var endDate = StartDate.AddMonths(Durationmonth);
            IsActive = DateTime.Now <= endDate; // True if current date is before or on the end date
        }
    }
}
