using BLL.Abstractions;
using DAL.Models;
using Mapster;
using My_Project.DTO;

namespace My_Project.Mapping
{
    public class MappingConfigurations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Product, ProductCardDTO>()
               .Map(dest => dest.Title, src => src.Title)
               .Map(dest => dest.Price, src => src.Price)
               .Map(dest => dest.Location, src => src.Location)
               .Map(dest => dest.IsAvailable, src => src.IsAvailable)
               .Map(dest => dest.AgencyName, src => src.Agency.Name)
               .Map(dest => dest.AgentName, src => src.Agent.User.UserName)
               .Map(dest => dest.ProductType, src => src.GetType().Name);
           

            config.NewConfig<House, HouseRequestDTO>();
            config.NewConfig<House, HouesResponseDTO>()
                .Map(dest => dest.AgencyName, src => src.Agency.Name)
                .Map(dest => dest.AgencyEmail, src => src.Agency.Owner.Email)
                .Map(dest => dest.AgencyPhoneNumber, src => src.Agency.Owner.PhoneNumberConfirmed)
                .Map(dest => dest.AgentName, src => src.Agent.User.UserName)
                .Map(dest => dest.AgentEmail, src => src.Agent.User.Email)
                .Map(dest => dest.AgentPhoneNumber, src => src.Agent.User.PhoneNumber);


            config.NewConfig<Apartment, ApartmentRequestDto>();
            config.NewConfig<Apartment, ApartmentResponseDTO>()
                .Map(dest => dest.AgencyName, src => src.Agency.Name)
                .Map(dest => dest.AgencyEmail, src => src.Agency.Owner.Email)
                .Map(dest => dest.AgencyPhoneNumber, src => src.Agency.Owner.PhoneNumberConfirmed)
                .Map(dest => dest.AgentName, src => src.Agent.User.UserName)
                .Map(dest => dest.AgentEmail, src => src.Agent.User.Email)
                .Map(dest => dest.AgentPhoneNumber, src => src.Agent.User.PhoneNumber);
            config.NewConfig<Villa, VillaRequestDto>();
            config.NewConfig<Villa, VillaResponseDTO>()
                .Map(dest => dest.AgencyName, src => src.Agency.Name)
                .Map(dest => dest.AgencyEmail, src => src.Agency.Owner.Email)
                .Map(dest => dest.AgencyPhoneNumber, src => src.Agency.Owner.PhoneNumberConfirmed)
                .Map(dest => dest.AgentName, src => src.Agent.User.UserName)
                .Map(dest => dest.AgentEmail, src => src.Agent.User.Email)
                .Map(dest => dest.AgentPhoneNumber, src => src.Agent.User.PhoneNumber);
            config.NewConfig<tasks, TaskRequestDTO>();
            config.NewConfig<tasks, TaskResponseDTO>();
            config.NewConfig<Agency, AgencyRequestDTO>();

            config.NewConfig<Agency, AgencyResponseDTO>()
                 .Map(dest => dest.AgencyName, src => src.Name)
                 .Map(dest => dest.OwnerName, src => src.Owner.UserName)
                 .Map(dest => dest.OwnerEmail, src => src.Owner.Email)
                 .Map(dest => dest.OwnerPhone, src => src.Owner.PhoneNumber)
                 .Map(dest => dest.SubscriptionId, src => src.SubscriptionId)
                 .Map(dest => dest.SubscriptionType, src => src.Subscription.SubscriptionType)
                 .Map(dest => dest.AgentsNumber, src => src.Agents.Count)
                 .Map(dest => dest.ProductsNumber, src => src.Products.Count);




            config.NewConfig<Agent, AgentRequestDTO>()
                .Map(dest => dest.id, src => src.Id)
                 .Map(dest => dest.name, src => src.User.UserName)
                 .Map(dest => dest.Email, src => src.User.Email)
                 .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber)
                 .Map(dest => dest.AgencyName, src => src.Agency.Name)
                 .Map(dest => dest.IsActive, src => src.IsActive);
            config.NewConfig<Agent, AgentResponseDTO>()
                 .Map(dest => dest.id, src => src.Id)
                 .Map(dest => dest.name, src => src.User.UserName)
                 .Map(dest => dest.Email, src => src.User.Email)
                 .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber)
                 .Map(dest => dest.AgencyName, src => src.Agency.Name)
                 .Map(dest => dest.CreatedDate, src => src.CreatedDate)
                 .Map(dest => dest.IsActive, src => src.IsActive)
                 .Map(dest => dest.ProductsNumber, src => src.Products.Count)
                 .Map(dest => dest.TasksNumber, src => src.Tasks.Count)
                 .Map(dest => dest.Products, src => src.Products)
                 .Map(dest => dest.Tasks, src => src.Tasks);
            config.NewConfig<Subscription, SubscriptionRequestDTO>();
            config.NewConfig<Subscription, SubscriptionResponseDTO>()
                  .Map(dest => dest.Id, src => src.Id);
                 
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
            TypeAdapterConfig.GlobalSettings.Default.IgnoreNullValues(true);



        }

    }
}


