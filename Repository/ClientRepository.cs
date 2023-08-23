using SimpleDotNETAPI.Data;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.Interfaces;

namespace SimpleDotNETAPI.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly DataContext _context;

        public ClientRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<Client> GetClients()
        {
            return this._context.Clients.OrderBy(c => c.Created).ToList();
        }

        public bool ClientExists(string uid)
        {
            return this._context.Clients.Any(c => c.UID == uid);
        }

        public Client? GetClient(string uid)
        {
            return this._context.Clients.Where(c => c.UID == uid).FirstOrDefault();
        }
        public int GetClientMonthQueriesCount(string clientUid)
        {
            return this.GetClient(clientUid).MonthQueriesCount;
        }

        public ICollection<Plan>? GetClientPlanHistory(string clientUid)
        {
            return this.GetClient(clientUid).Plans.ToList();
        }

        public Plan? GetClientPlan(string clientUid)
        {
            return this.GetClientPlanHistory(clientUid).OrderByDescending(p => p.Created).FirstOrDefault();
        }

        public ICollection<Query>? GetClientQueries(string clientUid)
        {
            return this.GetClientQueries(clientUid, new DateTime(), DateTime.Now);
        }

        public ICollection<Query>? GetClientQueries(string clientUid, DateTime start)
        {
            return this.GetClientQueries(clientUid, start, DateTime.Now);
        }

        public ICollection<Query>? GetClientQueries(string clientUid, DateTime start, DateTime end)
        {
            return this.GetClient(clientUid).Queries.Where(q => q.Created > start && q.Created < end).ToList();
        }

        public ICollection<User>? GetUsers(string clientUid, ICollection<Role>? roles)
        {
            if (roles != null && roles.Count > 0)
            {
                return this.GetClient(clientUid).Users.Where(user =>
                {
                    foreach (var role in roles)
                    {
                        if (user.Role == role)
                            return true;
                    }
                    return false;
                }).ToList();
            }
            else
            {
                return this.GetClient(clientUid).Users;
            }
        }

        public User? GetUser(string clientUid, string userUid)
        {
            ICollection<User> users = this.GetUsers(clientUid, null);
            if (users != null && users.Count > 0)
                foreach (var user in users)
                    if (user.UID == userUid)
                        return user;
            return null;
        }

        public int GetClientQueriesCount(string clientUid)
        {
            return this.GetClientQueriesCount(clientUid, new DateTime());
        }

        public int GetClientQueriesCount(string clientUid, DateTime start)
        {
            return this.GetClientQueriesCount(clientUid, start, DateTime.Now);
        }

        public int GetClientQueriesCount(string clientUid, DateTime start, DateTime end)
        {
            var queries = this.GetClientQueries(clientUid, start, end);
            if (queries == null)
                return 0;
            return queries.Count;
        }

        public bool CreateClient(Client client)
        {
            this._context.Add(client);
            return this.Save();
        }

        public bool Save()
        {
            var saved = this._context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}