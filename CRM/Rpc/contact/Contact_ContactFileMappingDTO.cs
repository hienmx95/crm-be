using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ContactFileMappingDTO : DataDTO
    {
        public long FileId { get; set; }
        public long ContactFileGroupingId { get; set; }
        public Contact_FileDTO File { get; set; }

        public Contact_ContactFileMappingDTO() { }
        public Contact_ContactFileMappingDTO(ContactFileMapping ContactFileMapping)
        {
            this.FileId = ContactFileMapping.FileId;
            this.ContactFileGroupingId = ContactFileMapping.ContactFileGroupingId;
            this.File = ContactFileMapping.File == null ? null : new Contact_FileDTO(ContactFileMapping.File);
            this.Errors = ContactFileMapping.Errors;
        }
    }

    public class Contract_ContactFileGroupingDTOFilterDTO : FilterDTO
    {

        public IdFilter FileId { get; set; }

        public IdFilter ContactFileGroupingId { get; set; }

        public ContactFileMappingOrder OrderBy { get; set; }
    }
}