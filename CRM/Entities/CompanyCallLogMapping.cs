using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CompanyCallLogMapping : DataEntity,  IEquatable<CompanyCallLogMapping>
    {
        public long CompanyId { get; set; }
        public long CallLogId { get; set; }
        public Company Company { get; set; }
        public CallLog CallLog { get; set; }

        public bool Equals(CompanyCallLogMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CompanyCallLogMappingFilter : FilterEntity
    {
        public IdFilter CompanyId { get; set; }
        public IdFilter CallLogId { get; set; }
        public List<CompanyCallLogMappingFilter> OrFilter { get; set; }
        public CompanyCallLogMappingOrder OrderBy {get; set;}
        public CompanyCallLogMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompanyCallLogMappingOrder
    {
        Company = 0,
        CallLog = 1,
    }

    [Flags]
    public enum CompanyCallLogMappingSelect:long
    {
        ALL = E.ALL,
        Company = E._0,
        CallLog = E._1,
    }
}
