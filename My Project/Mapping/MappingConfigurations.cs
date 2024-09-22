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

        }

    }
}


