using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.order_quote
{
    public class OrderQuote_ContactDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        public long? ProfessionId { get; set; }
        
        public long? CompanyId { get; set; }
        
        public long? ContactStatusId { get; set; }
        
        public string Address { get; set; }
        
        public long? NationId { get; set; }
        
        public long? ProvinceId { get; set; }
        
        public long? DistrictId { get; set; }
        
        public long? CustomerLeadId { get; set; }
        
        public long? ImageId { get; set; }
        
        public string Description { get; set; }
        
        public string EmailOther { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        public string Phone { get; set; }
        
        public string PhoneHome { get; set; }
        
        public string FAX { get; set; }
        
        public string Email { get; set; }
        
        public string Department { get; set; }
        
        public string ZIPCode { get; set; }
        
        public long? SexId { get; set; }
        
        public long? AppUserId { get; set; }
        
        public bool? RefuseReciveEmail { get; set; }
        
        public bool? RefuseReciveSMS { get; set; }
        
        public long? PositionId { get; set; }
        
        public long CreatorId { get; set; }
        

        public OrderQuote_ContactDTO() {}
        public OrderQuote_ContactDTO(Contact Contact)
        {
            
            this.Id = Contact.Id;
            
            this.Name = Contact.Name;
            
            this.ProfessionId = Contact.ProfessionId;
            
            this.CompanyId = Contact.CompanyId;
            
            this.ContactStatusId = Contact.ContactStatusId;
            
            this.Address = Contact.Address;
            
            this.NationId = Contact.NationId;
            
            this.ProvinceId = Contact.ProvinceId;
            
            this.DistrictId = Contact.DistrictId;
            
            this.CustomerLeadId = Contact.CustomerLeadId;
            
            this.ImageId = Contact.ImageId;
            
            this.Description = Contact.Description;
            
            this.EmailOther = Contact.EmailOther;
            
            this.DateOfBirth = Contact.DateOfBirth;
            
            this.Phone = Contact.Phone;
            
            this.PhoneHome = Contact.PhoneHome;
            
            this.FAX = Contact.FAX;
            
            this.Email = Contact.Email;
            
            this.Department = Contact.Department;
            
            this.ZIPCode = Contact.ZIPCode;
            
            this.SexId = Contact.SexId;
            
            this.AppUserId = Contact.AppUserId;
            
            this.RefuseReciveEmail = Contact.RefuseReciveEmail;
            
            this.RefuseReciveSMS = Contact.RefuseReciveSMS;
            
            this.PositionId = Contact.PositionId;
            
            this.CreatorId = Contact.CreatorId;
            
            this.Errors = Contact.Errors;
        }
    }

    public class OrderQuote_ContactFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Name { get; set; }
        
        public IdFilter ProfessionId { get; set; }
        
        public IdFilter CompanyId { get; set; }
        
        public IdFilter ContactStatusId { get; set; }
        
        public StringFilter Address { get; set; }
        
        public IdFilter NationId { get; set; }
        
        public IdFilter ProvinceId { get; set; }
        
        public IdFilter DistrictId { get; set; }
        
        public IdFilter CustomerLeadId { get; set; }
        
        public IdFilter ImageId { get; set; }
        
        public StringFilter Description { get; set; }
        
        public StringFilter EmailOther { get; set; }
        
        public DateFilter DateOfBirth { get; set; }
        
        public StringFilter Phone { get; set; }
        
        public StringFilter PhoneHome { get; set; }
        
        public StringFilter FAX { get; set; }
        
        public StringFilter Email { get; set; }
        
        public StringFilter Department { get; set; }
        
        public StringFilter ZIPCode { get; set; }
        
        public IdFilter SexId { get; set; }
        
        public IdFilter AppUserId { get; set; }
        
        public IdFilter PositionId { get; set; }
        
        public IdFilter CreatorId { get; set; }
        
        public ContactOrder OrderBy { get; set; }
    }
}