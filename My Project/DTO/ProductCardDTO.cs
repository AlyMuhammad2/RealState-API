namespace My_Project.DTO
{
    public class ProductCardDTO
    {
        public string Title { get; set; }

        public decimal Price { get; set; }

        public double Area {  get; set; }

        public DateTime CreatedDate { get; set; }

        public bool? IsForRent { get; set; }

        public int? NumOfBedroom { get; set; }

        public int? NumOfBathrom { get; set; }

        public int? NumOfCars { get; set; }

        public string Location { get; set; }

        public bool IsAvailable { get; set; }

        public string AgencyName { get; set; }

        public string AgentName { get; set; }

        public string PrimaryImg { get; set; }

        public string ProductType { get; set; }

    }
}
