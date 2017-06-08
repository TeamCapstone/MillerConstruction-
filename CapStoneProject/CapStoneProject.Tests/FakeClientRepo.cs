using CapStoneProject.Models;
using CapStoneProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Repositories
{
    public class FakeClientRepo : IClientRepo
    {
        private ApplicationDbContext context;

        public FakeClientRepo (ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Client> Client
        {
            get
            {
                return context.Clients.Include(c => c.UserIdentity).ToList();
            }
        }

        //public List<Client> GetAllClients()
        //{
        //    return Client.ToList();
        //}

        public IQueryable<Client> GetAllClients()
        {

            return context.Clients.Include(c => c.UserIdentity);

        }

        public int Create(Client client)
        {
            Client c = GetClientByEmail(client.Email);
            if (c != null)
                context.Clients.Update(c);
            else
                context.Clients.Add(client);         
            
            return context.SaveChanges();
        }

        public int Update(Client client)
        {
            context.Clients.Update(client);
            return context.SaveChanges();
        }

        public int Delete(int clientId)
        {

            Client client = Client.FirstOrDefault(m => m.ClientID == clientId);
            if (client != null)
            {
                context.Clients.Remove(client);
                return context.SaveChanges();
            }
            return clientId;
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
            return context.Clients.FirstOrDefault(c => c.Email == email);
        }

        public bool ContainsClient(Client client)
        {
            Client confirm = context.Clients.FirstOrDefault(c => c.Email == client.Email);
            //(from c in context.Clients where c.Email == client.Email select c)
            //    .FirstOrDefault<Client>();

            if(confirm != null)
            {
                return true;
            }
            return false;
        }

        UserIdentity user = new UserIdentity
        {
            FirstName = "Henry",
            LastName = "Homes",
            Email = "Hello1@gmail.com",
            Password = "Capstone1!",
            UserName = "Hello1@gmail.com"
        };

        Client client = new Client
        {
            FirstName = "Henry",
            LastName = "Homes",
            CompanyName = "Murder Company Inc.",
            Street = "999 Elm Street",
            City = "Eugene",
            State = "OR",
            Zipcode = "97405",
            PhoneNumber = "(541) 548-9852",
            Email = "Hello1@gmail.com",
            UserIdentity = user
        };
    }
}
