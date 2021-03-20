using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CustomerLeadFileMapping : DataEntity,  IEquatable<CustomerLeadFileMapping>
    {
        public long CustomerLeadFileGroupId { get; set; }
        public long FileId { get; set; }
        public CustomerLeadFileGroup CustomerLeadFileGroup { get; set; }
        public File File { get; set; }

        public bool Equals(CustomerLeadFileMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CustomerLeadFileMappingFilter : FilterEntity
    {
        public IdFilter CustomerLeadFileGroupId { get; set; }
        public IdFilter FileId { get; set; }
        public List<CustomerLeadFileMappingFilter> OrFilter { get; set; }
        public CustomerLeadFileMappingOrder OrderBy {get; set;}
        public CustomerLeadFileMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerLeadFileMappingOrder
    {
        CustomerLeadFileGroup = 0,
        File = 1,
    }

    [Flags]
    public enum CustomerLeadFileMappingSelect:long
    {
        ALL = E.ALL,
        CustomerLeadFileGroup = E._0,
        File = E._1,
    }
}
