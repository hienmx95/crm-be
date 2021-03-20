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
    public class CustomerLeadFileGroup : DataEntity, IEquatable<CustomerLeadFileGroup>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CustomerLeadId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }

        public AppUser Creator { get; set; }
        public CustomerLead CustomerLead { get; set; }
        public FileType FileType { get; set; }
        public List<CustomerLeadFileMapping> CustomerLeadFileMappings { get; set; }
        public bool Equals(CustomerLeadFileGroup other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CustomerLeadFileGroupFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter FileTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerLeadFileGroupOrder OrderBy { get; set; }
        public List<CustomerLeadFileGroupFilter> OrFilter { get; set; }
        public CustomerLeadFileGroupSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CustomerLeadFileGroupOrder
    {
        Id = 0,
        Title = 1,
        Description = 2,
        CustomerLead = 3,
        Creator = 4,
        FileType = 5,
        CreatedAt = 49,
        UpdatedAt = 50,
    }

    [Flags]
    public enum CustomerLeadFileGroupSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        Description = E._2,
        CustomerLead = E._3,
        Creator = E._4,
        FileType = E._5
    }
}
