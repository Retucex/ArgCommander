using System;

namespace ArgCommander
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CmdArgAttribute : Attribute
    {
        public string Argument { get; }
        public bool IsRequired { get; }
        public string Group { get; }
        public CmdArgGroupMode GroupMode { get; }

        /// <summary>
        /// Makes the property targetable by ArgCommander.ParseArgs<T>() to receive the value or flag passed as a command line argument.
        /// </summary>
        /// <param name="argument">String passed by command line argument representing a property.</param>
        /// <param name="isRequired">Set to true to make the argument required. Throws CmdArgRequired if the argument is missing.</param>
        /// <param name="group">Identifies the group the argument belongs to.</param>
        /// <param name="groupMode">Defines the behavior of the group.</param>
        public CmdArgAttribute(string argument, bool isRequired, string group, CmdArgGroupMode groupMode)
        {
            Argument = argument;
            IsRequired = isRequired;
            Group = group;
            GroupMode = groupMode;
        }

        /// <summary>
        /// Makes the property targetable by ArgCommander.ParseArgs<T>() to receive the value or flag passed as a command line argument.
        /// </summary>
        /// <param name="argument">String passed by command line argument representing a property.</param>
        /// <param name="isRequired">Set to true to make the argument required. Throws CmdArgRequired if the argument is missing.</param>
        public CmdArgAttribute(string argument, bool isRequired)
            : this(argument, isRequired, null, CmdArgGroupMode.None) { }

        /// <summary>
        /// Makes the property targetable by ArgCommander.ParseArgs<T>() to receive the value or flag passed as a command line argument.
        /// </summary>
        /// <param name="argument">String passed by command line argument representing a property.</param>
        public CmdArgAttribute(string argument)
            : this(argument, false, null, CmdArgGroupMode.None) { }

    }
}
