using System;
using System.Collections.Generic;
using CRM.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CRM.Entities
{
    public class TicketOfDepartment : DataEntity,  IEquatable<TicketOfDepartment>
    {
        public long Id { get; set; }
        public string Notes { get; set; }
        public long DepartmentId { get; set; }
        public long TicketId { get; set; }
        public long TicketStatusId { get; set; }
        public Organization Department { get; set; }
        public Ticket Ticket { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool Equals(TicketOfDepartment other)
        {
            return other != null && Id == other.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    public class TicketOfDepartmentFilter : FilterEntity
    {
        public IdFilter Id { get; set; }
        public StringFilter Notes { get; set; }
        public IdFilter DepartmentId { get; set; }
        public IdFilter TicketId { get; set; }
        public IdFilter TicketStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public List<TicketOfDepartmentFilter> OrFilter { get; set; }
        public TicketOfDepartmentOrder OrderBy {get; set;}
        public TicketOfDepartmentSelect Selects {get; set;}
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum TicketOfDepartmentOrder
    {
        Id = 0,
        Notes = 1,
        Department = 2,
        Ticket = 3,
        TicketStatus = 4,
        CreatedAt = 50,
        UpdatedAt = 51,
    }

    [Flags]
    public enum TicketOfDepartmentSelect:long
    {
        ALL = E.ALL,
        Id = E._0,
        Notes = E._1,
        Department = E._2,
        Ticket = E._3,
        TicketStatus = E._4,
    }
}
