namespace My_Project.DTO
{
    public class HouseRequestDTO
    {
        public int Id { get; set;}
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public bool? IsForRent { get; set; }

        public int? NumOfBedroom { get; set; }

        public int? NumOfBathrom { get; set; }

        public int? NumOfCars
        {
            get; set;
        }
        public DateTime CreatedDate { get; set; }

        public bool IsAvailable { get; set; }

        public int NumberOfRooms { get; set; } 

        public bool HasGarage { get; set; } 

        public bool HasBackyard { get; set; }

        public string PrimaryImg {  get; set; }

        public List<string> images { get; set; }
        public int AgencyId { get; set; }

        public int AgentId { get; set; }
    }
}
