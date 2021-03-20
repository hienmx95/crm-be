using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class MailTemplate : DataEntity,  IEquatable<MailTemplate>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long? StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(MailTemplate other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class MailTemplateFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Content { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<MailTemplateFilter> OrFilter { get; set; }
        public MailTemplateOrder OrderBy {get; set;}
        public MailTemplateSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum MailTemplateOrder
    {
        Id = 0,
        Code = 1,
        Name = 2,
        Content = 3,
        Status = 4,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum MailTemplateSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Code = E._1,
        Name = E._2,
        Content = E._3,
        Status = E._4,
    }
}
