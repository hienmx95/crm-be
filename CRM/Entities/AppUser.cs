using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace CRM.Entities
{
    public class AppUser : DataEntity, IEquatable<AppUser>
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? PositionId { get; set; }
        public string Department { get; set; }
        public long OrganizationId { get; set; }
        public long? ERouteScopeId { get; set; }
        public long StatusId { get; set; }
        public long SexId { get; set; }
        public DateTime? Birthday { get; set; }
        public long? ProvinceId { get; set; }
        public Guid RowId { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool Used { get; set; }
        public Organization Organization { get; set; }
        public Organization ERouteScope { get; set; }
        public Position Position { get; set; }
        public Province Province { get; set; }
        public Status Status { get; set; }
        public Sex Sex { get; set; }
        public List<AppUserRoleMapping> AppUserRoleMappings { get; set; }

        public bool Equals(AppUser other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class AppUserFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Username { get; set; }
        public StringFilter Password { get; set; }
        public StringFilter DisplayName { get; set; }
        public StringFilter Address { get; set; }
        public StringFilter Email { get; set; }
        public StringFilter Phone { get; set; }
        public IdFilter PositionId { get; set; }
        public StringFilter Department { get; set; }
        public IdFilter OrganizationId { get; set; }
        public IdFilter ERouteScopeId { get; set; }
        public IdFilter SexId { get; set; }
        public IdFilter StatusId { get; set; }
        public IdFilter RoleId { get; set; }
        public IdFilter ProvinceId { get; set; }
        public DateFilter Birthday { get; set; }
        public StringFilter Avatar { get; set; }
        public List<AppUserFilter> OrFilter { get; set; }
        public AppUserOrder OrderBy { get; set; }
        public AppUserSelect Selects { get; set; }
        public DecimalFilter Longitude { get; set; }
        public DecimalFilter Latitude { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AppUserOrder
    {
        Id = 0,
        Username = 1,
        Password = 2,
        DisplayName = 3,
        Address = 4,
        Email = 5,
        Phone = 6,
        Position = 7,
        Department = 8,
        Organization = 9,
        Sex = 10,
        Status = 11,
        Birthday = 12,
        Province = 13,
        ERouteScope = 14,
        Longitude = 15,
        Latitude = 16
    }

    [Flags]
    public enum AppUserSelect : long
    {
        ALL = E.ALL,
        Id = E._0,
        Username = E._1,
        Password = E._2,
        DisplayName = E._3,
        Address = E._4,
        Email = E._5,
        Phone = E._6,
        Position = E._7,
        Department = E._8,
        Organization = E._9,
        Sex = E._10,
        Status = E._11,
        Birthday = E._12,
        RowId = E._13,
        Avatar = E._14,
        Province = E._15,
        Latitude = E._16,
        Longitude = E._17,
        ERouteScope = E._18
    }
}