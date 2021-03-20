using CRM.Common;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_ContractFileMappingDTO : DataDTO
    {
        public long FileId { get; set; }
        public long ContractFileGroupingId { get; set; }
        public Contract_FileDTO File { get; set; }

        public Contract_ContractFileMappingDTO() { }
        public Contract_ContractFileMappingDTO(ContractFileMapping ContractFileMapping)
        {
            this.FileId = ContractFileMapping.FileId;
            this.ContractFileGroupingId = ContractFileMapping.ContractFileGroupingId;
            this.File = ContractFileMapping.File == null ? null : new Contract_FileDTO(ContractFileMapping.File);
            this.Errors = ContractFileMapping.Errors;
        }
    }

    public class Contract_ContractFileGroupingDTOFilterDTO : FilterDTO
    {

        public IdFilter FileId { get; set; }

        public IdFilter ContractFileGroupingId { get; set; }

        public ContractFileMappingOrder OrderBy { get; set; }
    }
}