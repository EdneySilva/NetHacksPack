namespace NetHacksPack.Integration.Azure
{
    public class AzureConnectionOptions
    {
        public string ConnectionString { get; set; }
        public string Queue { get; set; }
        public bool? AutoComplete { get; set; }
        public int? MaxConcurrent { get; set; }
    }
}
