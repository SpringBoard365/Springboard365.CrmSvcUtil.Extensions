namespace Springboard365.CrmSvcUtil.Extensions
{
    public class AttributeDetail
    {
        public AttributeDetail(string name, bool enabled)
        {
            AttributeName = name;
            DisplayName = name;
            Enabled = enabled;
        }

        public bool Enabled { get; set; }

        public string AttributeName { get; set; }

        public string DisplayName { get; set; }
    }
}