using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.customer_lead
{
    public class CustomerLead_CustomerLeadFileMappingDTO : DataDTO
    {
        public long FileId { get; set; }
        public long CustomerLeadFileGroupId { get; set; }
        public CustomerLead_FileDTO File { get; set; }
        public CustomerLead_CustomerLeadFileMappingDTO() { }
        public CustomerLead_CustomerLeadFileMappingDTO(CustomerLeadFileMapping CustomerLeadFileMapping)
        {
            this.CustomerLeadFileGroupId = CustomerLeadFileMapping.CustomerLeadFileGroupId;
            this.FileId = CustomerLeadFileMapping.FileId;
            this.File = CustomerLeadFileMapping.File == null ? null : new CustomerLead_FileDTO(CustomerLeadFileMapping.File);
        }
    }

    public class CustomerLead_CustomerLeadFileMappingFilterDTO : FilterDTO
    {

        public IdFilter FileId { get; set; }

        public IdFilter CustomerLeadFileGroupId { get; set; }

        public CustomerLeadFileGroupOrder OrderBy { get; set; }
    }
}