using ArgParser.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArgParser
{
    public static class ArgParser
    {
        static List<ParsableProperty> parsableProperties = new List<ParsableProperty>();

        /// <summary>
        /// Parses a string array into a class containing [CmdArg] adorned properties.
        /// </summary>
        /// <typeparam name="T">Class containing [CmdArg] adorned properties</typeparam>
        /// <param name="args">String array with cmd line arguments and values</param>
        /// <returns></returns>
        public static T ParseArgs<T>(string[] args) where T : new()
        {
            parsableProperties = new List<ParsableProperty>();
            T obj = new T();

            var props = typeof(T).GetProperties()
                .Where(x => x.IsDefined(typeof(CmdArgAttribute)));

            foreach (var prop in props)
            {
                var attrs = prop.GetCustomAttributes<CmdArgAttribute>();
                foreach (var attr in attrs)
                {
                    var isRequired = attr.IsRequired;
                    var parseSuccess = ParseArg(prop, attr, args, obj);
                    CheckRequired(prop, isRequired, parseSuccess);
                    SetParsableProperty(prop, attr, parseSuccess);
                }
            }
            ValidateArgUniqueness();
            RemoveDuplicatesNotSet();
            ValidateGroups();

            return obj;
        }

        private static void RemoveDuplicatesNotSet()
        {
            var groupedProp = parsableProperties.GroupBy(x => x.PropName);
            foreach (var prop in groupedProp)
            {
                var remove = prop.ToList().Where(x => !x.IsSet);
                foreach (var toRemove in remove)
                {
                    parsableProperties.Remove(toRemove);
                }
            }
        }

        private static void ValidateArgUniqueness()
        {
            var propNameGroup = parsableProperties.GroupBy(x => x.PropName);
            foreach (var propName in propNameGroup)
            {
                if (propName.Count(x => x.IsSet) > 1)
                {
                    throw new CmdArgException(propName.FirstOrDefault(x => true).PropName, $"Cannot set same argument more than once. {propName.FirstOrDefault(x => true).PropName}");
                }
            }
        }

        private static bool ParseArg<T>(PropertyInfo prop, CmdArgAttribute attr, string[] args, T obj) where T : new()
        {
            var arg = args.FirstOrDefault(x => x == attr.Argument);

            if (arg != null)
            {
                if (prop.PropertyType == typeof(bool) ||
                    prop.PropertyType == typeof(bool?))
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
                return true;
            }
            return false;
        }

        private static void CheckRequired(PropertyInfo prop, bool isRequired, bool parseSuccess)
        {
            if (isRequired && parseSuccess == false)
            {
                throw new CmdArgRequiredException(prop.Name);
            }
        }

        private static void SetParsableProperty(PropertyInfo prop, CmdArgAttribute attr, bool isSet)
        {
            if (!string.IsNullOrEmpty(attr.Group))
            {
                parsableProperties.Add(new ParsableProperty
                {
                    GroupName = attr.Group,
                    GroupMode = attr.GroupMode,
                    PropName = prop.Name,
                    IsSet = isSet
                });
            }
        }

        private static void ValidateGroups()
        {
            foreach (var group in parsableProperties.GroupBy(x => x.GroupName))
            {
                ValidateIsSameGroupMode(group);

                switch (group.ToList()[0].GroupMode)
                {
                    case CmdArgGroupMode.None:
                        break;
                    case CmdArgGroupMode.OnlyOne:
                        ValidateOnlyOne(group);
                        break;
                    case CmdArgGroupMode.AtleastOne:
                        ValidateAtleastOne(group);
                        break;
                    case CmdArgGroupMode.All:
                        ValidateAll(group);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ValidateAll(IEnumerable<ParsableProperty> group)
        {
            if (!group.All(x => x.IsSet) && !group.All(x => !x.IsSet))
            {
                throw new CmdArgException(group.First().GroupName, $"All arguments need to be defined in {group.First().GroupName}.");
            }
        }

        private static void ValidateAtleastOne(IEnumerable<ParsableProperty> group)
        {
            if (group.Count(x => x.IsSet) == 0)
            {
                throw new CmdArgException(group.First().GroupName, $"Atleast 1 argument needs to be defined in {group.First().GroupName}.");
            }
        }

        private static void ValidateOnlyOne(IEnumerable<ParsableProperty> group)
        {
            if (group.Count(x => x.IsSet) > 1)
            {
                throw new CmdArgException(group.First().GroupName, $"Only 1 argument can be defined in {group.First().GroupName}.");
            }
        }

        private static void ValidateIsSameGroupMode(IEnumerable<ParsableProperty> group)
        {
            var firstMode = group.ToList()[0].GroupMode;
            if (!group.All(x => x.GroupMode == firstMode))
            {
                throw new CmdArgException(group.First().GroupName, $"{group.First().GroupName} defines different GroupModes.");
            }
        }
    }
}
