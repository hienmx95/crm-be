using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_MappingDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Company_MappingDTO() {}
        public Company_MappingDTO(Mapping Mapping)
        {
            this.Id = Mapping.Id;
            this.Name = Mapping.Name;
        }
    }
    public class Company_MappingFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter ReferenceCode { get; set; }
    }

}