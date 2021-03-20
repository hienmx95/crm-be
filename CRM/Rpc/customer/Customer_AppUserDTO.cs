using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer
{
    public class Customer_AppUserDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Username { get; set; }
        
        public string DisplayName { get; set; }
        
        public string Address { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public long SexId { get; set; }
        
        public DateTime? Birthday { get; set; }
        
        public string Avatar { get; set; }
        
        public string Department { get; set; }
        
        public long OrganizationId { get; set; }
        
        public decimal? Longitude { get; set; }
        
        public decimal? Latitude { get; set; }
        
        public long StatusId { get; set; }
        
        public Guid RowId { get; set; }
        
        public bool Used { get; set; }
        

        public Customer_AppUserDTO() {}
        public Customer_AppUserDTO(AppUser AppUser)
        {
            
            this.Id = AppUser.Id;
            
            this.Username = AppUser.Username;
            
            this.DisplayName = AppUser.DisplayName;
            
            this.Address = AppUser.Address;
            
            this.Email = AppUser.Email;
            
            this.Phone = AppUser.Phone;
            
            this.SexId = AppUser.SexId;
            
            this.Birthday = AppUser.Birthday;
            
            this.Avatar = AppUser.Avatar;
            
            this.Department = AppUser.Department;
            
            this.OrganizationId = AppUser.OrganizationId;
            
            this.Longitude = AppUser.Longitude;
            
            this.Latitude = AppUser.Latitude;
            
            this.StatusId = AppUser.StatusId;
            
            this.RowId = AppUser.RowId;
            
            this.Used = AppUser.Used;
            
            this.Errors = AppUser.Errors;
        }
    }

    public class Customer_AppUserFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Username { get; set; }
        
        public StringFilter DisplayName { get; set; }
        
        public StringFilter Address { get; set; }
        
        public StringFilter Email { get; set; }
        
        public StringFilter Phone { get; set; }
        
        public IdFilter SexId { get; set; }
        
        public DateFilter Birthday { get; set; }
        
        public StringFilter Avatar { get; set; }
        
        public StringFilter Department { get; set; }
        
        public IdFilter OrganizationId { get; set; }
        
        public DecimalFilter Longitude { get; set; }
        
        public DecimalFilter Latitude { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public AppUserOrder OrderBy { get; set; }
    }
}