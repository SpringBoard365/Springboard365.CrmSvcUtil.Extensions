namespace Springboard365.CrmSvcUtil.Extensions
{
    using System;
    using Microsoft.Crm.Services.Utility;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Metadata;

    public class SingleEntityNamingService : INamingService
    {
        private readonly INamingService namingService;

        public SingleEntityNamingService(INamingService namingService)
        {
            this.namingService = namingService;
        }

        public string GetNameForAttribute(EntityMetadata entityMetadata, AttributeMetadata attributeMetadata, IServiceProvider services)
        {
            return namingService.GetNameForAttribute(entityMetadata, attributeMetadata, services);
        }

        public string GetNameForEntity(EntityMetadata entityMetadata, IServiceProvider services)
        {
            var entityName = SingleEntityConfiguration.Instance.GetEntityAlternateName();

            if (!string.IsNullOrWhiteSpace(entityName))
            {
                return entityName;
            }

            return namingService.GetNameForEntity(entityMetadata, services);
        }

        public string GetNameForEntitySet(EntityMetadata entityMetadata, IServiceProvider services)
        {
            var entityName = SingleEntityConfiguration.Instance.GetEntityAlternateName();

            if (!string.IsNullOrWhiteSpace(entityName))
            {
                return entityName + "Set";
            }

            return namingService.GetNameForEntitySet(entityMetadata, services);
        }

        public string GetNameForMessagePair(SdkMessagePair messagePair, IServiceProvider services)
        {
            return namingService.GetNameForMessagePair(messagePair, services);
        }

        public string GetNameForOption(OptionSetMetadataBase optionSetMetadata, OptionMetadata optionMetadata, IServiceProvider services)
        {
            return namingService.GetNameForOption(optionSetMetadata, optionMetadata, services);
        }

        public string GetNameForOptionSet(EntityMetadata entityMetadata, OptionSetMetadataBase optionSetMetadata, IServiceProvider services)
        {
            return namingService.GetNameForOptionSet(entityMetadata, optionSetMetadata, services);
        }

        public string GetNameForRelationship(EntityMetadata entityMetadata, RelationshipMetadataBase relationshipMetadata, EntityRole? reflexiveRole, IServiceProvider services)
        {
            return namingService.GetNameForRelationship(entityMetadata, relationshipMetadata, reflexiveRole, services);
        }

        public string GetNameForRequestField(SdkMessageRequest request, SdkMessageRequestField requestField, IServiceProvider services)
        {
            return namingService.GetNameForRequestField(request, requestField, services);
        }

        public string GetNameForResponseField(SdkMessageResponse response, SdkMessageResponseField responseField, IServiceProvider services)
        {
            return namingService.GetNameForResponseField(response, responseField, services);
        }

        public string GetNameForServiceContext(IServiceProvider services)
        {
            return namingService.GetNameForServiceContext(services);
        }
    }
}