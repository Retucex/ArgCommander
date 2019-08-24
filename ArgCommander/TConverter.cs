using System;
using System.ComponentModel;

namespace ArgCommander
{
    /// <summary>
    /// Class to help convert generics
    /// </summary>
    internal static class TConverter
    {
        internal static bool TryChangeType<T>(object value, out T parsedValue)
        {
            try
            {
                parsedValue = (T)ChangeType(typeof(T), value);
                return true;
            }
            catch
            {
                parsedValue = default(T);
                return false;
            }
        }

        internal static bool TryChangeType(Type t, object value, out object parsedValue)
        {
            try
            {
                parsedValue = ChangeType(t, value);
                return true;
            }
            catch
            {
                if (t.IsValueType)
                {
                    parsedValue = Activator.CreateInstance(t);
                }
                else
                {
                    parsedValue = null;
                }
                return false;
            }
        }

        internal static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value);
        }

        internal static object ChangeType(Type t, object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(t);
            return tc.ConvertFrom(value);
        }

        internal static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {

            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
    }
}
