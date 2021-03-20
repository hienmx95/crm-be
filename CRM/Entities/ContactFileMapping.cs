using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class ContactFileMapping : DataEntity,  IEquatable<ContactFileMapping>
    {
        public long ContactFileGroupingId { get; set; }
        public long FileId { get; set; }
        public ContactFileGrouping ContactFileGrouping { get; set; }
        public File File { get; set; }

        public bool Equals(ContactFileMapping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class ContactFileMappingFilter : FilterEntity
    {
        public IdFilter ContactFileGroupingId { get; set; }
        public IdFilter FileId { get; set; }
        public List<ContactFileMappingFilter> OrFilter { get; set; }
        public ContactFileMappingOrder OrderBy {get; set;}
        public ContactFileMappingSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContactFileMappingOrder
    {
        ContactFileGrouping = 0,
        File = 1,
    }

    [Flags]
    public enum ContactFileMappingSelect:long
    {
        ALL = E.ALL,
        ContactFileGrouping = E._0,
        File = E._1,
    }
}
