namespace My_Project.DTO
{
    public class AgentResponseDTO
    {
        public int id {  get; set; }
        public string name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
      
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionType { get; set; }
        public string AgencyName { get; set; }
        public int Agencyid { get; set; }
        public int TasksNumber { get; set; }
        public int ProductsNumber { get; set; }
        public List<ProductCardDTO>? Products { get; set; }
        public List<TaskResponseDTO>? Tasks { get; set; }
    }
}
