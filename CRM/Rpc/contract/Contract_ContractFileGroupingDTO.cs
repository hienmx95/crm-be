using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contract
{
    public class Contract_ContractFileGroupingDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long ContractId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid RowId { get; set; }
        public Contract_AppUserDTO Creator { get; set; }
        public Contract_FileTypeDTO FileType { get; set; }

        public List<Contract_ContractFileMappingDTO> ContractFileMappings { get; set; }

        public Contract_ContractFileGroupingDTO() { }
        public Contract_ContractFileGroupingDTO(ContractFileGrouping ContractFileGrouping)
        {
            this.Id = ContractFileGrouping.Id;
            this.Title = ContractFileGrouping.Title;
            this.Description = ContractFileGrouping.Description;
            this.CreatorId = ContractFileGrouping.CreatorId;
            this.ContractId = ContractFileGrouping.ContractId;
            this.FileTypeId = ContractFileGrouping.FileTypeId;
            this.CreatedAt = ContractFileGrouping.CreatedAt;
            this.UpdatedAt = ContractFileGrouping.UpdatedAt;
            this.RowId = ContractFileGrouping.RowId;
            this.Errors = ContractFileGrouping.Errors;
            this.Creator = ContractFileGrouping.Creator == null ? null : new Contract_AppUserDTO(ContractFileGrouping.Creator);
            this.FileType = ContractFileGrouping.FileType == null ? null : new Contract_FileTypeDTO(ContractFileGrouping.FileType);
            this.ContractFileMappings = ContractFileGrouping.ContractFileMappings?.Select(x => new Contract_ContractFileMappingDTO(x)).ToList();
        }
    }

    public class Contract_ContractFileGroupingFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter Description { get; set; }
        public IdFilter ContractId { get; set; }
        public IdFilter CreatorId { get; set; }
        public IdFilter FileTypeId { get; set; }
        public DateFilter CreatedAt { get; set; }
        public DateFilter UpdatedAt { get; set; }
        public ContractFileGroupingOrder OrderBy { get; set; }
    }
}
