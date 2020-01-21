namespace Corepoint.Plugin
{
    public class Function
    {
        public string FunctionName { get; set; }
        public Parameters Parameters { get; set; }
        public string InputData { get; set; }
    }

    public class Parameters
    {
        public bool GithubFlavored { get; set; }
        public bool RemoveComments { get; set; }
        public bool SmartHrefHandling { get; set; }
        public string UnknownTags { get; set; }
        public string[] WhitelistUriSchemes { get; set; }
        public string TableWithoutHeaderRowHandling { get; set; }
    }
}