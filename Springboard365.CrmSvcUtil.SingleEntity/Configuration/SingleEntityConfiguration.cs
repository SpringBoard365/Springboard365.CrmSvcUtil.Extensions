namespace Springboard365.CrmSvcUtil.Extensions
{
    using System;
    using System.Collections.Generic;

    public class SingleEntityConfiguration : ISingleEntityConfiguration
    {
        private static SingleEntityConfiguration instance;
        private readonly IDictionary<string, string> parameters;

        private SingleEntityConfiguration()
        {
            throw new Exception("Who called this constructor?");
        }

        private SingleEntityConfiguration(IDictionary<string, string> parameters)
        {
            this.parameters = parameters;
        }

        public static SingleEntityConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new Exception("Object not created");
                }

                return instance;
            }
        }

        public static void Create(IDictionary<string, string> parameters)
        {
            if (instance != null)
            {
                throw new Exception("Object already created");
            }

            instance = new SingleEntityConfiguration(parameters);
        }

        public string GetEntityName()
        {
            return parameters["ENTITYNAME"].ToString();
        }

        public string GetEntityAlternateName()
        {
            var entityAlternateName = parameters["ENTITYALTERNATENAME"].ToString();

            if (!string.IsNullOrWhiteSpace(entityAlternateName))
            {
                return entityAlternateName;
            }

            return GetEntityName();
        }
    }
}