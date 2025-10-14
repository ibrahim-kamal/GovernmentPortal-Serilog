using GovernmentPortal.Models;

namespace GovernmentPortal.Services;

public class LicenseService : ILicenseService
{
    private readonly Dictionary<Guid, LicenseApplication> _applications = new();

    public IEnumerable<LicenseApplication> GetApplications()
    {
        foreach (var (appId,app) in _applications)
        {
            yield return app;
        }
    }
    public LicenseApplication GetApplication(Guid id)
    {
        return _applications.GetValueOrDefault(id);
    }

    public void UpdateApplication(LicenseApplication application)
    {
        _applications[application.Id] = application;
    }

    public void SubmitApplication(LicenseApplication application)
    {
        _applications[application.Id] = application;
    }
}
