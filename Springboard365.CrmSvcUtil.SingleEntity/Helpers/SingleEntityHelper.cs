﻿namespace Springboard365.CrmSvcUtil.Extensions
{
    using System;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;

    public  class SingleEntityHelper
    {
        private readonly RegexConfig[] stringConfigs = new RegexConfig[]
        {
            new RegexConfig("string", "//------------------------------------------------------------------------------", string.Empty),
            new RegexConfig("string", "// <auto-generated>", string.Empty),
            new RegexConfig("string", "//     This code was generated by a tool.", string.Empty),
            new RegexConfig("string", "//     Runtime Version:4.0.30319.42000", string.Empty),
            new RegexConfig("regex",  "^//$", string.Empty),
            new RegexConfig("string", "//     Changes to this file may cause incorrect behavior and will be lost if", string.Empty),
            new RegexConfig("string", "//     the code is regenerated.", string.Empty),
            new RegexConfig("string", "// </auto-generated>", string.Empty),
            new RegexConfig("string", "public partial class", "public class"),
            new RegexConfig("string", "this.", string.Empty),
            new RegexConfig("string", "Microsoft.Xrm.Sdk.Client.", string.Empty), 
            new RegexConfig("string", "Microsoft.Xrm.Sdk.", string.Empty),
            new RegexConfig("string", "System.Runtime.Serialization.", string.Empty),
            new RegexConfig("string", "System.Collections.Generic.", string.Empty),
            new RegexConfig("string", "System.Linq.", string.Empty),
            new RegexConfig("string", "System.Nullable", "Nullable"),
            new RegexConfig("string", "System.Guid", "Guid"),
            new RegexConfig("string", "System.DateTime", "DateTime"),
            new RegexConfig("string", "System.Enum", "Enum"),
            new RegexConfig("string", "System.CodeDom.Compiler.", string.Empty),
            new RegexConfig("string", "GeneratedCodeAttribute", "GeneratedCode"),
            new RegexConfig("string", "Springboard365.Xrm.Entities.", string.Empty),
            new RegexConfig("regex",  Regex.Escape("[GeneratedCode(\"CrmSvcUtil\", \"9.1.0.25\")]"), string.Empty),
            new RegexConfig("string", "DataContractAttribute()", "DataContract"),
            new RegexConfig("string", "AttributeLogicalNameAttribute", "AttributeLogicalName"),
            new RegexConfig("string", "EntityLogicalNameAttribute", "EntityLogicalName"),
            new RegexConfig("string", "RelationshipSchemaNameAttribute", "RelationshipSchemaName"),
            new RegexConfig("string", "EnumMemberAttribute()", "EnumMember"),
            new RegexConfig("string", "ProxyTypesAssemblyAttribute()", "ProxyTypesAssembly"),
            new RegexConfig("string", "public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;", string.Empty),
            new RegexConfig("string", "public event System.ComponentModel.PropertyChangingEventHandler PropertyChanging;", string.Empty),
            new RegexConfig("string", ", System.ComponentModel.INotifyPropertyChanged", string.Empty),
            new RegexConfig("string", ", System.ComponentModel.INotifyPropertyChanging", string.Empty),
            new RegexConfig("regex",  @"^(\s)+OnPropertyChanging\(""(\w)+""\)\;", string.Empty),
            new RegexConfig("regex",  @"^(\s)+OnPropertyChanged\(""(\w)+""\)\;", string.Empty),
            new RegexConfig("regex",  @"public const int EntityTypeCode = (\d)+;", string.Empty),
            new RegexConfig("string", "Nullable<Guid>", "Guid?"),
            new RegexConfig("string", "Nullable<double>", "double?"),
            new RegexConfig("string", "Nullable<int>", "int?"),
            new RegexConfig("string", "Nullable<DateTime>", "DateTime?"),
            new RegexConfig("string", "Nullable<bool>", "bool?"),
            new RegexConfig("string", "Nullable<long>", "long?"),
            new RegexConfig("string", "Nullable<decimal>", "decimal?"),
        };

        private readonly RegexConfig[] fullDocumentRegexConfigs = new RegexConfig[]
        {
            new RegexConfig("regex", @"^.*\/\/\/ ?<summary>.*\n(?:^.*\/\/\/.*$\n)*", string.Empty),
            new RegexConfig("regex", @"^(\s)+private.*\n(.*\n){6}", string.Empty),
            //new RegexConfig("regex", Regex.Escape("\r\n(\\s)+\r\n"), string.Empty),
            //new RegexConfig("regex", Regex.Escape("{\r\n(\\s){0,4}\r\n(\\s){2}"), "{\r\n\t\t"),
        };

        private SingleEntityHelper()
        {
        }

        public static SingleEntityHelper Instance { get; } = new SingleEntityHelper();

        public void WriteToFile(string fileName)
        {
            WriteToFile(fileName, fileName);
        }

        public void WriteToFile(string fileName, string outputFile)
        {
            File.WriteAllText(outputFile, SearchAndReplaceLineByLine(fileName));

            File.WriteAllText(outputFile, SearchAndReplaceFullDocument(outputFile));

            File.WriteAllText(outputFile, AddNamespaces(outputFile));
        }

        private string AddNamespaces(string fileName)
        {
            var stringBuilder = new StringBuilder();
            var namespaces = new string[]
            {
                "using System;",
                "using System.Runtime.Serialization;",
                "using Microsoft.Xrm.Sdk;",
                "using Microsoft.Xrm.Sdk.Client;",
            };

            foreach (var ns in namespaces)
            {
                stringBuilder.AppendLine(ns);
            }

            foreach (var line in File.ReadLines(fileName))
            {
                stringBuilder.AppendLine(line);
            }

            return stringBuilder.ToString();
        }

        private string SearchAndReplaceLineByLine(string fileName)
        {
            var stringBuilder = new StringBuilder();

            foreach (var line in File.ReadLines(fileName))
            {
                var lineToAdd = line;

                bool isLineEmpty = string.IsNullOrWhiteSpace(lineToAdd);

                foreach (var stringConfig in stringConfigs)
                {
                    if (stringConfig.Type.Equals("regex"))
                    {
                        lineToAdd = Regex.Replace(lineToAdd, stringConfig.Key, stringConfig.Value, RegexOptions.Singleline);
                    }
                    else if (stringConfig.Type.Equals("string"))
                    {
                        lineToAdd = lineToAdd.Replace(stringConfig.Key, stringConfig.Value);
                    }
                }

                if (isLineEmpty)
                {
                    stringBuilder.AppendLine(string.Empty);
                }

                if (!isLineEmpty && !string.IsNullOrWhiteSpace(lineToAdd))
                {
                    stringBuilder.AppendLine(lineToAdd);
                }
            }

            return stringBuilder.ToString();
        }

        private string SearchAndReplaceFullDocument(string fileName)
        {
            var fullDocument = string.Empty;

            using (var streamReader = new StreamReader(fileName))
            {
                fullDocument = streamReader.ReadToEnd();

                foreach (var regexConfig in fullDocumentRegexConfigs)
                {
                    if (regexConfig.Type.Equals("regex"))
                    {
                        fullDocument = Regex.Replace(fullDocument, regexConfig.Key, regexConfig.Value, RegexOptions.Multiline);
                    }
                    else if (regexConfig.Type.Equals("string"))
                    {
                        fullDocument = fullDocument.Replace(regexConfig.Key, regexConfig.Value);
                    }
                }
            }

            return fullDocument;
        }
    }
}