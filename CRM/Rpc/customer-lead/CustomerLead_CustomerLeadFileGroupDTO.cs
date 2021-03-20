using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadFileGroupDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CustomerLeadId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } 
        public CustomerLead_AppUserDTO Creator { get; set; } 
        public CustomerLead_FileTypeDTO FileType { get; set; } 

        public List<CustomerLead_CustomerLeadFileMappingDTO> CustomerLeadFileMappings { get; set; }

        public CustomerLead_CustomerLeadFileGroupDTO() {}
        public CustomerLead_CustomerLeadFileGroupDTO(CustomerLeadFileGroup CustomerLeadFileGroup)
        {
            this.Id = CustomerLeadFileGroup.Id;
            this.Title = CustomerLeadFileGroup.Title;
            this.Description = CustomerLeadFileGroup.Description;
            this.CreatorId = CustomerLeadFileGroup.CreatorId;
            this.CustomerLeadId = CustomerLeadFileGroup.CustomerLeadId;
            this.FileTypeId = CustomerLeadFileGroup.FileTypeId;
            this.CreatedAt = CustomerLeadFileGroup.CreatedAt;
            this.UpdatedAt = CustomerLeadFileGroup.UpdatedAt;
            this.Errors = CustomerLeadFileGroup.Errors;
            this.Creator = CustomerLeadFileGroup.Creator == null ? null : new CustomerLead_AppUserDTO(CustomerLeadFileGroup.Creator);
            this.FileType = CustomerLeadFileGroup.FileType == null ? null : new CustomerLead_FileTypeDTO(CustomerLeadFileGroup.FileType);
            this.CustomerLeadFileMappings = CustomerLeadFileGroup.CustomerLeadFileMappings?.Select(x => new CustomerLead_CustomerLeadFileMappingDTO(x)).ToList();
        }
    }

    public class CustomerLead_CustomerLeadFileGroupFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter CustomerLeadId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter FileTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CustomerLeadFileGroupOrder OrderBy { get; set; }
    }
}
