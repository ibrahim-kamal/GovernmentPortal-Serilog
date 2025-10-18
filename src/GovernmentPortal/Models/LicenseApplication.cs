using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace GovernmentPortal.Models;

public class LicenseApplication
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public string NationalNumber { get;set; }
    public string Mobile { get;set; }
    public LicenseApplicationStatus Status { get; set; } = LicenseApplicationStatus.Apply;
    public string RejectReason { get; set; } = string.Empty;
    public DateTime SubmittedAt { get; } = DateTime.Now;

    public LicenseApplication(LicenseApplication app) {

        Id = app.Id;
        Name = app.Name;
        NationalNumber = app.NationalNumber;
        Mobile = app.Mobile;
        Status = app.Status;
        RejectReason = app.RejectReason;
        SubmittedAt = app.SubmittedAt;
    }


    public LicenseApplication(string mobile ,string name , string nationalNumber)
    {
        Name = name;
        NationalNumber = nationalNumber;
        Mobile = mobile;
    }
}

public enum LicenseApplicationStatus
{
    Apply,Accept,Reject,Review
}
public class LicenseApplicationViwModel
{
    public string Name { get; set; }
    public string NationalNumber { get; set; }
    public string Mobile { get; set; }
}



public class LicenseApplicationActionViwModel
{
    public Guid Id { get; set; }
    public LicenseApplicationStatus Status { get; set; } = LicenseApplicationStatus.Apply;
    public string RejectReason { get; set; } = string.Empty;

}



public static class ObjectDiffHelper
{
    public static List<PropertyChange> GetChangedProperties<T>(T oldObject, T newObject)
    {
        var changes = new List<PropertyChange>();

        if (oldObject == null || newObject == null)
            return changes;

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            // Skip if property can’t be read or has index parameters
            if (!prop.CanRead || prop.GetIndexParameters().Length > 0)
                continue;

            var oldValue = prop.GetValue(oldObject);
            var newValue = prop.GetValue(newObject);

            // Compare values — handle null safely
            if (!Equals(oldValue, newValue))
            {
                changes.Add(new PropertyChange
                {
                    Name = prop.Name,
                    OldValue = oldValue,
                    NewValue = newValue
                });
            }
        }

        return changes;
    }
}

public class PropertyChange
{
    public string Name { get; set; } = "";
    public object? OldValue { get; set; }
    public object? NewValue { get; set; }
}
