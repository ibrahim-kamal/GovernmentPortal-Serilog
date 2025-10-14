using GovernmentPortal.Models;

namespace GovernmentPortal.Services;

public interface ILicenseService
{
    IEnumerable<LicenseApplication> GetApplications();
    LicenseApplication GetApplication(Guid id);
    void UpdateApplication(LicenseApplication application);
    void SubmitApplication(LicenseApplication application);
}
