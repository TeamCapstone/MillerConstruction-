using CapStoneProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapStoneProject.Repositories.Interfaces
{
    public interface IClientRepo
    {
        List<Client> GetAllClients();
        int Create(Client client);
        int Update(Client client);
        int Delete(int id);
        Client GetClientById(int id);
        Client GetClientByFirstName(string firstName);
        Client GetClientByLastName(string lastName);
        Client GetClientByEmail(string email);
        bool ContainsClient(Client client);

    }
}
