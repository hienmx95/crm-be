using CRM.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Enums
{
    public class EntityReferenceEnum
    {
        public static GenericEnum CUSTOMER_LEAD => new GenericEnum { Id = 1, Name = "Lead", Code = "CUSTOMER_LEAD" };
        public static GenericEnum CONTACT => new GenericEnum { Id = 2, Name = "Liên hệ", Code = "CONTACT" };
        public static GenericEnum ACCOUNT => new GenericEnum { Id = 3, Name = "Công ty", Code = "ACCOUNT" };
        public static GenericEnum OPPORTUNITY => new GenericEnum { Id = 4, Name = "Cơ hội", Code = "OPPORTUNITY" };
        public static GenericEnum CUSTOMER_RETAIL => new GenericEnum { Id = 5, Name = "Khách hàng lẻ", Code = "CUSTOMER_RETAIL" };
        public static GenericEnum CUSTOMER_AGENT => new GenericEnum { Id = 6, Name = "Khách hàng đại lý", Code = "CUSTOMER_AGENT" };
        public static GenericEnum CUSTOMER_PROJECT => new GenericEnum { Id = 7, Name = "Khách hàng dự án", Code = "CUSTOMER_PROJECT" };
        public static GenericEnum CUSTOMER_EXPORT => new GenericEnum { Id = 8, Name = "Khách hàng xuất khẩu", Code = "CUSTOMER_EXPORT" };

        public static List<GenericEnum> EntityReferenceEnumList = new List<GenericEnum>()
        {
            CUSTOMER_LEAD, CONTACT,ACCOUNT,OPPORTUNITY, CUSTOMER_RETAIL, CUSTOMER_AGENT, CUSTOMER_PROJECT, CUSTOMER_EXPORT
        };
    }
}
