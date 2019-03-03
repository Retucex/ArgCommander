using System;
using System.Reflection;
using System.Linq;

namespace ArgParser
{
    public static class ArgParser
    {
		/// <summary>
		/// Parses a string array into a class containing [CmdArg] adorned prperties.
		/// </summary>
		/// <typeparam name="T">Class containing [CmdArg] adorned prperties</typeparam>
		/// <param name="args">String array with cmd line arguments and values</param>
		/// <returns></returns>
		public static T ParseArgs<T>(string[] args) where T : new()
        {
            T obj = new T();

            var props = typeof(T).GetProperties()
				.Where(x => x.IsDefined(typeof(CmdArgAttribute)));

			foreach(var prop in props)
			{
				ParseArg(prop, args, obj);
			}

            return obj;
        }

		private static void ParseArg<T>(PropertyInfo prop, string[] args, T obj) where T : new()
		{
			var attr = prop.GetCustomAttribute<CmdArgAttribute>();
			var arg = args.FirstOrDefault(x => x == attr.Argument);

			if (arg != null)
			{
				if (attr.Mode == CmdArgMode.IsFlag)
				{
					prop.SetValue(obj, true);
				}
				else if (attr.Mode == CmdArgMode.HasValue)
				{
					var i = Array.IndexOf(args, arg);
					var value = TConverter.ChangeType(prop.PropertyType, args[i + 1]);
					prop.SetValue(obj, value);
				}
				else
				{
					throw new ArgumentException("Unexpected value CmdArgMode");
				}
			}
		}

		private static PropertyInfo GetPropWithAttrib(string arg, PropertyInfo[] props)
        {
            return props.FirstOrDefault(x =>
            {
                var attr = x.GetCustomAttribute<CmdArgAttribute>();
                if (attr == null)
                {
                    return false;
                }
                if (attr.Argument == arg)
                {
                    return true;
                }
                return false;
            });
        }
    }
}
