using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace ArgParser
{
    public static class ArgParser
    {
		private static MethodInfo[] failureMethods;

		/// <summary>
		/// Parses a string array into a class containing [CmdArg] adorned properties.
		/// </summary>
		/// <typeparam name="T">Class containing [CmdArg] adorned prperties</typeparam>
		/// <param name="args">String array with cmd line arguments and values</param>
		/// <returns></returns>
		public static T ParseArgs<T>(string[] args) where T : new()
        {
			GetTFailureMethods(typeof(T));

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
							CallFailureMethods(obj);
						}
					}
					catch (Exception ex)
					{

						CallFailureMethods(obj);
					}
				}
			}
		}

		private static void GetTFailureMethods(Type type)
		{
			failureMethods = type.GetMethods()
				.Where(x => x.IsDefined(typeof(CmdArgOnFailureAttribute)))
				.ToArray();
		}

		private static void CallFailureMethods<T>(T obj)
		{
			if(failureMethods.Length > 0)
			{
				foreach(var method in failureMethods)
				{
					try
					{
						method.Invoke(obj, new object[] { });
					}
					catch (Exception ex)
					{
						if(ex.InnerException != null)
						{
							throw ex.InnerException;
						}
					}
				}
			}
			else
			{
				DefaultFailureMethod();
			}
		}

		private static void DefaultFailureMethod()
		{
			throw new CmdArgException("Arguments invalid");
		}
	}
}
