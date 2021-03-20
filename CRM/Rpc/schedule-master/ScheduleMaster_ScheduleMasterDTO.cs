using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.schedule_master
{
    public class ScheduleMaster_ScheduleMasterDTO : DataDTO
    {
        public long Id { get; set; }
        public long? ManagerId { get; set; }
        public long? SalerId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public long? StatusId { get; set; }
        public DateTime? RecurDays { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? NoEndDate { get; set; }
        public DateTime? StartDayOfWeek { get; set; }
        public long? DisplayOrder { get; set; }
        public string Description { get; set; }
        public ScheduleMaster_AppUserDTO Manager { get; set; }
        public ScheduleMaster_AppUserDTO Saler { get; set; }
        public ScheduleMaster_StatusDTO Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ScheduleMaster_ScheduleMasterDTO() {}
        public ScheduleMaster_ScheduleMasterDTO(ScheduleMaster ScheduleMaster)
        {
            this.Id = ScheduleMaster.Id;
            this.ManagerId = ScheduleMaster.ManagerId;
            this.SalerId = ScheduleMaster.SalerId;
            this.Name = ScheduleMaster.Name;
            this.Code = ScheduleMaster.Code;
            this.StatusId = ScheduleMaster.StatusId;
            this.RecurDays = ScheduleMaster.RecurDays;
            this.StartDate = ScheduleMaster.StartDate;
            this.EndDate = ScheduleMaster.EndDate;
            this.NoEndDate = ScheduleMaster.NoEndDate;
            this.StartDayOfWeek = ScheduleMaster.StartDayOfWeek;
            this.DisplayOrder = ScheduleMaster.DisplayOrder;
            this.Description = ScheduleMaster.Description;
            this.Manager = ScheduleMaster.Manager == null ? null : new ScheduleMaster_AppUserDTO(ScheduleMaster.Manager);
            this.Saler = ScheduleMaster.Saler == null ? null : new ScheduleMaster_AppUserDTO(ScheduleMaster.Saler);
            this.Status = ScheduleMaster.Status == null ? null : new ScheduleMaster_StatusDTO(ScheduleMaster.Status);
            this.CreatedAt = ScheduleMaster.CreatedAt;
            this.UpdatedAt = ScheduleMaster.UpdatedAt;
            this.Errors = ScheduleMaster.Errors;
        }
    }

    public class ScheduleMaster_ScheduleMasterFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public IdFilter ManagerId { get; set; }
        public IdFilter SalerId { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Code { get; set; }
        public IdFilter StatusId { get; set; }
        public DateFilter RecurDays { get; set; }
        public DateFilter StartDate { get; set; }
        public DateFilter EndDate { get; set; }
        public DateFilter StartDayOfWeek { get; set; }
        public LongFilter DisplayOrder { get; set; }
        public StringFilter Description { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ScheduleMasterOrder OrderBy { get; set; }
    }
}
