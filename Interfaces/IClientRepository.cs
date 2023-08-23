using SimpleDotNETAPI.Models;

namespace SimpleDotNETAPI.Interfaces
{
    public interface IClientRepository
    {
        ICollection<Client> GetClients();
        bool ClientExists(string uid);
        Client? GetClient(string uid);
        int GetClientMonthQueriesCount(string clientUid);
        ICollection<Plan>? GetClientPlanHistory(string clientUid);
        Plan? GetClientPlan(string clientUid);
        ICollection<Query>? GetClientQueries(string clientUid);
        ICollection<Query>? GetClientQueries(string clientUid, DateTime start);
        ICollection<Query>? GetClientQueries(string clientUid, DateTime start, DateTime end);
        ICollection<User>? GetUsers(string clientUid, ICollection<Role>? roles);
        User? GetUser(string clientUid, string userUid);
        int GetClientQueriesCount(string clientUid);
        int GetClientQueriesCount(string clientUid, DateTime start);
        int GetClientQueriesCount(string clientUid, DateTime start, DateTime end);

        bool CreateClient(Client client);
        bool Save();
    }
}