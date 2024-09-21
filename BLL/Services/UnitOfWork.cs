using BLL.Interfaces;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly Context context;
        public UnitOfWork(Context context , IRepository<Product> ProductRepository,
                                    IRepository<Agency> AgencyRepository, IRepository<Agent> AgentRepository
                                   , IRepository<Apartment> ApartmentRepository, IRepository<House> HouseRepository
                                    , IRepository<Villa> VillaRepository, IRepository<Payment> PaymentRepository 
                                      , IRepository<Subscription> SubscriptionRepository , IRepository<tasks> TasksRepository)
        {
            
            this.context = context;
           this.ProductRepository = ProductRepository;
            this.AgencyRepository = AgencyRepository;
            this.AgentRepository = AgentRepository;
            this.HouseRepository = HouseRepository;
            this.PaymentRepository = PaymentRepository;
            this.SubscriptionRepository = SubscriptionRepository;
            this.TasksRepository = TasksRepository;
            this.VillaRepository = VillaRepository;
            this.ApartmentRepository = ApartmentRepository;
        }
       public IRepository<Product> ProductRepository { get;}
        public IRepository<Agency> AgencyRepository {  get;}
        public IRepository<Agent> AgentRepository { get;}
        public IRepository<Apartment> ApartmentRepository { get;}
        public IRepository<House> HouseRepository { get;}
        public IRepository<Villa> VillaRepository { get;}
        public IRepository<Payment> PaymentRepository { get; }
        public IRepository<Subscription> SubscriptionRepository { get; }
        public IRepository<tasks> TasksRepository { get; }

       public void Save()
        {
            context.SaveChanges();
        }
    }
}
