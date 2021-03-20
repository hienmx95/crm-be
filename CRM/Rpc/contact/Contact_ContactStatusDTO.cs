using CRM.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using CRM.Entities;

namespace CRM.Rpc.contact
{
    public class Contact_ContactStatusDTO : DataDTO
    {

        public long Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public long? DisplayOrder { get; set; }


        public Contact_ContactStatusDTO() { }
        public Contact_ContactStatusDTO(ContactStatus ContactStatus)
        {

            this.Id = ContactStatus.Id;

            this.Code = ContactStatus.Code;

            this.Name = ContactStatus.Name;
             
            this.Errors = ContactStatus.Errors;
        }
    }

    public class Contact_ContactStatusFilterDTO : FilterDTO
    {

        public IdFilter Id { get; set; }

        public StringFilter Code { get; set; }

        public StringFilter Name { get; set; }

        public LongFilter DisplayOrder { get; set; }

        public ContactStatusOrder OrderBy { get; set; }
    }
}