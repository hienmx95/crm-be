using System;
using System.Collections.Generic;

namespace CRM.Models
{
    public partial class ContactDAO
    {
        public ContactDAO()
        {
            ContactActivities = new HashSet<ContactActivityDAO>();
            ContactCallLogMappings = new HashSet<ContactCallLogMappingDAO>();
            ContactEmails = new HashSet<ContactEmailDAO>();
            ContactFileGroupings = new HashSet<ContactFileGroupingDAO>();
            ContractContactMappings = new HashSet<ContractContactMappingDAO>();
            OpportunityContactMappings = new HashSet<OpportunityContactMappingDAO>();
            OrderQuotes = new HashSet<OrderQuoteDAO>();
        }

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
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngày cập nhật
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Ngày xoá
        /// </summary>
        public DateTime? DeletedAt { get; set; }

        public virtual AppUserDAO AppUser { get; set; }
        public virtual CompanyDAO Company { get; set; }
        public virtual ContactStatusDAO ContactStatus { get; set; }
        public virtual AppUserDAO Creator { get; set; }
        public virtual CustomerLeadDAO CustomerLead { get; set; }
        public virtual DistrictDAO District { get; set; }
        public virtual ImageDAO Image { get; set; }
        public virtual NationDAO Nation { get; set; }
        public virtual PositionDAO Position { get; set; }
        public virtual ProfessionDAO Profession { get; set; }
        public virtual ProvinceDAO Province { get; set; }
        public virtual SexDAO Sex { get; set; }
        public virtual ICollection<ContactActivityDAO> ContactActivities { get; set; }
        public virtual ICollection<ContactCallLogMappingDAO> ContactCallLogMappings { get; set; }
        public virtual ICollection<ContactEmailDAO> ContactEmails { get; set; }
        public virtual ICollection<ContactFileGroupingDAO> ContactFileGroupings { get; set; }
        public virtual ICollection<ContractContactMappingDAO> ContractContactMappings { get; set; }
        public virtual ICollection<OpportunityContactMappingDAO> OpportunityContactMappings { get; set; }
        public virtual ICollection<OrderQuoteDAO> OrderQuotes { get; set; }
    }
}
