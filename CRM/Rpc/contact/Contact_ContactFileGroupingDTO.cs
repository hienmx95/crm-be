using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ContactFileGroupingDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long ContactId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Contact_AppUserDTO Creator { get; set; }
        public Contact_FileTypeDTO FileType { get; set; }

        public List<Contact_ContactFileMappingDTO> ContactFileMappings { get; set; }

        public Contact_ContactFileGroupingDTO() { }
        public Contact_ContactFileGroupingDTO(ContactFileGrouping ContactFileGrouping)
        {
            this.Id = ContactFileGrouping.Id;
            this.Title = ContactFileGrouping.Title;
            this.Description = ContactFileGrouping.Description;
            this.CreatorId = ContactFileGrouping.CreatorId;
            this.ContactId = ContactFileGrouping.ContactId;
            this.FileTypeId = ContactFileGrouping.FileTypeId;
            this.CreatedAt = ContactFileGrouping.CreatedAt;
            this.UpdatedAt = ContactFileGrouping.UpdatedAt;
            this.Errors = ContactFileGrouping.Errors;
            this.Creator = ContactFileGrouping.Creator == null ? null : new Contact_AppUserDTO(ContactFileGrouping.Creator);
            this.FileType = ContactFileGrouping.FileType == null ? null : new Contact_FileTypeDTO(ContactFileGrouping.FileType);
            this.ContactFileMappings = ContactFileGrouping.ContactFileMappings?.Select(x => new Contact_ContactFileMappingDTO(x)).ToList();
        }
    }

    public class Contract_ContactFileGroupingFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter ContactId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter FileTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ContactFileGroupingOrder OrderBy { get; set; }
    }
}
