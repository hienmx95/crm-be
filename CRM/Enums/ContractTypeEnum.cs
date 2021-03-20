using CRM.Common;
using System.Collections.Generic;

namespace CRM.Enums
{
    public class ContractTypeEnum
    {
        public static GenericEnum RETAIL = new GenericEnum { Id = 1, Code = "RETAIL", Name = "Khách lẻ" };
        public static GenericEnum COMPANY = new GenericEnum { Id = 2, Code = "COMPANY", Name = "Công ty" };

        public static List<GenericEnum> ContractTypeEnumList = new List<GenericEnum>
        {
            RETAIL, COMPANY
        };
    }

}
