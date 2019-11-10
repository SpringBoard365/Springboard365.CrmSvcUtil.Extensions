namespace Springboard365.CrmSvcUtil.Extensions
{
    using System.Collections.Generic;

    public class EntityDetail
    {
        public EntityDetail(string name, bool enabled)
        {
            EntityName = name;
            DisplayName = name;
            AttributeDetails = new List<AttributeDetail>();
            Enabled = enabled; 
        }

        public bool Enabled { get; set; }

        public string EntityName { get; set; }

        public string DisplayName { get; set; }

        public List<AttributeDetail> AttributeDetails { get; set; }
    }
}