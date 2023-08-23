using SimpleDotNETAPI.Models;

namespace SimpleDotNETAPI.Interfaces
{
    public interface IApplicationRepository
    {
        ICollection<Application>? GetApplications();
        Application? GetApplication(string uid);
        void CreateApplication();
        Application? GetApplicationFromPublicKey(string publicKey);
    }
}