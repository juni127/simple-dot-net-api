using Microsoft.EntityFrameworkCore;

using SimpleDotNETAPI.Data;
using SimpleDotNETAPI.Models;
using SimpleDotNETAPI.Interfaces;

namespace SimpleDotNETAPI.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly DataContext _context;

        public ApplicationRepository(DataContext context)
        {
            this._context = context;
        }

        public Application? GetApplication(string uid)
        {
            return this._context.Applications.FirstOrDefault(a => a.UID == uid);
        }

        public Application? GetApplicationFromPublicKey(string publicKey)
        {
            return this._context.Applications.Where(a => (a.PublicKey == publicKey)).Include(a => a.RedirectURIs).FirstOrDefault();
        }

        public ICollection<Application>? GetApplications()
        {
            throw new NotImplementedException();
        }

        public void CreateApplication()
        {
            Application application = new Application();
            this._context.Applications.Add(application);
        }
    }
}