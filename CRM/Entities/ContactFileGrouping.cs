using CRM.Common;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CRM.Entities
{
    public class ContactFileGrouping : DataEntity, IEquatable<ContactFileGrouping>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long ContactId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }

        public AppUser Creator { get; set; }
        public Contact Contact { get; set; }
        public FileType FileType { get; set; }
        public List<ContactFileMapping> ContactFileMappings { get; set; }
        public bool Equals(ContactFileGrouping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class ContactFileGroupingFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter FileTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ContactFileGroupingOrder OrderBy { get; set; }
        public List<ContactFileGroupingFilter> OrFilter { get; set; }
        public ContactFileGroupingSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContactFileGroupingOrder
    {
        Id = 0,
        Title = 1,
        Description = 2,
        Contact = 3,
        Creator = 4,
        FileType = 5,
        CreatedAt = 49,
        UpdatedAt = 50,
    }

    [Flags]
    public enum ContactFileGroupingSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        Description = E._2,
        Contact = E._3,
        Creator = E._4,
        FileType = E._5
    }
}
