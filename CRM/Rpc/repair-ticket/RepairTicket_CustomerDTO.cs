using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.repair_ticket
{
    public class RepairTicket_CustomerDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string FullName { get; set; }
        
        public string Phone { get; set; }
        
        public string IdentificationNumber { get; set; }
        
        public string Email { get; set; }
        
        public string Address { get; set; }
        
        public long StatusId { get; set; }
        
        public long? NationId { get; set; }
        
        public long? ProvinceId { get; set; }
        
        public long? DistrictId { get; set; }
        
        public long? WardId { get; set; }
        
        public long CreatorId { get; set; }
        
        public long? OrganizationId { get; set; }
        
        public long? ImageId { get; set; }
        
        public long? ProfessionId { get; set; }
        
        public bool? Used { get; set; }
        

        public RepairTicket_CustomerDTO() {}
        public RepairTicket_CustomerDTO(Customer Customer)
        {
            
            this.Id = Customer.Id;
            
            this.Code = Customer.Code;
            
            this.FullName = Customer.Name;
            
            this.Phone = Customer.Phone;
            
            
            this.Email = Customer.Email;
            
            this.Address = Customer.Address;
            
            this.StatusId = Customer.StatusId;
            
            this.NationId = Customer.NationId;
            
            this.ProvinceId = Customer.ProvinceId;
            
            this.DistrictId = Customer.DistrictId;
            
            this.WardId = Customer.WardId;
            
            this.ProfessionId = Customer.ProfessionId;
            
            this.Used = Customer.Used;
            
            this.Errors = Customer.Errors;
        }
    }

    public class RepairTicket_CustomerFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter FullName { get; set; }
        
        public StringFilter Phone { get; set; }
        
        public StringFilter IdentificationNumber { get; set; }
        
        public StringFilter Email { get; set; }
        
        public StringFilter Address { get; set; }
        
        public IdFilter StatusId { get; set; }
        
        public IdFilter NationId { get; set; }
        
        public IdFilter ProvinceId { get; set; }
        
        public IdFilter DistrictId { get; set; }
        
        public IdFilter WardId { get; set; }
        
        public IdFilter CreatorId { get; set; }
        
        public IdFilter OrganizationId { get; set; }
        
        public IdFilter ImageId { get; set; }
        
        public IdFilter ProfessionId { get; set; }
        
        public CustomerOrder OrderBy { get; set; }
    }
}