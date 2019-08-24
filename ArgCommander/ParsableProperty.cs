namespace ArgCommander
{
    internal class ParsableProperty
    {
        public string GroupName { get; set; }
        public CmdArgGroupMode GroupMode { get; set; }
        public string PropName { get; set; }
        public bool IsSet { get; set; }
    }
}
