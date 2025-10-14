using GovernmentPortal.Models;
using GovernmentPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using static System.Net.Mime.MediaTypeNames;

namespace GovernmentPortal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LicensingController : ControllerBase
{
    private readonly ILicenseService _licenseService;

    public LicensingController(ILicenseService licenseService)
    {
        _licenseService = licenseService;
    }

    [HttpGet("")]
    public IActionResult GetApplications()
    {
        var apps = _licenseService.GetApplications();
        return Ok(apps);
    }

    [HttpGet("{id}")]
    public IActionResult GetApplication(Guid id)
    {
        var app = _licenseService.GetApplication(id);
        return Ok(app);
    }

    [HttpPost]
    public IActionResult SubmitApplication([FromBody] LicenseApplicationViwModel app)
    {
        var application = new LicenseApplication
        {
            Mobile = app.Mobile,
            Name = app.Name,
            NationalNumber = app.NationalNumber
        };
        _licenseService.SubmitApplication(application);
        Log.Information("new License Application Was Submitted {@info}", app);
        return Ok(new { Message = "Application submitted", Id = application.Id });
    }

    [HttpPut("")]
    public IActionResult UpdateApplicationStatus([FromBody] LicenseApplicationActionViwModel action)
    {
        if (action.Status == LicenseApplicationStatus.Reject && string.IsNullOrWhiteSpace(action.RejectReason))
        {
            Log.Error("app id is {id} Reason Is Required at {@datetime}" ,action.Id,  DateTime.Now);
            return BadRequest();

        }
        var application = _licenseService.GetApplication(action.Id);
        if (application == null) {
            Log.Error("Application is null at {Time}. Id: {Id}", DateTime.UtcNow, action.Id);
            return BadRequest();
        }
        var before = application;
        application.Status = action.Status;
        if(application.Status == LicenseApplicationStatus.Reject)
            application.RejectReason = action.RejectReason;

        _licenseService.UpdateApplication(application);
        var chanes = ObjectDiffHelper.GetChangedProperties(before, application);
        Log.Information("License Application {@ApplicationId} with National Number {@NationalNumber} was updated. Changes: {@Changes} at {@UpdatedAt}", application.Id , application.NationalNumber, chanes, DateTime.Now);
        return Ok(new { Message = "Application updated" });
    }
}
