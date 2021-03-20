using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class File : DataEntity,  IEquatable<File>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long? AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public byte[] Content { get; set; }
        public string Path { get; set; }
        public Guid RowId { get; set; }
        public bool Equals(File other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class FileFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Url { get; set; }
        public IdFilter AppUserId { get; set; }
        public IdFilter GroupId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<FileFilter> OrFilter { get; set; }
        public FileOrder OrderBy {get; set;}
        public FileSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum FileOrder
    {
        Id = 0,
        Name = 1,
        Url = 2,
        AppUser = 3,
        Group = 4,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum FileSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Name = E._1,
        Url = E._2,
        AppUser = E._3,
        Group = E._4,
    }
}
