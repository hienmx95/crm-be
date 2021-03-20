using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class CustomerTypeEnum
    {
        public static GenericEnum RETAIL = new GenericEnum { Id = 1, Code = "RETAIL", Name = "Khách hàng lẻ" };
        public static GenericEnum COMPANY = new GenericEnum { Id = 2, Code = "COMPANY", Name = "Khách hàng công ty" };
        public static List<GenericEnum> CustomerTypeEnumList = new List<GenericEnum>()
        {
            COMPANY, RETAIL
        };
    }

}
