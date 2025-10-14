using System.Reflection;

namespace GovernmentPortal.Helper
{
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
}
