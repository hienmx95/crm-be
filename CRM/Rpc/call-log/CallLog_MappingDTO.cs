using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_MappingDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CallLog_MappingDTO() {}
        public CallLog_MappingDTO(Mapping Mapping)
        {
            this.Id = Mapping.Id;
            this.Name = Mapping.Name;
        }
    }
    public class CallLog_MappingFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public StringFilter ReferenceCode { get; set; }
    }

}