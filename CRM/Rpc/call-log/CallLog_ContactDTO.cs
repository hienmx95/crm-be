using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.call_log
{
    public class CallLog_ContactDTO : DataDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public CallLog_ContactDTO() {}
        public CallLog_ContactDTO(Contact Contact)
        {
            this.Id = Contact.Id;
            this.Name = Contact.Name;
        }
    }

    public class CallLog_ContactFilterDTO : FilterDTO
    {
        public IdFilter Id { get; set; }
        public StringFilter Name { get; set; }
        public ContactOrder OrderBy { get; set; }
    }
}