using FluentValidation.Validators;

namespace My_Project.DTO
{
    public class AgencyResponseDTO
    {
        public int Id { get; set; }
        public string AgencyName { get; set; }
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string OwnerPhone { get; set; }
        public int SubscriptionId { get; set; }
        public string SubscriptionType { get; set; }
        public int AgentsNumber   { get; set; }
        public int ProductsNumber { get; set; }
      //  public ICollection<ProductCardDTO> products { get; set; }
 //       public ICollection<TaskResponseDTO> tasks { get; set; }

    }
}
