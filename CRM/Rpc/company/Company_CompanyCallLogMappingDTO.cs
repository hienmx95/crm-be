using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.company
{
    public class Company_CompanyCallLogMappingDTO : DataDTO
    {
        public long CompanyId { get; set; }
        public long CallLogId { get; set; }
        public Company_CallLogDTO CallLog { get; set; }   

        public Company_CompanyCallLogMappingDTO() {}
        public Company_CompanyCallLogMappingDTO(CompanyCallLogMapping CompanyCallLogMapping)
        {
            this.CompanyId = CompanyCallLogMapping.CompanyId;
            this.CallLogId = CompanyCallLogMapping.CallLogId;
            this.CallLog = CompanyCallLogMapping.CallLog == null ? null : new Company_CallLogDTO(CompanyCallLogMapping.CallLog);
            this.Errors = CompanyCallLogMapping.Errors;
        }
    }

    public class Company_CompanyCallLogMappingFilterDTO : FilterDTO
    {
        
        public IdFilter CompanyId { get; set; }
        
        public IdFilter CallLogId { get; set; }
        
        public CompanyCallLogMappingOrder OrderBy { get; set; }
    }
}