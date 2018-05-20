using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Plain.Library.Reflection
{
    public class Mapping
    {
        public static void Map<TSource, TDestiny>(TSource source, TDestiny destiny)
            where TSource : class
            where TDestiny : class
        {
            var sourceType = source.GetType();
            var destinyType = destiny.GetType();

            var destinyProperties = new List<PropertyInfo>(destinyType.GetProperties());

            foreach (var propertyInfo in sourceType.GetProperties())
            {
                var pinfo = propertyInfo;
                var destProp = destinyProperties.Find(x => x.Name == pinfo.Name);
                if (destProp != null && destProp.CanWrite && destProp.PropertyType == pinfo.PropertyType)
                {
                    destProp.SetValue(destiny, pinfo.GetValue(source, null), null);
                }
            }
        }
    }
}
