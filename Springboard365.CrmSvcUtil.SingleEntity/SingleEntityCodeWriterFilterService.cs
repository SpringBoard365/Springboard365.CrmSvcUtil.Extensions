namespace Springboard365.CrmSvcUtil.Extensions
{
    using System;
    using Microsoft.Crm.Services.Utility;
    using Microsoft.Xrm.Sdk.Metadata;

    public class SingleEntityCodeWriterFilterService : ICodeWriterFilterService
    {
        private readonly ICodeWriterFilterService codeWriterFilterService;

        public SingleEntityCodeWriterFilterService(ICodeWriterFilterService codeWriterFilterService)
        {
            this.codeWriterFilterService = codeWriterFilterService;
        }

        public bool GenerateAttribute(AttributeMetadata attributeMetadata, IServiceProvider services)
        {
            return codeWriterFilterService.GenerateAttribute(attributeMetadata, services);
        }

        public bool GenerateEntity(EntityMetadata entityMetadata, IServiceProvider services)
        {
            var entityName = SingleEntityConfiguration.Instance.GetEntityName();
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return codeWriterFilterService.GenerateEntity(entityMetadata, services);
            }

            return entityName.ToLowerInvariant().Equals(entityMetadata.LogicalName.ToLowerInvariant());
        }

        public bool GenerateOption(OptionMetadata optionMetadata, IServiceProvider services)
        {
            return codeWriterFilterService.GenerateOption(optionMetadata, services);
        }

        public bool GenerateOptionSet(OptionSetMetadataBase optionSetMetadata, IServiceProvider services)
        {
            return codeWriterFilterService.GenerateOptionSet(optionSetMetadata, services);
        }

        public bool GenerateRelationship(RelationshipMetadataBase relationshipMetadata, EntityMetadata otherEntityMetadata, IServiceProvider services)
        {
            return false;
        }

        public bool GenerateServiceContext(IServiceProvider services)
        {
            return false;
        }
    }
}