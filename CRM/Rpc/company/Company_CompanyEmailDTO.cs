using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CompanyEmailDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Reciepient { get; set; }
        public long CompanyId { get; set; }
        public long CreatorId { get; set; }
        public long EmailStatusId { get; set; }
        public Company_AppUserDTO Creator { get; set; }   
        public Company_EmailStatusDTO EmailStatus { get; set; }   
        public List<Company_CompanyEmailCCMappingDTO> CompanyEmailCCMappings { get; set; }
        public Company_CompanyEmailDTO() {}
        public Company_CompanyEmailDTO(CompanyEmail CompanyEmail)
        {
            this.Id = CompanyEmail.Id;
            this.Title = CompanyEmail.Title;
            this.Content = CompanyEmail.Content;
            this.Reciepient = CompanyEmail.Reciepient;
            this.CompanyId = CompanyEmail.CompanyId;
            this.CreatorId = CompanyEmail.CreatorId;
            this.EmailStatusId = CompanyEmail.EmailStatusId;
            this.Creator = CompanyEmail.Creator == null ? null : new Company_AppUserDTO(CompanyEmail.Creator);
            this.EmailStatus = CompanyEmail.EmailStatus == null ? null : new Company_EmailStatusDTO(CompanyEmail.EmailStatus);
            this.CompanyEmailCCMappings = CompanyEmail.CompanyEmailCCMappings?.Select(x => new Company_CompanyEmailCCMappingDTO(x)).ToList();
            this.Errors = CompanyEmail.Errors;
        }
    }

    public class Company_CompanyEmailFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Title { get; set; }
        
        public StringFilter Content { get; set; }
        
        public StringFilter Reciepient { get; set; }
        
        public IdFilter CompanyId { get; set; }
        
        public IdFilter CreatorId { get; set; }
        
        public IdFilter EmailStatusId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public CompanyEmailOrder OrderBy { get; set; }
    }
}