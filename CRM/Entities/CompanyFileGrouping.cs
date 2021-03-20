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
    public class CompanyFileGrouping : DataEntity, IEquatable<CompanyFileGrouping>
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid RowId { get; set; }

        public AppUser Creator { get; set; }
        public Company Company { get; set; }
        public FileType FileType { get; set; }
        public List<CompanyFileMapping> CompanyFileMappings { get; set; }
        public bool Equals(CompanyFileGrouping other)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class CompanyFileGroupingFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Title { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter CompanyId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter FileTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CompanyFileGroupingOrder OrderBy { get; set; }
        public List<CompanyFileGroupingFilter> OrFilter { get; set; }
        public CompanyFileGroupingSelect Selects { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CompanyFileGroupingOrder
    {
        Id = 0,
        Title = 1,
        Description = 2,
        Company = 3,
        Creator = 4,
        FileType = 5,
        CreatedAt = 49,
        UpdatedAt = 50,
    }

    [Flags]
    public enum CompanyFileGroupingSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Title = E._1,
        Description = E._2,
        Company = E._3,
        Creator = E._4,
        FileType = E._5
    }
}
