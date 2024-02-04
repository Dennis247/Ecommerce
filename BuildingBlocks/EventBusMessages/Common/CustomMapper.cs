using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventBusMessages.Common
{
    public class CustomMapper
    {
        public static void MapProperties(object source, object destination)
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                PropertyInfo destinationProperty = Array.Find(destinationProperties, p => p.Name == sourceProperty.Name);

                if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType && destinationProperty.CanWrite)
                {
                    object value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                }
            }
        }

        public static void MapListProperties<T1, T2>(List<T1> sourceList, List<T2> destinationList)
        {
            PropertyInfo[] sourceProperties = typeof(T1).GetProperties();
            PropertyInfo[] destinationProperties = typeof(T2).GetProperties();

            foreach (var sourceItem in sourceList)
            {
                var destinationItem = Activator.CreateInstance<T2>();

                foreach (PropertyInfo sourceProperty in sourceProperties)
                {
                    PropertyInfo destinationProperty = Array.Find(destinationProperties, p => p.Name == sourceProperty.Name);

                    if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
                    {
                        object value = sourceProperty.GetValue(sourceItem);
                        destinationProperty.SetValue(destinationItem, value);
                    }
                }

                destinationList.Add(destinationItem);
            }
        }
    }
}
