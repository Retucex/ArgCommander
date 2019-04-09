using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ArgParser
{
	/// <summary>
	/// Class to help convert generics
	/// </summary>
    static class TConverter
    {
		public static bool TryChangeType<T>(object value, out T parsedValue)
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

		public static bool TryChangeType(Type t, object value, out object parsedValue)
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

		public static T ChangeType<T>(object value)
        {
            return (T)ChangeType(typeof(T), value);
        }

        public static object ChangeType(Type t, object value)
        {
            TypeConverter tc = TypeDescriptor.GetConverter(t);
            return tc.ConvertFrom(value);
        }

        public static void RegisterTypeConverter<T, TC>() where TC : TypeConverter
        {

            TypeDescriptor.AddAttributes(typeof(T), new TypeConverterAttribute(typeof(TC)));
        }
    }
}
