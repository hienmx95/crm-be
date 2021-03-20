using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class CompanyFileMapping : DataEntity,  IEquatable<CompanyFileMapping>
    {
        public long CompanyFileGroupingId { get; set; }
        public long FileId { get; set; }
        public CompanyFileGrouping CompanyFileGrouping { get; set; }
        public File File { get; set; }

        public bool Equals(CompanyFileMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CompanyFileMappingFilter : FilterEntity
    {
        public IdFilter CompanyFileGroupingId { get; set; }
        public IdFilter FileId { get; set; }
        public List<CompanyFileMappingFilter> OrFilter { get; set; }
        public CompanyFileMappingOrder OrderBy {get; set;}
        public CompanyFileMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompanyFileMappingOrder
    {
        CompanyFileGrouping = 0,
        File = 1,
    }

    [Flags]
    public enum CompanyFileMappingSelect:long
    {
        ALL = E.ALL,
        CompanyFileGrouping = E._0,
        File = E._1,
    }
}
