using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CompanyFileMappingDTO : DataDTO
    {
        public long FileId { get; set; }
        public long CompanyFileGroupingId { get; set; }
        public Company_FileDTO File { get; set; }

        public Company_CompanyFileMappingDTO() { }
        public Company_CompanyFileMappingDTO(CompanyFileMapping CompanyFileMapping)
        {
            this.FileId = CompanyFileMapping.FileId;
            this.CompanyFileGroupingId = CompanyFileMapping.CompanyFileGroupingId;
            this.File = CompanyFileMapping.File == null ? null : new Company_FileDTO(CompanyFileMapping.File);
            this.Errors = CompanyFileMapping.Errors;
        }
    }

    public class Company_CompanyFileGroupingDTOFilterDTO : FilterDTO
    {

        public IdFilter FileId { get; set; }

        public IdFilter CompanyFileGroupingId { get; set; }

        public CompanyFileMappingOrder OrderBy { get; set; }
    }
}