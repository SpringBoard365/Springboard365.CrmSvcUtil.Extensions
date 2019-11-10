namespace Springboard365.CrmSvcUtil.Extensions
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Crm.Services.Utility;
    using Microsoft.Xrm.Sdk.Metadata;

    public class SingleEntityCodeGenerationService : ICodeGenerationService
    {
        private readonly ICodeGenerationService codeGenerationService;

        public SingleEntityCodeGenerationService(ICodeGenerationService codeGenerationService, IDictionary<string, string> parameters)
        {
            this.codeGenerationService = codeGenerationService;
            SingleEntityConfiguration.Create(parameters);
        }

        public CodeGenerationType GetTypeForAttribute(EntityMetadata entityMetadata, AttributeMetadata attributeMetadata, IServiceProvider services)
        {
            return codeGenerationService.GetTypeForAttribute(entityMetadata, attributeMetadata, services);
        }

        public CodeGenerationType GetTypeForEntity(EntityMetadata entityMetadata, IServiceProvider services)
        {
            return codeGenerationService.GetTypeForEntity(entityMetadata, services);
        }

        public CodeGenerationType GetTypeForMessagePair(SdkMessagePair messagePair, IServiceProvider services)
        {
            return codeGenerationService.GetTypeForMessagePair(messagePair, services);
        }

        public CodeGenerationType GetTypeForOption(OptionSetMetadataBase optionSetMetadata, OptionMetadata optionMetadata, IServiceProvider services)
        {
            return codeGenerationService.GetTypeForOption(optionSetMetadata, optionMetadata, services);
        }

        public CodeGenerationType GetTypeForOptionSet(EntityMetadata entityMetadata, OptionSetMetadataBase optionSetMetadata, IServiceProvider services)
        {
            return codeGenerationService.GetTypeForOptionSet(entityMetadata, optionSetMetadata, services);
        }

        public CodeGenerationType GetTypeForRequestField(SdkMessageRequest request, SdkMessageRequestField requestField, IServiceProvider services)
        {
            return codeGenerationService.GetTypeForRequestField(request, requestField, services);
        }

        public CodeGenerationType GetTypeForResponseField(SdkMessageResponse response, SdkMessageResponseField responseField, IServiceProvider services)
        {
            return codeGenerationService.GetTypeForResponseField(response, responseField, services);
        }

        public void Write(IOrganizationMetadata organizationMetadata, string language, string outputFile, string targetNamespace, IServiceProvider services)
        {
            var entityName = SingleEntityConfiguration.Instance.GetEntityAlternateName();
            codeGenerationService.Write(organizationMetadata, language, $"{entityName}.cs", targetNamespace, services);
            SingleEntityHelper.Instance.WriteToFile($"{entityName}.cs", $"{entityName}_new.cs");
        }
    }
}