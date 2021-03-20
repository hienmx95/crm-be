using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CompanyFileGroupingDTO : DataDTO
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long CompanyId { get; set; }
        public long CreatorId { get; set; }
        public long FileTypeId { get; set; }
        public Guid RowId { get; set; }
        public Company_AppUserDTO Creator { get; set; }   
        public Company_FileTypeDTO FileType { get; set; }   

        public Company_CompanyFileGroupingDTO() {}
        public Company_CompanyFileGroupingDTO(CompanyFileGrouping CompanyFileGrouping)
        {
            this.Id = CompanyFileGrouping.Id;
            this.Title = CompanyFileGrouping.Title;
            this.Description = CompanyFileGrouping.Description;
            this.CompanyId = CompanyFileGrouping.CompanyId;
            this.CreatorId = CompanyFileGrouping.CreatorId;
            this.FileTypeId = CompanyFileGrouping.FileTypeId;
            this.RowId = CompanyFileGrouping.RowId;
            this.Creator = CompanyFileGrouping.Creator == null ? null : new Company_AppUserDTO(CompanyFileGrouping.Creator);
            this.FileType = CompanyFileGrouping.FileType == null ? null : new Company_FileTypeDTO(CompanyFileGrouping.FileType);
            this.RowId = CompanyFileGrouping.RowId;
            this.Errors = CompanyFileGrouping.Errors;
        }
    }

    public class Company_CompanyFileGroupingFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Title { get; set; }
        
        public StringFilter Description { get; set; }
        
        public IdFilter CompanyId { get; set; }
        
        public IdFilter CreatorId { get; set; }
        
        public IdFilter FileTypeId { get; set; }
        
        public GuidFilter RowId { get; set; }
        
        public CompanyFileGroupingOrder OrderBy { get; set; }
    }
}