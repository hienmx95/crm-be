using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class SendMailToEnum
    {
        public static GenericEnum CUSTOMER_LEAD = new GenericEnum { Id = 1, Code = "CUSTOMER_LEAD", Name = "Đầu mối" };
        public static GenericEnum OPPORTUNITY = new GenericEnum { Id = 2, Code = "OPPORTUNITY", Name = "Cơ hội" };
        public static GenericEnum ACCOUNT = new GenericEnum { Id = 3, Code = "ACCOUNT", Name = "Công ty" };
        public static GenericEnum CONTACT = new GenericEnum { Id = 4, Code = "CONTACT", Name = "Liên hệ" };
        public static List<GenericEnum> CompanyStatusEnumList = new List<GenericEnum>
        {
            CUSTOMER_LEAD, OPPORTUNITY, ACCOUNT, CONTACT
        };
    }

}
