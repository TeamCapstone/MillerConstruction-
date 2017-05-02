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

        public Client GetClientById(int id)
        {
            return Client.FirstOrDefault(c => c.ClientID == id);
        }

        public Client GetClientByFirstName(string firstName)
        {
            return Client.FirstOrDefault(c => c.FirstName == firstName);
        }

        public Client GetClientByLastName(string lastName)
        {
            return Client.FirstOrDefault(c => c.LastName == lastName);
        }

        public Client GetClientByEmail(string email)
        {
            return Client.FirstOrDefault(c => c.Email == email);
        }
    }
}
