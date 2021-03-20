using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_FileTypeDTO : DataDTO
    {
        
        public long Id { get; set; }
        
        public string Code { get; set; }
        
        public string Name { get; set; }
        

        public Company_FileTypeDTO() {}
        public Company_FileTypeDTO(FileType FileType)
        {
            
            this.Id = FileType.Id;
            
            this.Code = FileType.Code;
            
            this.Name = FileType.Name;
            
            this.Errors = FileType.Errors;
        }
    }

    public class Company_FileTypeFilterDTO : FilterDTO
    {
        
        public IdFilter Id { get; set; }
        
        public StringFilter Code { get; set; }
        
        public StringFilter Name { get; set; }
        
        public FileTypeOrder OrderBy { get; set; }
    }
}