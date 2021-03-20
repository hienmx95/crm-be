using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_CompanyDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CallLog_CompanyDTO() {}
        public CallLog_CompanyDTO(Company Company)
        {
            this.Id = Company.Id;
            this.Name = Company.Name;
        }
    }

    public class CallLog_CompanyFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public CompanyOrder OrderBy { get; set; }
    }
}