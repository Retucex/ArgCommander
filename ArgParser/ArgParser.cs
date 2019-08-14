using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using ArgParser.Exceptions;

namespace ArgParser
{
    public static class ArgParser
    {
		private static MethodInfo[] failureMethods;

		/// <summary>
		/// Parses a string array into a class containing [CmdArg] adorned properties.
		/// </summary>
		/// <typeparam name="T">Class containing [CmdArg] adorned properties</typeparam>
		/// <param name="args">String array with cmd line arguments and values</param>
		/// <returns></returns>
		public static T ParseArgs<T>(string[] args) where T : new()
        {
            T obj = new T();

            var props = typeof(T).GetProperties()
				.Where(x => x.IsDefined(typeof(CmdArgAttribute)));

			foreach(var prop in props)
			{
				var attrs = prop.GetCustomAttributes<CmdArgAttribute>();
				foreach (var attr in attrs)
				{
					ParseArg(prop, attr, args, obj);
				}
			}

            return obj;
        }

		private static void ParseArg<T>(PropertyInfo prop, CmdArgAttribute attr, string[] args, T obj) where T : new()
		{
			var arg = args.FirstOrDefault(x => x == attr.Argument);

			if (arg != null)
			{
				if (prop.PropertyType == typeof(bool))
				{
					prop.SetValue(obj, true);
				}
				else
				{
					try
					{
						var i = Array.IndexOf(args, arg);
						object value;
						if (TConverter.TryChangeType(prop.PropertyType, args[i + 1], out value))
						{
							prop.SetValue(obj, value);
						}
                        else
                        {
                            throw new CmdArgException(prop.Name);
                        }
					}
					catch (Exception ex)
					{
                        throw new CmdArgException(prop.Name, ex.Message, ex);
					}
				}
			}
		}
	}
}
