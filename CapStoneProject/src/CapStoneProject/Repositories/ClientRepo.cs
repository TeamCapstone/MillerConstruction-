using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Repositories
{
    public class ClientRepo : IClientRepo
    {
        private ApplicationDbContext context;

        public ClientRepo (ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Client> Client
        {
            get
            {
                return context.Clients.ToList();
            }
        }

        public List<Client> GetAllClients()
        {
            return Client.ToList();
        }

        public int Create(Client client)
        {
            context.Clients.Add(client);
            return context.SaveChanges();
        }
    }
}
