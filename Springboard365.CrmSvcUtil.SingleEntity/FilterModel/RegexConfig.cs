namespace Springboard365.CrmSvcUtil.Extensions
{
    internal class RegexConfig
    {
        public RegexConfig(string typeString, string key, string value)
        {
            Type = typeString;
            Key = key;
            Value = value;
        }

        public string Type { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}