using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourProjectNamespace.Models;

namespace BLL.Interfaces
{
    public interface IUnitOfWork
    {
       public IRepository<Product> ProductRepository { get; }
        public IRepository<Agency> AgencyRepository { get; }
        public IRepository<Agent> AgentRepository { get; }
        public IRepository<Apartment> ApartmentRepository { get; }
        public IRepository<House> HouseRepository { get; }
        public IRepository<Villa> VillaRepository { get; }
        public IRepository<Payment> PaymentRepository { get; }
        public IRepository<Subscription> SubscriptionRepository { get; }
        public IRepository<tasks> TasksRepository {  get; }
        public IRepository<User> UsersRepository { get; }

        void Save();
    }
}
